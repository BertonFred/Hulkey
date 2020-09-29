using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Hulkey.PLL.PresentationCommon
{
    /// <summary>
    /// Classe de base des ViewModel
    /// Apporte l'implementation de INotifyPropertyChanged
    /// Contient le Frame courant pour les fonction de gestion de navigation
    /// </summary>
    public class ViewModelBase : Observable
    {
        public Frame Frame { get; set; }
    }
}
