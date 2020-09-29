using Hulkey.DAL;
using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Hulkey.PLL.PresentationCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.Helpers;

namespace Hulkey.PLL.PresentationCommon
{
    /// <summary>
    /// ViewModel pour la page Utilisateur Détaille
    /// </summary>
    public class UtilisateurDetailViewModel : ViewModelBase
    {
        public UtilisateurDetailViewModel()
        {
        }

        /// <summary>
        /// Charge l'Utilisateur specifié.
        /// Si l'ID est egal à 0, alors c'est un nouvelle utilisteur qui est fabriqué
        /// </summary>
        public void LoadUtilisateur(int UtilisateurID)
        {
            if (UtilisateurID == 0)
            {
                // Creation d'un nouvel utilisateur
                this.IsInEdit = true;
                this.IsNewUtilisateur = true;
                this.IsModified = true;

                this.Utilisateur = new Utilisateur();
            }
            else
            {
                // Chargement de l'utilisateur
                this.IsInEdit = false;
                this.IsNewUtilisateur = false;
                this.IsModified = false;

                using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
                {
                    IRepository<Utilisateur> repUtilisateur = uow.GetRepositoryUtilisateur();
                    this.Utilisateur = repUtilisateur.Read(UtilisateurID);
                }
            }
        }

        /// <summary>
        /// Charge les dependences
        /// Les listes annexe utilisées  
        /// </summary>
        public void LoadDependencies()
        {
            // Rien pour le momment
        }

        public string Nom
        {
            get => Utilisateur.Nom;
            set
            {
                if (value != Utilisateur.Nom)
                {
                    Utilisateur.Nom = value;
                    IsModified = true;
                    NotifyChanges();
                }
            }
        }
        public string Prenom
        {
            get => Utilisateur.Prenom;
            set
            {
                if (value != Utilisateur.Prenom)
                {
                    Utilisateur.Prenom = value;
                    IsModified = true;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Password
        {
            get => Utilisateur.Password;
            set
            {
                if (value != Utilisateur.Password)
                {
                    Utilisateur.Password = value;
                    IsModified = true;
                    NotifyPropertyChanged();
                }
            }
        }
        public string eMail
        {
            get => Utilisateur.eMail;
            set
            {
                if (value != Utilisateur.eMail)
                {
                    Utilisateur.eMail = value;
                    IsModified = true;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Telephonne
        {
            get => Utilisateur.Telephonne;
            set
            {
                if (value != Utilisateur.Telephonne)
                {
                    Utilisateur.Telephonne = value;
                    IsModified = true;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Utilisateur.
        /// </summary>
        public Utilisateur Utilisateur
        {
            get => m_Utilisateur;
            set
            {
                Set(ref m_Utilisateur, value);
                NotifyChanges();
            }
        }
        private Utilisateur m_Utilisateur;

        /// <summary>
        /// Gets or sets a value that indicates whether to show a progress bar. 
        /// </summary>
        public bool IsLoading
        {
            get => m_isLoading;
            set => Set(ref m_isLoading, value);
        }
        private bool m_isLoading;

        /// <summary>
        /// Gets or sets a value that indicates whether this is a new customer.
        /// </summary>
        public bool IsNewUtilisateur
        {
            get => m_isNewUtilisateur;
            set => Set(ref m_isNewUtilisateur, value);
        }
        private bool m_isNewUtilisateur;

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
                    IRepository<Utilisateur> repo = uow.GetRepositoryUtilisateur();
                    if (IsNewUtilisateur == true)
                    {
                        IsNewUtilisateur = false;
                        repo.Create(this.Utilisateur);
                    }
                    else
                    {
                        repo.Update(this.Utilisateur);
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
                //await Task.Run(() =>
                //{
                    LoadUtilisateur(this.Utilisateur.ID);
                    IsModified = false;
                //});
            }
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
