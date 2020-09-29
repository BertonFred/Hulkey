using Hulkey.DAL.DTO;
using Hulkey.DAL.Entities;
using Hulkey.PLL.PresentationCommon;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Hulkey.PLL.PresentationCommon
{
    public sealed partial class UtilisateurListPage : Page
    {
        public UtilisateurListViewModel ViewModel { get; private set; } 

        public UtilisateurListPage()
        {
            this.ViewModel = new UtilisateurListViewModel(this.Frame);

            InitializeComponent();
        }

        /// <summary>
        /// Retrieve the list of Utilisateurs when the user navigates to the page. 
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.ViewModel.Frame = this.Frame;

            ViewModel.LoadUtilisateurs();
        }

        /// <summary>
        /// Selects the tapped customer. 
        /// </summary>
        private void DataGrid_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ViewModel.SelectedUtilisateur = (e.OriginalSource as FrameworkElement).DataContext as UtilisateurListItemDTO;

            if (ViewModel.SelectedUtilisateur != null)
            {
                Frame.Navigate(typeof(UtilisateurDetailPage), ViewModel.SelectedUtilisateur.ID, new DrillInNavigationTransitionInfo());
            }
        }

        private void DataGrid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            ViewModel.SelectedUtilisateur = (e.OriginalSource as FrameworkElement).DataContext as UtilisateurListItemDTO;

            if (ViewModel.SelectedUtilisateur != null)
            {
                Frame.Navigate(typeof(UtilisateurDetailPage), ViewModel.SelectedUtilisateur.ID, new DrillInNavigationTransitionInfo());
            }
        }

        /// <summary>
        /// Sorts the data in the DataGrid.
        /// </summary>
        private void DataGrid_Sorting(object sender, DataGridColumnEventArgs e)
        {
            (sender as DataGrid).Sort(e.Column, ViewModel.Utilisateurs.Sort);
        }

    }
}
