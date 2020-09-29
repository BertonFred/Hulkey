using Hulkey.Common;
using Hulkey.DAL.Repository;
using Hulkey.PLL.PresentationCommon;
using System;


using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace Hulkey
{
    public sealed partial class App : Application
    {
        private Lazy<ActivationService> _activationService;

        public ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            // Init log path
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            NLog.LogManager.Configuration.Variables["LogPath"] = storageFolder.Path;

            Log.Info("Démarrage de l'application");

            InitializeComponent();

            // $$$ pas sur que ce soit la qu'il faut faire ça
            DatabaseHelpers.EnsureCreated();

            // Deferred execution until used.
            // Check https://msdn.microsoft.com/library/dd642331(v=vs.110).aspx for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (args.PrelaunchActivated == false)
            {
                await ActivationService.ActivateAsync(args);
            }
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        public ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(PLL.BackOffice.HomePage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new ShellPage();
        }
    }
}
