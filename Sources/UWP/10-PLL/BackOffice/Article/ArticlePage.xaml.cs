using System;


using Windows.UI.Xaml.Controls;

namespace Hulkey.PLL.BackOffice
{
    public sealed partial class ArticlePage : Page
    {
        public ArticleViewModel ViewModel { get; } = new ArticleViewModel();

        public ArticlePage()
        {
            InitializeComponent();
        }
    }
}
