using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Hulkey.PLL.PresentationCommon
{
    public sealed partial class UtilisateurDetailPage : Page
    {
        public UtilisateurDetailViewModel ViewModel { get; private set; }

        public UtilisateurDetailPage()
        {
            this.ViewModel = new UtilisateurDetailViewModel();

            InitializeComponent();
        }

        /// <summary>
        /// Adjust the command bar button label positions for optimimum viewing.
        /// </summary>
        private void CommandBar_Loaded(object sender, RoutedEventArgs e)
        {
            //if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            //{
            //    (sender as CommandBar).DefaultLabelPosition = CommandBarDefaultLabelPosition.Bottom;
            //}
            //else
            //{
            //    (sender as CommandBar).DefaultLabelPosition = CommandBarDefaultLabelPosition.Right;
            //}

            // Disable dynamic overflow on this page. There are only a few commands, and it causes
            // layout problems when save and cancel commands are shown and hidden.
            //(sender as CommandBar).IsDynamicOverflowEnabled = false;
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// Charger les informations de l'Utilisateur
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.ViewModel.Frame = this.Frame;
            int UtilisateurID = (int)e.Parameter;
            ViewModel.LoadUtilisateur(UtilisateurID);
            ViewModel.LoadDependencies();

            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Check whether there are unsaved changes and warn the user.
        /// </summary>
        protected /*async*/ override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (ViewModel.IsModified)
            {
                // Cancel the navigation immediately, otherwise it will continue at the await call. 
                e.Cancel = true;

                void resumeNavigation()
                {
                    if (e.NavigationMode == NavigationMode.Back)
                    {
                        Frame.GoBack();
                    }
                    else
                    {
                        Frame.Navigate(e.SourcePageType, e.Parameter, e.NavigationTransitionInfo);
                    }
                }

                //var saveDialog = new SaveChangesDialog() { Title = $"Save changes?" };
                //await saveDialog.ShowAsync();
                //SaveChangesDialogResult result = saveDialog.Result;

                //switch (result)
                //{
                //    case SaveChangesDialogResult.Save:
                //        await ViewModel.SaveAsync();
                //        resumeNavigation();
                //        break;
                //    case SaveChangesDialogResult.DontSave:
                //        await ViewModel.RevertChangesAsync();
                //        resumeNavigation();
                //        break;
                //    case SaveChangesDialogResult.Cancel:
                //        break;
                //}
            }

            base.OnNavigatingFrom(e);
        }


    }
}
