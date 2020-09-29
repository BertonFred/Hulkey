using Hulkey.PLL.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hulkey.PLL.Administration
{
    /// <summary>
    /// View model de la home page des service d'administration
    /// </summary>
    public sealed class HomeAdministrationViewModel : ViewModelBase
    {
        #region ACTIONS
        public void NavigateToUtilisateur()
        {
            ViewNavigationService.Instance.Navigate(typeof(UtilisateurListView));
        }

        public bool CanNavigateToUtilisateur()
        {
            return true;
        }

        public void NavigateToTVA()
        {
            ViewNavigationService.Instance.Navigate(typeof(TVAListView));
        }

        public bool CanNavigateToTVA()
        {
            return true;
        }
        public void NavigateToTPF()
        {
            ViewNavigationService.Instance.Navigate(typeof(TPFListView));
        }

        public bool CanNavigateToTPF()
        {
            return true;
        }

        public void NavigateToTypePaiement()
        {
            ViewNavigationService.Instance.Navigate(typeof(TypePaiementListView));
        }
        public bool CanNavigateToTypePaiement()
        {
            return true;
        }
        public void NavigateToHome()
        {
            ViewNavigationService.Instance.NavigateToHome();
        }
        public bool CanNavigateToHome()
        {
            return true;
        }
        #endregion

        #region COMMAND
        public ICommand NavToUtilisateurCmd
        {
            get
            {
                return m_NavToUtilisateurCmd ?? (m_NavToUtilisateurCmd = new RelayCommand(NavigateToUtilisateur, CanNavigateToUtilisateur));
            }
        }
        private ICommand m_NavToUtilisateurCmd;
        public ICommand NavToTVACmd
        {
            get
            {
                return m_NavToTVACmd ?? (m_NavToTVACmd = new RelayCommand(NavigateToTVA, CanNavigateToTVA));
            }
        }
        private ICommand m_NavToTVACmd;
        public ICommand NavToTPFCmd
        {
            get
            {
                return m_NavToTPFCmd ?? (m_NavToTPFCmd = new RelayCommand(NavigateToTPF, CanNavigateToTPF));
            }
        }
        private ICommand m_NavToTPFCmd;
        public ICommand NavToTypePaiementCmd
        {
            get
            {
                return m_NavToTypePaiementCmd ?? (m_NavToTypePaiementCmd = new RelayCommand(NavigateToTypePaiement, CanNavigateToTypePaiement));
            }
        }
        private ICommand m_NavToTypePaiementCmd;
        public ICommand NavToHomeCmd
        {
            get
            {
                return m_NavToHomeCmd ?? (m_NavToHomeCmd = new RelayCommand(NavigateToHome, CanNavigateToHome));
            }
        }
        private ICommand m_NavToHomeCmd;
        #endregion
    }
}
