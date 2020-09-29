using Hulkey.DAL;
using Hulkey.SLL.ServiceCommon;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Hulkey.PLL.MVVM
{
    /// <summary>
    /// Classe de base des ViewModel List
    /// Donc a utiliser pour les views qui affiche une liste d'information
    /// Apporte les notions de currentItem et Items
    /// </summary>
    public abstract class ListViewModel<T_SERVICE,T_DATA, T_DATALIST> 
        : ViewModelBase, IDataGridViewModel
        where T_SERVICE : IBusinessService<T_DATA, T_DATALIST>
        where T_DATA : IPersistentObject
        where T_DATALIST : IListItemDTO
    {
        public ListViewModel()
            : base()
        {
        }

        #region ACTIONS
        /// <summary>
        /// Navigation vers la home page de la liste
        /// </summary>
        public abstract void Home();

        /// <summary>
        /// Rechercher les données de la liste
        /// Si le texte de SeachText est present, il est utilisé comme critere de recherche
        /// </summary>
        public void Search()
        {
            string sSearchText = string.IsNullOrEmpty(SearchText) ? null : SearchText;
            List<T_DATALIST> lst = Service.GetList(sSearchText);
            this.Datas.Clear();
            lst.ForEach(item => this.Datas.Add(item));
        }

        /// <summary>
        /// Creation d'une nouvelle données
        /// et Affichage de la page de saisie détails
        /// </summary>
        public abstract void Create();

        /// <summary>
        /// Edition de la données courante.
        /// Pour entree en saisie SelectedData doit être different de null
        /// </summary>
        public abstract void Edit();

        /// <summary>
        /// Retourne true si commande Edit peut être executée
        /// </summary>
        public virtual bool CanEdit()
        {
            if (Datas.Count == 0) return false;
            if (SelectedData == null) return false;
            return true;
        }

        /// <summary>
        /// Suppression de la données courante.
        /// La suppression n'est pas possible que si SelectedData est different de null
        /// </summary>
        public async void Delete()
        {
            if (CanDelete() == false) return;

            var result = await MessageDialog.ShowAffirmativeAndNegative("Suppresion", "Confirmer la suppression ?");
            if (result == MessageDialogResult.Affirmative)
            {
                Service.DeleteById(SelectedData.ID);
                Search();
            }
        }

        /// <summary>
        /// Retourne true si commande Delete peut être executée
        /// </summary>
        public virtual bool CanDelete()
        {
            if (Datas.Count == 0) return false;
            if (SelectedData == null) return false;
            return true;
        }

        #endregion

        #region IDataGridViewModel
        /// <summary>
        /// lors du double clique sur la grid
        /// Reboucle sur le mode edit
        /// </summary>
        public void MouseDoubleClick(object SelectedItem, int iRow, int iCol)
        {
            if (SelectedItem != null)
                Edit();
        }
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Gets or sets the text for search.
        /// </summary>
        public string SearchText
        {
            get => m_SearchText;
            set => Set(ref m_SearchText, value, bMarkAsModified: false);
        }
        private string m_SearchText;

        /// <summary>
        /// Liste des utilisateurs a afficher
        /// </summary>
        public ObservableCollection<T_DATALIST> Datas { get; private set; } = new ObservableCollection<T_DATALIST>();

        /// <summary>
        /// Indique la données selectionné dans la liste.
        /// null si pas de selection
        /// </summary>
        public IListItemDTO SelectedData
        {
            get => m_SelectedData;
            set => Set(ref m_SelectedData, value, bMarkAsModified: false);
        }
        private IListItemDTO m_SelectedData;

        /// <summary>
        /// Le service de gestion des utilisateurs
        /// </summary>
        protected T_SERVICE Service
        {
            get;
            set;
        }
        #endregion

        #region COMMAND
        public ICommand HomeCmd
        {
            get
            {
                return m_HomeCmd ?? (m_HomeCmd = new RelayCommand(Home));
            }
        }
        private ICommand m_HomeCmd;
        public ICommand SearchCmd
        {
            get
            {
                return m_SearchCmd ?? (m_SearchCmd = new RelayCommand(Search));
            }
        }
        private ICommand m_SearchCmd;
        public ICommand CreateCmd
        {
            get
            {
                return m_CreateCmd ?? (m_CreateCmd = new RelayCommand(Create));
            }
        }
        private ICommand m_CreateCmd;
        public ICommand EditCmd
        {
            get
            {
                return m_EditCmd ?? (m_EditCmd = new RelayCommand(Edit,CanEdit));
            }
        }
        private ICommand m_EditCmd;
        public ICommand DeleteCmd
        {
            get
            {
                return m_DeleteCmd ?? (m_DeleteCmd = new RelayCommand(Delete,CanDelete));
            }
        }
        private ICommand m_DeleteCmd;
        #endregion
    }
}
