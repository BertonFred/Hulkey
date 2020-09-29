using Hulkey.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulkey.PLL.MVVM
{
    /// <summary>
    /// Le view model de la ShellView
    /// </summary>
    public sealed class ShellViewModel : ViewModelBase
    {
        /// <summary>
        /// La view courante
        /// </summary>
        public object CurrentView
        {
            get => m_CurrentView;
            set => Set(ref m_CurrentView, value);
        }
        private object m_CurrentView;

        /// <summary>
        /// L'utilisateur courant
        /// </summary>
        public Utilisateur UtilisateurCourant
        {
            get => m_UtilisateurCourant;
            set
            { 
                Set(ref m_UtilisateurCourant, value);
                NotifyPropertyChanged("UserName");
            }
        }
        private Utilisateur m_UtilisateurCourant;

        /// <summary>
        /// Retourne l'utilisateur courant, null, si pas d'utilisateur courant
        /// </summary>
        public string UserName
        {
            get
            {
                return UtilisateurCourant == null ? "Inconnu" : UtilisateurCourant.DisplayName;
            }
        }
    }
}
