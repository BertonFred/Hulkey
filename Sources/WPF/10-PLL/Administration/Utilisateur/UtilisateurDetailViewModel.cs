using Hulkey.DAL.DTO;
using Hulkey.DAL.Entities;
using Hulkey.PLL.MVVM;
using Hulkey.PLL.PresentationCommon;
using Hulkey.SLL.Services;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using MessageDialog = Hulkey.PLL.MVVM.MessageDialog;

namespace Hulkey.PLL.Administration
{
    /// <summary>
    /// View model pour la page de detaille d'un utilisateur 
    /// </summary>
    public sealed class UtilisateurDetailViewModel : DetailViewModel
    {
        public UtilisateurDetailViewModel(int iUtilisateurID)
            : base()
        {
            this.Titre = "Utilisateur détail";
            this.Service = new UtilisateurBS(UserContext.Instance);

            Load(iUtilisateurID);
        }

        #region ACTIONS
        /// <summary>
        /// Charge les données de la view de detaille
        /// </summary>
        public override void Load(int iUtilisateurID)
        {
            this.UtilisateurID = iUtilisateurID;
            if (this.UtilisateurID == 0)
            {
                // Mode Creation d'un utilisateur
                this.Utilisateur = new Utilisateur();
                this.MarkAsTransiant();
            }
            else 
            {
                // Mode Edit, d'un utilisateur existant d'un utilisateur existant
                this.Utilisateur = Service.Read(this.UtilisateurID);
                this.MarkAsPersistant();
            }

            LoadDependences();

            NotifyChanges();
        }

        /// <summary>
        ///  Sauvegarde les modification de la view de detaille
        /// </summary>
        public override void Save()
        {
            if (this.PersistanteState == ePersistantState.Transiant)
            {
                // Mode Creation d'un utilisateur
                Service.Create(this.Utilisateur);
            }
            else
            {
                // Mode Edit d'un utilisateur existant
                Service.Update(this.Utilisateur);
            }
            this.MarkAsPersistant();

            ViewNavigationService.Instance.Navigate(typeof(UtilisateurListView));
        }

        /// <summary>
        ///  Ferme la vue de detaille
        /// </summary>
        public override void Close()
        {
            ConfirmClose();

            ViewNavigationService.Instance.Navigate(typeof(UtilisateurListView));
        }

        #endregion

        #region PROPERTIES
        /// <summary>
        /// ID de l'utilisateur, 0 pour une creation, >0 pour un utilisateur existant
        /// </summary>
        public int UtilisateurID
        {
            get => m_UtilisateurID;
            set => Set(ref m_UtilisateurID, value,bMarkAsModified:false);
        }
        private int m_UtilisateurID;

        /// <summary>
        /// Information de l'utilisateur courant
        /// </summary>
        public Utilisateur Utilisateur
        {
            get => m_Utilisateur;
            set => Set(ref m_Utilisateur, value, bMarkAsModified: false);
        }
        private Utilisateur m_Utilisateur;

        public string Nom
        {
            get => Utilisateur.Nom;
            set
            {
                if (value != Utilisateur.Nom)
                {
                    Utilisateur.Nom = value;
                    MarkAsModified();
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
                    MarkAsModified();
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
                    MarkAsModified();
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
                    MarkAsModified();
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
                    MarkAsModified();
                    NotifyPropertyChanged();
                }
            }
        }
        public eRoleUtilisateur Role
        {
            get => Utilisateur.Role;
            set
            {
                if (value != Utilisateur.Role)
                {
                    Utilisateur.Role = value;
                    MarkAsModified();
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Le service de gestion des utilisateurs
        /// </summary>
        private UtilisateurBS Service
        {
            get;set;
        }
        #endregion

        #region COMMAND
        #endregion
    }
}
