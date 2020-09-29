using Hulkey.DAL;
using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Hulkey.PLL.PresentationCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Hulkey.DAL.DTO;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Hulkey.PLL.BackOffice
{
    /// <summary>
    /// ViewModel pour la page TVA Master/Détaille
    /// </summary>
    public class TVAMasterDetailsViewModel : Observable
    {
        public TVAMasterDetailsViewModel()
        {
        }

        /// <summary>
        /// Charger la liste des TVAs
        /// </summary>
        public async Task LoadTVAListAsync()
        {
            List<TVA> lst;

            using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
            {
                TVARepository repo = uow.GetRepositoryTVA();
                lst = await repo.GetListAsync();
            }

            this.TVAs.Clear();
            lst.ForEach(taxe => this.TVAs.Add(new TVAViewModel(taxe)));

            this.CurrentItem = this.TVAs.FirstOrDefault();
        }

        /// <summary>
        /// Charge l'item specifié par son ID.
        /// </summary>
        public async Task<TVA> LoadTVAAsync(int itemID)
        {
            TVA item;

            using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
            {
                IRepository<TVA> repository = uow.GetRepositoryTVA();
                item = await repository.ReadAsync(itemID);
            }

            return item;
        }

        /// <summary>
        /// Saves customer data that has been edited.
        /// </summary>
        public async Task SaveAsync()
        {
            IsInEdit = false;
            IsModified = false;

            using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
            {
                IRepository<TVA> repo = uow.GetRepositoryTVA();

                if (IsNewItem == true)
                {
                    IsNewItem = false;
                    await repo.CreateAsync(this.CurrentItem.Value);
                }
                else
                {
                    repo.Update(this.CurrentItem.Value);
                }

                await uow.SaveChangesAsync();
                this.CurrentItem.NotifyChanges();
            }
        }

        /// <summary>
        /// Supprimer un TVA
        /// </summary>
        public async Task DeleteAsync()
        {
            IsNewItem = false;
            IsInEdit = false;

            // suppresion de l'item courant dans la base
            if (this.CurrentItem.ID != 0)
            {
                using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
                {
                    IRepository<TVA> repo = uow.GetRepositoryTVA();
                    await repo.DeleteByIdAsync(this.CurrentItem.ID);
                    await uow.SaveChangesAsync();
                }
            }

            // Suppression de l'item courant dans la liste
            this.TVAs.Remove(this.CurrentItem);
            this.CurrentItem = this.TVAs.FirstOrDefault();
        }

        /// <summary>
        /// Ajouoter un nouvelle TVA
        /// </summary>
        public void Add()
        {
            TVAViewModel newItem = new TVAViewModel(new TVA());
            this.TVAs.Add(newItem);
            this.CurrentItem = newItem;

            IsNewItem = true;
            StartEdit();
        }

        /// <summary>
        /// Enables edit mode.
        /// </summary>
        public void StartEdit()
        {
            IsInEdit = true;
        }

        /// <summary>
        /// Cancels any in progress edits.
        /// </summary>
        public async Task CancelEditsAsync()
        {
            IsInEdit = false;

            if (IsModified == true)
            {
                this.CurrentItem.Value = await LoadTVAAsync(this.CurrentItem.ID);
                IsModified = false;
            }
        }

        /// <summary>
        /// Gets the articles to display.
        /// </summary>
        public ObservableCollection<TVAViewModel> TVAs { get; private set; } = new ObservableCollection<TVAViewModel>();

        /// <summary>
        /// Gets or sets the current 
        /// </summary>
        public TVAViewModel CurrentItem
        {
            get => m_CurrentItem;
            set
            {
                Set(ref m_CurrentItem, value);
            }
        }
        private TVAViewModel m_CurrentItem;

        /// <summary>
        /// Gets or sets a value that indicates whether this is a new item.
        /// </summary>
        public bool IsNewItem
        {
            get => m_IsNewItem;
            set => Set(ref m_IsNewItem, value);
        }
        private bool m_IsNewItem;

        /// <summary>
        /// Gets or sets a value that indicates whether the customer data is being edited.
        /// </summary>
        public bool IsInEdit
        {
            get => m_isInEdit;
            set => Set(ref m_isInEdit, value);
        }
        private bool m_isInEdit = false;

    }
}
