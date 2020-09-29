﻿using Hulkey.DAL.DTO;
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

namespace Hulkey.PLL.BackOffice
{
    /// <summary>
    /// View model pour le view Liste des tpfs
    /// Permet:
    /// * La Recherche
    /// * La Creation
    /// * La consultation ou mise a jour
    /// * La suppression
    /// </summary>
    public sealed class CategorieListViewModel
        : ListViewModel<CategorieBS, Categorie, CategorieListItemDTO>
    {
        public CategorieListViewModel()
            : base()
        {
            this.Titre = "Listes des Categories";
            this.Service = new CategorieBS(UserContext.Instance);
        }

        /// <summary>
        /// Indique à la vue que l'on viens de naviguer sur elle
        /// A ce moment la vue est crée, et elle est déjà la fenetre courante affiché
        /// C'est donc le momment ideale pour faire des initialisation dans le contenu de la vue
        /// </summary>
        /// <param name="parameter">Le aprametre porté par la navigation</param>
        public override void NavigateTo(object parameter)
        {
            Search();
        }

        #region ACTIONS
        /// <summary>
        /// Navigation vers la home page de la liste
        /// </summary>
        public override void Home()
        {
            ViewNavigationService.Instance.Navigate(typeof(HomeBackOfficeView));
        }

        /// <summary>
        /// Creation d'une nouvelle données
        /// et Affichage de la page de saisie détails
        /// </summary>
        public override void Create()
        {
            ViewNavigationService.Instance.Navigate(typeof(CategorieDetailView), 0);
        }

        /// <summary>
        /// Edition de la données courante.
        /// Pour entree en saisie SelectedData doit être different de null
        /// </summary>
        public override void Edit()
        {
            if (CanEdit() == false) return;

            ViewNavigationService.Instance.Navigate(typeof(CategorieDetailView), SelectedData.ID);
        }

        #endregion
    }
}