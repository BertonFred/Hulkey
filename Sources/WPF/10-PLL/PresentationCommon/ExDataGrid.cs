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
    /// Surcharge du controle DataGrid pour faire le couplage avec le viewmodel
    /// ici juste le double clique
    /// TODO faire la gestion de la saisie en ligne
    /// </summary>
    public class ExDataGrid : DataGrid
    {
        public ExDataGrid()
        {
            this.MouseDoubleClick += ExDataGrid_MouseDoubleClick;
        }

        /// <summary>
        /// Gestion du double click 
        /// on renvois vers le double click du view mode
        /// </summary>
        private void ExDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int iCol = (this.CurrentColumn != null) ? this.CurrentColumn.DisplayIndex : -1;
            int iRow = this.SelectedIndex;
            object SelectedItem = this.SelectedItem;

            ViewModel.MouseDoubleClick(SelectedItem, iRow, iCol);
        }

        public IDataGridViewModel ViewModel
        {
            get 
            { 
                return this.DataContext as IDataGridViewModel; 
            }
        }

    }
}
