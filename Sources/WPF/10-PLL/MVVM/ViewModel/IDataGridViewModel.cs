using Hulkey.DAL;
using Hulkey.SLL.ServiceCommon;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Hulkey.PLL.MVVM
{
    public interface IDataGridViewModel
    {
        void MouseDoubleClick(object SelectedItem, int iRow, int iCol);
    }
}
