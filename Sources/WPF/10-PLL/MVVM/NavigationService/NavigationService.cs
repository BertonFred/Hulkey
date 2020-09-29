using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulkey.PLL.MVVM
{

    /// <summary>
    /// La ShellWindow affiche une View courante dans son Content
    /// Le navigation service permet de géré la navigation des View qui sont affichées 
    /// dans la ShellWindow.
    /// Le contenu de la ShellWindow peut être soit :
    /// * Une IViewNavigation, c'est à dire un controle qui implemente l'interface IViewNavigation
    ///     ce qui devrait être le cas de toutes les View
    /// * Un Control, c'est à dire un controle WPF qui n'implemente pas IViewNavigation
    /// Lorsque la navigation ce fait vers ou depuis une view qui implemente IViewNavigation
    /// L'interface est solicitée pour la gestion de la navigation
    /// </summary>
    public class ViewNavigationService
    {
        private ViewNavigationService()
        { 
            // Singleton
        }
        /// <summary>
        /// Retourne l'instance du ViewNavigationService qui est un singleton
        /// </summary>
        public static ViewNavigationService Instance
        {
            get
            {
                return m_Instance ?? (m_Instance = new ViewNavigationService());
            }
        }
        private static ViewNavigationService m_Instance;

        /// <summary>
        /// Demande la navigation vers la home de l'application
        /// </summary>
        public void NavigateToHome()
        {
            Navigate(HomeView, null);
        }

        /// <summary>
        /// Demande la navigation vers une View indiqué par ton type
        /// La view sera instancié par la service de navigation
        /// </summary>
        /// <param name="ViewType">Le type de la view vers laquelle ont souhaite navigué</param>
        public void Navigate(Type ViewType)
        {
            Navigate(ViewType, null);
        }

        /// <summary>
        /// Demande la navigation vers une View indiqué par ton type
        /// La view sera instancié par la service de navigation
        /// Les parametre d'initialisation de la view peuvent être passé par Parameter.
        /// Ce parametre est transmis à la view par la methode 
        /// NavigateTo de l'interface IViewNavigation que la view doit implementé.
        /// Si la vue n'implemente pas IViewNavigation, le parametre n'est pas passé
        /// </summary>
        /// <param name="ViewType">Le type de la view vers laquelle ont souhaite navigué</param>
        /// <param name="parameter">un objet qui contient le ou les parametre de la view. 
        /// </param>
        public void Navigate(Type ViewType, object parameter)
        {
            // Verifier si la view courante autorise la navigation vers une autre page
            // Le content de la shellView c'est la view courante
            if (ApplicationContext.Instance.ShellView.Content != null)
            {
                IViewNavigation viewNav = CurrentView as IViewNavigation;
                if (viewNav != null)
                {
                    // La view courante supporte la gestion de la navigation
                    bool bCanNavigate = viewNav.CanNavigateTo(ViewType, parameter);
                    if (bCanNavigate == false)
                        return;  // La View refuse la navigation
                }
            }

            // création d'instance de la view cible de la navigation
            IView ViewToGo;
            if (parameter == null)
                ViewToGo = (IView)Activator.CreateInstance(ViewType);
            else
                ViewToGo = (IView)Activator.CreateInstance(ViewType,parameter);

            // remplacement du contenu de la fenêtre principale de l'application 
            CurrentView = ViewToGo;

            // passer le parametre de navigation à la nouvelle view courante
            // si elle supporte la navigation
            IViewNavigation ViewNav = CurrentView as IViewNavigation;
            if (ViewNav != null)
            {
                // La view courante supporte la gestion de la navigation
                ViewNav.NavigateTo(parameter);
            }
        }

        public void Navigate(IView view, object parameter)
        {
            // Verifier si la view courante autorise la navigation vers une autre page
            if (ApplicationContext.Instance.ShellView.Content != null)
            {
                IViewNavigation viewNav = CurrentView as IViewNavigation;
                if (viewNav != null)
                {
                    // La view courante supporte la gestion de la navigation
                    bool bCanNavigate = viewNav.CanNavigateTo(view.GetType(), parameter);
                    if (bCanNavigate == false)
                        return;  // La View refuse la navigation
                }
            }

            // création d'instance de la view cible de la navigation
            IView ViewToGo = (IView)view;

            // remplacement du contenu de la fenêtre principale de l'application 
            CurrentView = ViewToGo;

            // passer le parametre de navigation à la nouvelle view courante
            // si elle supporte la navigation
            IViewNavigation ViewNav = CurrentView as IViewNavigation;
            if (ViewNav != null)
            {
                // La view courante supporte la gestion de la navigation
                ViewNav.NavigateTo(parameter);
            }
        }

        /// <summary>
        /// Retourne la view courante affichée dans le ShellWindow, c'est à dire le Content 
        /// de la fenetre principale
        /// </summary>
        public IView CurrentView
        {
            get => (IView)ApplicationContext.Instance.ShellViewModel.CurrentView;
            set => ApplicationContext.Instance.ShellViewModel.CurrentView = value;
        }

        /// <summary>
        /// Pointeur sur la fenetre Home de l'application
        /// </summary>
        public IView HomeView { get; set; }
    }
}
