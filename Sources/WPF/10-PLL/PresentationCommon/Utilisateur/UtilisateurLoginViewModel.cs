using Hulkey.DAL.DTO;
using Hulkey.PLL.MVVM;
using Hulkey.PLL.PresentationCommon;
using Hulkey.SLL.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Hulkey.PLL.PresentationCommon
{
    /// <summary>
    /// View model pour la page de selection d'un utilisateur et de saisie du mot de pass
    /// </summary>
    public sealed class UtilisateurLoginViewModel : ViewModelBase
    {
        public UtilisateurLoginViewModel()
            : base()
        {
            LoadUtilisateurs();
        }

        /// <summary>
        /// Charger la liste des utilisateurs
        /// </summary>
        public void LoadUtilisateurs()
        {
            this.Utilisateurs = new ObservableCollection<UtilisateurListItemDTO>(Service.GetList());
        }

        /// <summary>
        /// Effacer la derniere saisie
        /// </summary>
        public void KeyBack()
        {
            if (Password.Length == 0) return;
            if (Password.Length == 1)
            {
                Password = null;
                return;
            }

            Password = Password.Substring(0, Password.Length - 1);
        }

        /// <summary>
        /// Ajouter la touche utilisé au password courant 
        /// La touche est entre 0 et 9
        /// </summary>
        public void Key(int iKey)
        {
            string sKey = iKey.ToString(); ;
            Password = Password + sKey;
        }

        /// <summary>
        /// Login de l'utilisateur
        /// </summary>
        public void Login()
        {
            UserContext.Instance.ChangeUtilisateurCourant(SelectedUtilisateur.ID);
            ViewNavigationService.Instance.NavigateToHome();
        }

        /// <summary>
        /// Liste des utilisateurs a afficher
        /// </summary>
        public ObservableCollection<UtilisateurListItemDTO> Utilisateurs { get; private set; } = new ObservableCollection<UtilisateurListItemDTO>();

        /// <summary>
        /// Gets or sets the selected utilisateur.
        /// </summary>
        public UtilisateurListItemDTO SelectedUtilisateur
        {
            get => m_SelectedUtilisateur;
            set => Set(ref m_SelectedUtilisateur, value);
        }
        private UtilisateurListItemDTO m_SelectedUtilisateur;

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password
        {
            get => m_Password;
            set => Set(ref m_Password, value);
        }
        private string m_Password;

        public bool IsValidPassword
        {
            get 
            {
                if (SelectedUtilisateur == null)
                    return false;
                return Service.CheckPassword(SelectedUtilisateur.ID, this.Password);
            }
        }

        /// <summary>
        /// Le service de gestion des utilisateurs
        /// </summary>
        private UtilisateurBS Service
        {
            get
            {
                if (m_Service == null)
                    this.m_Service = new UtilisateurBS(UserContext.Instance);
                return m_Service;
            }
        }
        private UtilisateurBS m_Service;
    }
}
