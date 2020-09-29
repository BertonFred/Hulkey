using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Hulkey.PLL.MVVM
{
    /// <summary>
    /// Classe de base pour les control utiliser comme des view
    /// Une vue est un UserControl WPF qui implemente IView
    /// </summary>
    public class ViewControl : UserControl, IView
    {
    }
}
