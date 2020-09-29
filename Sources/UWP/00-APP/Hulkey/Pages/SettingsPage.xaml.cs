using Hulkey.CreateDatabase;
using System;


using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Hulkey
{
    // TODO WTS: Change the URL for your privacy policy in the Resource File, currently set to https://YourPrivacyUrlGoesHere
    public sealed partial class SettingsPage : Page
    {
        public SettingsViewModel ViewModel { get; } = new SettingsViewModel();

        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Initialize();
        }

        private async void BtnDatabase_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            DatabaseDialog dlg = new DatabaseDialog();
            await dlg.ShowAsync();
        }
    }
}
