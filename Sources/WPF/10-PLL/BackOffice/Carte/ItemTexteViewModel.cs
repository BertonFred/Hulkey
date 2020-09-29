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
    /// Item View model 
    /// Utilser par CarteViewModel pour represente les données d'un controle Item
    /// </summary>
    public sealed class ItemTexteViewModel
        : ItemViewModelBase
    {
        public ItemTexteViewModel()
        {
        }

        public ItemTexteViewModel(string texte)
        {
            this.Titre = texte;
        }

        #region ACTIONS
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Texte a afficher par l'item Texte synonyme de titre
        /// </summary>
        public string Texte
        {
            get => Titre;
            set
            {
                Titre = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region COMMAND
        #endregion
    }
}
