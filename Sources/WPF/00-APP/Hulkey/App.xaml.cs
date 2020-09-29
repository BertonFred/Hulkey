using Hulkey.Common;
using Hulkey.DAL.Repository;
using Hulkey.PLL.BackOffice;
using Hulkey.PLL.MVVM;
using Hulkey.PLL.PresentationCommon;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Hulkey
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //
        // Summary:
        //     Raises the System.Windows.Application.Startup event.
        //
        // Parameters:
        //   e:
        //     A System.Windows.StartupEventArgs that contains the event data.
        protected override void OnStartup(StartupEventArgs e)
        {
            Log.Info($"Hulkey Start");

            DatabaseHelpers.EnsureCreated();

            ApplicationContext.Instance.ShellView = new ShellWindow();
            ApplicationContext.Instance.ShellView.Width = 1024;
            ApplicationContext.Instance.ShellView.Height = 768;
            ApplicationContext.Instance.ShellView.Title = "Hulkey";
            ApplicationContext.Instance.ShellView.Show();

            //// MODE NORMALE LOGIN, puis la page de login anvois les la HomePage
            //// Navigation vers la page de login de l'application
            //ViewNavigationService.Instance.HomeView = new HomeView();
            //ViewNavigationService.Instance.Navigate(typeof(UtilisateurLoginView));

            // MODE DEVELOPPEUR, le user est définit en dure ici
            UserContext.Instance.ChangeUtilisateurCourant(1);
            ViewNavigationService.Instance.HomeView = new HomeView();
            ViewNavigationService.Instance.NavigateToHome();
        }

        //
        // Summary:
        //     Raises the System.Windows.Application.Exit event.
        //
        // Parameters:
        //   e:
        //     An System.Windows.ExitEventArgs that contains the event data.
        protected override void OnExit(ExitEventArgs e)
        {
            Log.Info($"Hulkey Stop");
        }
    }
}
