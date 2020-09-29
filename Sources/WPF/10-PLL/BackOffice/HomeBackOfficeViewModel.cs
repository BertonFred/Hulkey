using Hulkey.PLL.MVVM;
using Hulkey.PLL.PresentationCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hulkey.PLL.BackOffice
{
    public sealed class HomeBackOfficeViewModel : ViewModelBase
    {
        #region ACTIONS
        public void NavigateToProduit()
        {
            ViewNavigationService.Instance.Navigate(typeof(ProduitListView));
        }
        public bool CanNavigateToProduit()
        {
            return true;
        }
        public void NavigateToCarte()
        {
            ViewNavigationService.Instance.Navigate(typeof(CarteView));
        }
        public bool CanNavigateToCarte()
        {
            return true;
        }
        public void NavigateToCategorie()
        {
            ViewNavigationService.Instance.Navigate(typeof(CategorieListView));
        }
        public bool CanNavigateToCategorie()
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
        public ICommand NavToProduitCmd
        {
            get
            {
                return m_NavToProduitCmd ?? (m_NavToProduitCmd = new RelayCommand(NavigateToProduit, CanNavigateToProduit));
            }
        }
        private ICommand m_NavToProduitCmd;
        public ICommand NavToCarteCmd
        {
            get
            {
                return m_NavToCarteCmd ?? (m_NavToCarteCmd = new RelayCommand(NavigateToCarte, CanNavigateToCarte));
            }
        }
        private ICommand m_NavToCarteCmd;
        public ICommand NavToCategorieCmd
        {
            get
            {
                return m_NavToCategorieCmd ?? (m_NavToCategorieCmd = new RelayCommand(NavigateToCategorie, CanNavigateToCategorie));
            }
        }
        private ICommand m_NavToCategorieCmd;

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
