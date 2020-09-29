using System;


using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Hulkey.PLL.BackOffice
{
    public sealed partial class TPFMasterDetailsPage : Page
    {
        public TPFMasterDetailsViewModel ViewModel { get; } = new TPFMasterDetailsViewModel(); 

        public TPFMasterDetailsPage()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// Charger les informations de l'article
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.LoadTPFs();

            base.OnNavigatedTo(e);
        }
    }
}
