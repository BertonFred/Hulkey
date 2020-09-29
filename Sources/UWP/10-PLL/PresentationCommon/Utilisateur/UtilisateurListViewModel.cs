using Hulkey.DAL;
using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Hulkey.PLL.PresentationCommon;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Hulkey.DAL.DTO;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Hulkey.PLL.PresentationCommon
{
    /// <summary>
    /// ViewModel pour la page liste des Utilisateurs
    /// </summary>
    public class UtilisateurListViewModel : ViewModelBase
    {
        public UtilisateurListViewModel(Frame frame)
        {
            this.Frame = frame;
        }

        /// <summary>
        /// Modifier/Consulter un utilisateur
        /// </summary>
        public void Edit()
        {
            int iUtilisateurID = this.SelectedUtilisateur.ID;
            Frame.Navigate(typeof(UtilisateurDetailPage), iUtilisateurID, new DrillInNavigationTransitionInfo());
        }

        /// <summary>
        /// Creation d'un utilisateur
        /// </summary>
        public void Create()
        {
            int iUtilisateurID = 0;
            Frame.Navigate(typeof(UtilisateurDetailPage), iUtilisateurID, new DrillInNavigationTransitionInfo());
        }

        /// <summary>
        /// Suppression d'un utilisateur
        /// </summary>
        public void Delete()
        {
            int iUtilisateurID = 0;
            Frame.Navigate(typeof(UtilisateurDetailPage), iUtilisateurID, new DrillInNavigationTransitionInfo());
        }

        /// <summary>
        /// Rechercher des utilisateur
        /// </summary>
        public void Search()
        {
            //TODO gestion des criteres
            LoadUtilisateurs();
        }

        /// <summary>
        /// Charger la liste des utilisateurs
        /// </summary>
        public void LoadUtilisateurs()
        {
            List<UtilisateurListItemDTO> lst;
            this.Utilisateurs.Clear();

            using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
            {
                UtilisateurRepository repo = uow.GetRepositoryUtilisateur();
                lst = repo.GetList();
            }

            lst.ForEach(a => this.Utilisateurs.Add(a));
        }

        /// <summary>
        /// Gets the articles to display.
        /// </summary>
        public ObservableCollection<UtilisateurListItemDTO> Utilisateurs { get; private set; } = new ObservableCollection<UtilisateurListItemDTO>();

        /// <summary>
        /// Gets or sets the selected article.
        /// </summary>
        public UtilisateurListItemDTO SelectedUtilisateur
        {
            get => m_SelectedUtilisateur;
            set => Set(ref m_SelectedUtilisateur, value);
        }
        private UtilisateurListItemDTO m_SelectedUtilisateur;
    }
}
