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
    /// ViewModel pour la page TPF Master/Détaille
    /// </summary>
    public class TPFMasterDetailsViewModel : Observable
    {
        public TPFMasterDetailsViewModel()
        {
        }

        /// <summary>
        /// Charger la liste des TPFs
        /// </summary>
        public void LoadTPFs()
        {
            List<TPF> lst;

            using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
            {
                TPFRepository repo = uow.GetRepositoryTPF();
                lst = repo.GetList();
            }

            this.TPFs.Clear();
            lst.ForEach(taxe => this.TPFs.Add(new TPFViewModel(taxe)));

            this.CurrentItem = this.TPFs.FirstOrDefault();
        }

        /// <summary>
        /// Charge l'item specifié par son ID.
        /// </summary>
        public TPF LoadTPF(int itemID)
        {
            TPF item;

            using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
            {
                IRepository<TPF> repository = uow.GetRepositoryTPF();
                item = repository.Read(itemID);
            }

            return item;
        }

        /// <summary>
        /// Gets the articles to display.
        /// </summary>
        public ObservableCollection<TPFViewModel> TPFs { get; private set; } = new ObservableCollection<TPFViewModel>();

        /// <summary>
        /// Gets or sets the current 
        /// </summary>
        public TPFViewModel CurrentItem
        {
            get => m_CurrentItem;
            set
            {
                Set(ref m_CurrentItem, value);
            }
        }
        private TPFViewModel m_CurrentItem;

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

        /// <summary>
        /// Saves customer data that has been edited.
        /// </summary>
        public async Task SaveAsync()
        {
            IsInEdit = false;
            IsModified = false;

            await Task.Run(() =>
            {
                using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
                {
                    IRepository<TPF> repo = uow.GetRepositoryTPF();
                    if (IsNewItem == true)
                    {
                        IsNewItem = false;
                        repo.Create(this.CurrentItem.Value);
                    }
                    else
                    {
                        repo.Update(this.CurrentItem.Value);
                    }

                    uow.SaveChanges();
                }
            });
        }

        /// <summary>
        /// Cancels any in progress edits.
        /// </summary>
        //TODO faire marché en async, probleme pendant la mise a jour par ipropertynotifychange 
        public /*async Task*/void CancelEditsAsync()
        {
            IsInEdit = false;

            if (IsModified == true)
            {
                this.CurrentItem.Value = LoadTPF(this.CurrentItem.ID);
                IsModified = false;
            }
        }

        /// <summary>
        /// Ajouoter un nouvelle TPF
        /// </summary>
        public void Add()
        {
            TPFViewModel newItem = new TPFViewModel(new TPF());
            this.TPFs.Add(newItem);
            this.CurrentItem = newItem;

            IsNewItem = true;
            StartEdit();
        }

        /// <summary>
        /// Supprimer un TPF
        /// </summary>
        public void Delete()
        {
            IsNewItem = false;
            IsInEdit = false;

            // suppresion de l'item courant dans la base
            if (this.CurrentItem.ID != 0)
            {
                using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
                {
                    IRepository<TPF> repo = uow.GetRepositoryTPF();
                    repo.DeleteById(this.CurrentItem.ID);
                    uow.SaveChanges();
                }
            }

            // Suppression de l'item courant dans la liste
            this.TPFs.Remove(this.CurrentItem);
            this.CurrentItem = this.TPFs.FirstOrDefault();
        }

        /// <summary>
        /// Enables edit mode.
        /// </summary>
        public void StartEdit()
        {
            IsInEdit = true;
        }

    }
}
