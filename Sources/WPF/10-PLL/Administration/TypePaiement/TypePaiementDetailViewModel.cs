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
    /// View model pour la page de detaille d'un TypePaiement 
    /// </summary>
    public sealed class TypePaiementDetailViewModel : DetailViewModel
    {
        public TypePaiementDetailViewModel(int iTypePaiementID)
            : base()
        {
            this.Titre = "Type de paiement détail";
            this.Service = new TypePaiementBS(UserContext.Instance);

            Load(iTypePaiementID);
        }

        #region ACTIONS
        /// <summary>
        /// Charge les données de la view de detaille
        /// </summary>
        public override void Load(int iTypePaiementID)
        {
            this.TypePaiementID = iTypePaiementID;
            if (this.TypePaiementID == 0)
            {
                // Mode Creation d'un TypePaiement
                this.TypePaiement = new TypePaiement();
                this.MarkAsTransiant();
            }
            else 
            {
                // Mode Edit, d'un TypePaiement existant d'un TypePaiement existant
                this.TypePaiement = Service.Read(this.TypePaiementID);
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
                // Mode Creation d'un TypePaiement
                Service.Create(this.TypePaiement);
            }
            else
            {
                // Mode Edit d'un TypePaiement existant
                Service.Update(this.TypePaiement);
            }
            this.MarkAsPersistant();

            ViewNavigationService.Instance.Navigate(typeof(TypePaiementListView));
        }

        /// <summary>
        ///  Ferme la vue de detaille
        /// </summary>
        public override void Close()
        {
            ConfirmClose();
            ViewNavigationService.Instance.Navigate(typeof(TypePaiementListView));
        }

        #endregion

        #region PROPERTIES
        /// <summary>
        /// ID de l'TypePaiement, 0 pour une creation, >0 pour un TypePaiement existant
        /// </summary>
        public int TypePaiementID
        {
            get => m_TypePaiementID;
            set => Set(ref m_TypePaiementID, value,bMarkAsModified:false);
        }
        private int m_TypePaiementID;

        /// <summary>
        /// Information de l'TypePaiement courant
        /// </summary>
        public TypePaiement TypePaiement
        {
            get => m_TypePaiement;
            set => Set(ref m_TypePaiement, value, bMarkAsModified: false);
        }
        private TypePaiement m_TypePaiement;

        public string Code
        {
            get => TypePaiement.Code;
            set
            {
                if (value != TypePaiement.Code)
                {
                    TypePaiement.Code = value;
                    MarkAsModified();
                    NotifyChanges();
                }
            }
        }
        public string Name
        {
            get => TypePaiement.Name;
            set
            {
                if (value != TypePaiement.Name)
                {
                    TypePaiement.Name = value;
                    MarkAsModified();
                    NotifyChanges();
                }
            }
        }
        public string Libelle
        {
            get => TypePaiement.Libelle;
            set
            {
                if (value != TypePaiement.Libelle)
                {
                    TypePaiement.Libelle = value;
                    MarkAsModified();
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Le service de gestion des TypePaiements
        /// </summary>
        private TypePaiementBS Service
        {
            get;set;
        }
        #endregion

        #region COMMAND
        #endregion
    }
}
