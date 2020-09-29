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

namespace Hulkey.PLL.BackOffice
{
    /// <summary>
    /// Interaction logic for ProduitDetailView.xaml
    /// </summary>
    public partial class ProduitDetailView : ViewNavigationControl
    {
        public ProduitDetailView()
        {
            this.DataContext = new ProduitDetailViewModel(0);
            InitializeComponent();
        }

        public ProduitDetailView(int iProduitID)
        {
            this.DataContext = new ProduitDetailViewModel(iProduitID);
            InitializeComponent();
        }
    }
}
