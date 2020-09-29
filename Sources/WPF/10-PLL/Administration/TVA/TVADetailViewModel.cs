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
    /// View model pour la page de detaille d'un tva 
    /// </summary>
    public sealed class TVADetailViewModel : DetailViewModel
    {
        public TVADetailViewModel(int iTVAID)
            : base()
        {
            this.Titre = "TVA détail";
            this.Service = new TVABS(UserContext.Instance);

            Load(iTVAID);
        }

        #region ACTIONS
        /// <summary>
        /// Charge les données de la view de detaille
        /// </summary>
        public override void Load(int iTVAID)
        {
            this.TVAID = iTVAID;
            if (this.TVAID == 0)
            {
                // Mode Creation d'un tva
                this.TVA = new TVA();
                this.MarkAsTransiant();
            }
            else 
            {
                // Mode Edit, d'un tva existant d'un tva existant
                this.TVA = Service.Read(this.TVAID);
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
                // Mode Creation d'un tva
                Service.Create(this.TVA);
            }
            else
            {
                // Mode Edit d'un tva existant
                Service.Update(this.TVA);
            }
            this.MarkAsPersistant();

            ViewNavigationService.Instance.Navigate(typeof(TVAListView));
        }

        /// <summary>
        ///  Ferme la vue de detaille
        /// </summary>
        public override void Close()
        {
            ConfirmClose();

            ViewNavigationService.Instance.Navigate(typeof(TVAListView));
        }

        #endregion

        #region PROPERTIES
        /// <summary>
        /// ID de l'tva, 0 pour une creation, >0 pour un tva existant
        /// </summary>
        public int TVAID
        {
            get => m_TVAID;
            set => Set(ref m_TVAID, value,bMarkAsModified:false);
        }
        private int m_TVAID;

        /// <summary>
        /// Information de l'tva courant
        /// </summary>
        public TVA TVA
        {
            get => m_TVA;
            set => Set(ref m_TVA, value, bMarkAsModified: false);
        }
        private TVA m_TVA;

        public string Code
        {
            get => TVA.Code;
            set
            {
                if (value != TVA.Code)
                {
                    TVA.Code = value;
                    MarkAsModified();
                    NotifyChanges();
                }
            }
        }
        public string Name
        {
            get => TVA.Name;
            set
            {
                if (value != TVA.Name)
                {
                    TVA.Name = value;
                    MarkAsModified();
                    NotifyChanges();
                }
            }
        }
        public decimal Taux
        {
            get => TVA.Taux;
            set
            {
                if (value != TVA.Taux)
                {
                    TVA.Taux = value;
                    MarkAsModified();
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Le service de gestion des tvas
        /// </summary>
        private TVABS Service
        {
            get;set;
        }
        #endregion

        #region COMMAND
        #endregion
    }
}
