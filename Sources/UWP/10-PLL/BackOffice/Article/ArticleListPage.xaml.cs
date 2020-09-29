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

namespace Hulkey.PLL.BackOffice
{
    public sealed partial class ArticleListPage : Page
    {
        public ArticleListViewModel ViewModel { get; } = new ArticleListViewModel();

        public ArticleListPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Retrieve the list of articles when the user navigates to the page. 
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.LoadArticles();
        }

        /// <summary>
        /// Selects the tapped customer. 
        /// </summary>
        private void DataGrid_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ViewModel.SelectedArticle = (e.OriginalSource as FrameworkElement).DataContext as ProduitListItemDTO;

            if (ViewModel.SelectedArticle != null)
            {
                Frame.Navigate(typeof(ArticleDetailPage), ViewModel.SelectedArticle.ID, new DrillInNavigationTransitionInfo());
            }
        }
        private void DataGrid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            ViewModel.SelectedArticle = (e.OriginalSource as FrameworkElement).DataContext as ProduitListItemDTO;

            if (ViewModel.SelectedArticle != null)
            {
                Frame.Navigate(typeof(ArticleDetailPage), ViewModel.SelectedArticle.ID, new DrillInNavigationTransitionInfo());
            }
        }

        /// <summary>
        /// Sorts the data in the DataGrid.
        /// </summary>
        private void DataGrid_Sorting(object sender, DataGridColumnEventArgs e)
        {
            (sender as DataGrid).Sort(e.Column, ViewModel.Articles.Sort);
        }

    }
}
