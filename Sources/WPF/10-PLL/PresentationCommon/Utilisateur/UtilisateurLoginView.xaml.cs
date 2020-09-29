using Hulkey.PLL.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hulkey.PLL.PresentationCommon
{
    /// <summary>
    /// Interaction logic for UtilisateurLoginView.xaml
    /// </summary>
    public partial class UtilisateurLoginView : ViewNavigationControl
    {
        public UtilisateurLoginView()
        {
            this.ViewModel = new UtilisateurLoginViewModel();
            InitializeComponent();
        
            this.txtPassword.Password = ViewModel.Password;
            this.DataContext = ViewModel;
        }
        /// <summary>
        /// Events bouton Click sur les boutons de 0..9
        /// ajouter la saisie un password courant
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int iKey = int.Parse(btn.Content.ToString());
            ViewModel.Key(iKey);
            this.txtPassword.Password = ViewModel.Password;
        }

        /// <summary>
        /// Events bouton OK
        /// Valider la saisie
        /// si oui on passer sur la home page
        /// </summary>
        private void Button_Ok_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Password = this.txtPassword.Password;
            if (ViewModel.IsValidPassword == true)
            {
                //Uri uri = new Uri("pack://application:,,,/HomePage.xaml");
                //this.NavigationService.Navigate(uri);
                ViewModel.Login();
            }
            else
            {
                ViewModel.Password = this.txtPassword.Password = null;
            }
        }

        /// <summary>
        /// Events bouton Back
        /// Supprimer la derniere saisie
        /// </summary>
        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.KeyBack();
            this.txtPassword.Password = ViewModel.Password;
        }

        /// <summary>
        /// ViewModel de la page
        /// </summary>
        public UtilisateurLoginViewModel ViewModel 
        { 
            get => (UtilisateurLoginViewModel)this.DataContext; 
            private set => this.DataContext = value; }
    }
}
