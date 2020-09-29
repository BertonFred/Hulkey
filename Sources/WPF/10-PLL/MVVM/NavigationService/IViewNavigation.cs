using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulkey.PLL.MVVM
{
    /// <summary>
    /// Gestion de la navigation entre view
    /// Implementation pour WPf des fonction de navigation de UWP (equivalent)
    /// </summary>
    public interface IViewNavigation 
    {
        /// <summary>
        /// Demande a la vue courant s'il elle auorise la navigation vers  une autre view
        /// </summary>
        /// <param name="ViewType">Le type de la vue ciblé par la navigation</param>
        /// <param name="parameter">Le parametre porté par la navigation, peut être null</param>
        /// <returns>La vue doit retournée true, si elle autorise la navigation</returns>
        bool CanNavigateTo(Type ViewType, object parameter);

        /// <summary>
        /// Indique à la vue que l'on viens de naviguer sur elle
        /// A ce moment la vue est crée, et elle est déjà la fenetre courante affiché
        /// C'est donc le momment ideale pour faire des initialisation dans le contenu de la vue
        /// </summary>
        /// <param name="parameter">Le aprametre porté par la navigation</param>
        void NavigateTo(object parameter);
    }

}
