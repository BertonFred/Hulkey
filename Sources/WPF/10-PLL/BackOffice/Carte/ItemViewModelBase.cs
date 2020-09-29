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
    /// Item View model base
    /// classe de base pour les Items
    /// il s'agit d'une classe abstraire, qui peut être soit une Texte, soit un Produit
    /// un item est contenu par un ExpanderViewModel
    /// </summary>
    public abstract class ItemViewModelBase
        : ViewModelBase
    {
        public ItemViewModelBase()
        {
            this.Titre = "Item";
        }

        #region ACTIONS
        #endregion

        #region PROPERTIES
        /// <summary>
        /// ID du carte Element representée
        /// </summary>
        public int ID
        {
            get => m_ID;
            set => Set(ref m_ID, value);
        }
        private int m_ID;

        /// <summary>
        /// ID de la carte parent du carte Element 
        /// </summary>
        public int CarteID
        {
            get => m_CarteID;
            set => Set(ref m_CarteID, value);
        }
        private int m_CarteID;

        /// <summary>
        /// Id du carte element parent du carte element courant
        /// </summary>
        public int ParentID
        {
            get => m_ParentID;
            set => Set(ref m_ParentID, value);
        }
        private int m_ParentID;

        /// <summary>
        /// Ordre d'affichage par rapport au Carte Element Parent
        /// </summary>
        public int Ordre
        {
            get => m_Ordre;
            set => Set(ref m_Ordre, value);
        }
        private int m_Ordre;
        #endregion

        #region COMMAND
        #endregion
    }
}
