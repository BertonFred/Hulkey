using System;


using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Hulkey.PLL.BackOffice
{
    public sealed partial class ArticleDetailPage : Page
    {
        public ArticleDetailViewModel ViewModel { get; } = new ArticleDetailViewModel(); 

        public ArticleDetailPage()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// Charger les informations de l'article
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int ArticleID = (int)e.Parameter;
            ViewModel.LoadArticle(ArticleID);
            ViewModel.LoadDependencies();

            base.OnNavigatedTo(e);
        }
    }
}
