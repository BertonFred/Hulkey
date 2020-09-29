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
    /// avec support de la gestion de la navigation
    /// Une vue est un UserControl WPF qui implemente IViewNavigation et IView
    /// </summary>
    public class ViewNavigationControl : UserControl, IViewNavigation, IView
    {
        /// <summary>
        /// Demande a la vue courant s'il elle auorise la navigation vers  une autre view
        /// </summary>
        /// <param name="ViewType">Le type de la vue ciblé par la navigation</param>
        /// <param name="parameter">Le parametre porté par la navigation, peut être null</param>
        /// <returns>La vue doit retournée true, si elle autorise la navigation</returns>
        public virtual bool CanNavigateTo(Type ViewType, object parameter)
        {
            ViewModelBase viewModel = (ViewModelBase)this.DataContext;
            return viewModel.CanNavigateTo(ViewType, parameter);
        }

        /// <summary>
        /// Indique à la vue que l'on viens de naviguer sur elle
        /// A ce moment la vue est crée, et elle est déjà la fenetre courante affiché
        /// C'est donc le momment ideale pour faire des initialisation dans le contenu de la vue
        /// </summary>
        /// <param name="parameter">Le aprametre porté par la navigation</param>
        public virtual void NavigateTo(object parameter)
        {
            ViewModelBase viewModel = (ViewModelBase)this.DataContext;
            viewModel.NavigateTo(parameter);
        }
    }
}
