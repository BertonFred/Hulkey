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
    /// TabItem View model 
    /// Utilser par CarteViewModel pour represente les données d'un controle TabItem
    /// </summary>
    public sealed class ItemTabViewModel
        : ItemViewModelBase
    {
        public ItemTabViewModel()
        {
            this.Titre = "TabItem";
        }

        #region ACTIONS
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Type du carte Element representée
        /// Pour un TabItem il s'agit soit d'un : Titre ou Menu
        /// 
        /// </summary>
        public eCarteItem CarteElementType
        {
            get => m_CarteElementType;
            set => Set(ref m_CarteElementType, value);
        }
        private eCarteItem m_CarteElementType;

        /// <summary>
        /// Liste des expander associé au tabitem
        /// </summary>
        public ObservableCollection<ItemExpanderViewModel> Expanders { get; set; } = new ObservableCollection<ItemExpanderViewModel>();

        /// <summary>
        /// Indique la données selectionné dans la liste.
        /// null si pas de selection
        /// </summary>
        public ItemExpanderViewModel SelectedExpander
        {
            get => m_SelectedExpander;
            set => Set(ref m_SelectedExpander, value, bMarkAsModified: false);
        }
        private ItemExpanderViewModel m_SelectedExpander;

        #endregion

        #region COMMAND
        #endregion
    }
}
