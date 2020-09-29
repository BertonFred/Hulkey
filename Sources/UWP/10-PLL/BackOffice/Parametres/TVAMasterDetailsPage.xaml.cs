using System;


using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Hulkey.PLL.BackOffice
{
    public sealed partial class TVAMasterDetailsPage : Page
    {
        public TVAMasterDetailsViewModel ViewModel { get; } = new TVAMasterDetailsViewModel(); 

        public TVAMasterDetailsPage()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// Charger les informations de l'article
        /// </summary>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.LoadTVAListAsync();

            base.OnNavigatedTo(e);
        }
    }
}
