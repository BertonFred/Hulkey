using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulkey.PLL.MVVM
{
    /// <summary>
    /// Interface representant une View
    /// Pour "simplifie" Les controles que l'on va utilisé ont tous un DataContext
    /// Du coup lorsque qu'il seront utiliser comme des View, ils devront derivé de l'interface
    /// IView qui sera implementée par défaut au niveau du controle de base WPF
    /// Cette astuce, permettra tout de même d'identifié un controle WPF qui est utilisé comme une view
    /// </summary>
    public interface IView
    {
        object DataContext { get; set; }
    }
}
