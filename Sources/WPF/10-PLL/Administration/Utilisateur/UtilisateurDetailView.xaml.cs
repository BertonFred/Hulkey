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

namespace Hulkey.PLL.Administration
{
    /// <summary>
    /// Interaction logic for UtilisateurDetailView.xaml
    /// </summary>
    public partial class UtilisateurDetailView : ViewNavigationControl
    {
        public UtilisateurDetailView()
        {
            this.DataContext = new UtilisateurDetailViewModel(0);
            InitializeComponent();
        }

        public UtilisateurDetailView(int iUtilisateurID)
        {
            this.DataContext = new UtilisateurDetailViewModel(iUtilisateurID);
            InitializeComponent();
        }
    }
}
