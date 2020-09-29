using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace Hulkey.PLL.MVVM
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : MetroWindow
    {
        public ShellWindow(ShellViewModel viewModel=null)
        {
            this.DataContext = viewModel ?? new ShellViewModel();
            InitializeComponent();
        }
        
        public ShellViewModel ViewModel 
        {
            get { return (ShellViewModel)this.DataContext; }
            set { this.DataContext = value; } 
        }
    }
}
