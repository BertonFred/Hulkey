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
    /// Interaction logic for CarteView.xaml
    /// </summary>
    public partial class CarteView : ViewNavigationControl
    {
        public CarteView()
        {
            this.DataContext = new CarteViewModel();
            InitializeComponent();
        }
    }
}
