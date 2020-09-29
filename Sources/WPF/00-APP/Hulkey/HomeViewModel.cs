using Hulkey.PLL.Administration;
using Hulkey.PLL.BackOffice;
using Hulkey.PLL.MVVM;
using Hulkey.PLL.PresentationCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hulkey
{
    public sealed class HomeViewModel : ViewModelBase
    {
        #region ACTIONS
        public void NavigateToCaisse()
        {
            // $$$ TODO
        }
        public bool CanNavigateToCaisse()
        {
            return true;
        }
        public void NavigateToBackOffice()
        {
            ViewNavigationService.Instance.Navigate(typeof(HomeBackOfficeView));
        }
        public bool CanNavigateToBackOffice()
        {
            return true;
        }
        public void NavigateToAdministration()
        {
            ViewNavigationService.Instance.Navigate(typeof(HomeAdministrationView));
        }
        public bool CanNavigateToAdministration()
        {
            return true;
        }
        #endregion

        #region COMMAND
        public ICommand NavToCaisseCmd
        {
            get
            {
                return m_NavToCaisseCmd ?? (m_NavToCaisseCmd = new RelayCommand(NavigateToCaisse, CanNavigateToCaisse));
            }
        }
        private ICommand m_NavToCaisseCmd;
        public ICommand NavToBackOfficeCmd
        {
            get
            {
                return m_NavToBackOfficeCmd ?? (m_NavToBackOfficeCmd = new RelayCommand(NavigateToBackOffice, CanNavigateToBackOffice));
            }
        }
        private ICommand m_NavToBackOfficeCmd;
        public ICommand NavToAdministrationCmd
        {
            get
            {
                return m_NavToAdministrationCmd ?? (m_NavToAdministrationCmd = new RelayCommand(NavigateToAdministration, CanNavigateToAdministration));
            }
        }
        private ICommand m_NavToAdministrationCmd;
        #endregion
    }
}
