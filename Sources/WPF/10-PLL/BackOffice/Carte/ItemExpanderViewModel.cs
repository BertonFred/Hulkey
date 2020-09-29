using Hulkey.DAL.DTO;
using Hulkey.DAL.Entities;
using Hulkey.PLL.PresentationCommon;
using Hulkey.SLL.Services;
using Hulkey.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Hulkey.PLL.MVVM;
using MahApps.Metro.Controls.Dialogs;

namespace Hulkey.PLL.BackOffice
{
    /// <summary>
    /// Item Expander View model 
    /// Utilser par CarteViewModel pour represente les données d'un controle Expander
    /// Un ItemExpanderViewModel est la representation possible pour un CarteItem de type eCarteItem.Carte ou eCarteItem.Menu
    /// qui a comme parent eCarteItem.Carte ou eCarteItem.Menu
    /// </summary>
    public sealed class ItemExpanderViewModel
        : ItemViewModelBase
    {
        public ItemExpanderViewModel()
        {
            this.Titre = "Expander";
            this.IsExpanded = true;
        }


        #region ACTIONS
        #endregion

        #region PROPERTIES
        /// <summary>
        /// True si l'expander doit être ouvert
        /// </summary>
        public bool IsExpanded
        {
            get => m_IsExpanded;
            set => Set(ref m_IsExpanded, value,bMarkAsModified:false);
        }
        private bool m_IsExpanded;

        /// <summary>
        /// Liste des utilisateurs a afficher
        /// </summary>
        public ObservableCollection<ItemViewModelBase> Items { get; set; } = new ObservableCollection<ItemViewModelBase>();

        /// <summary>
        /// Indique la données selectionné dans la liste.
        /// null si pas de selection
        /// </summary>
        public ItemViewModelBase SelectedItem
        {
            get => m_SelectedItem;
            set => Set(ref m_SelectedItem, value, bMarkAsModified: false);
        }
        private ItemViewModelBase m_SelectedItem;
        #endregion

        #region COMMAND
        #endregion
    }
}
