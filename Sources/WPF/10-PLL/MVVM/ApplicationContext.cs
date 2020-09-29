using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulkey.PLL.MVVM
{
    /// <summary>
    /// Contexte globale de l'application
    /// Permet de partager des informations global, comme un tableau blanc :-)
    /// SINGLETON
    /// </summary>
    public sealed class ApplicationContext : Observable
    {
        private ApplicationContext()
        {
        }

        /// <summary>
        /// Retourne l'instance singleton de l'Application contexte
        /// </summary>
        public static ApplicationContext Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new ApplicationContext();
                return m_Instance;
            }
        }
        private static ApplicationContext m_Instance;

        /// <summary>
        /// La fenetre principal de l'application
        /// MainWindow
        /// </summary>
        public ShellWindow ShellView 
        { 
            get => m_ShellView;
            set
            {
                m_ShellView = value;
                ShellViewModel = m_ShellView.ViewModel;
            } 
        }
        private ShellWindow m_ShellView;

        /// <summary>
        /// ViewModel de la fenetre de shell de l'application
        /// </summary>
        public ShellViewModel ShellViewModel { get; private set; }
    }
}
