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
    /// View model pour la page de detaille d'un tpf 
    /// </summary>
    public sealed class TPFDetailViewModel : DetailViewModel
    {
        public TPFDetailViewModel(int iTPFID)
            : base()
        {
            this.Titre = "TPF détail";
            this.Service = new TPFBS(UserContext.Instance);
            Load(iTPFID);
        }

        #region ACTIONS
        /// <summary>
        /// Charge les données de la view de detaille
        /// </summary>
        public override void Load(int iTPFID)
        {
            this.TPFID = iTPFID;
            if (this.TPFID == 0)
            {
                // Mode Creation d'un tpf
                this.TPF = new TPF();
                this.MarkAsTransiant();
            }
            else 
            {
                // Mode Edit, d'un tpf existant d'un tpf existant
                this.TPF = Service.Read(this.TPFID);
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
                // Mode Creation d'un tpf
                Service.Create(this.TPF);
            }
            else
            {
                // Mode Edit d'un tpf existant
                Service.Update(this.TPF);
            }
            this.MarkAsPersistant();

            ViewNavigationService.Instance.Navigate(typeof(TPFListView));
        }

        /// <summary>
        ///  Ferme la vue de detaille
        /// </summary>
        public override void Close()
        {
            ConfirmClose();
            ViewNavigationService.Instance.Navigate(typeof(TPFListView));
        }

        #endregion

        #region PROPERTIES
        /// <summary>
        /// ID de l'tpf, 0 pour une creation, >0 pour un tpf existant
        /// </summary>
        public int TPFID
        {
            get => m_TPFID;
            set => Set(ref m_TPFID, value,bMarkAsModified:false);
        }
        private int m_TPFID;

        /// <summary>
        /// Information de l'tpf courant
        /// </summary>
        public TPF TPF
        {
            get => m_TPF;
            set => Set(ref m_TPF, value, bMarkAsModified: false);
        }
        private TPF m_TPF;

        public string Code
        {
            get => TPF.Code;
            set
            {
                if (value != TPF.Code)
                {
                    TPF.Code = value;
                    MarkAsModified();
                    NotifyChanges();
                }
            }
        }
        public string Name
        {
            get => TPF.Name;
            set
            {
                if (value != TPF.Name)
                {
                    TPF.Name = value;
                    MarkAsModified();
                    NotifyChanges();
                }
            }
        }
        public decimal Taux
        {
            get => TPF.Taux;
            set
            {
                if (value != TPF.Taux)
                {
                    TPF.Taux = value;
                    MarkAsModified();
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Le service de gestion des tpfs
        /// </summary>
        private TPFBS Service
        {
            get;set;
        }
        #endregion

        #region COMMAND
        #endregion
    }
}
