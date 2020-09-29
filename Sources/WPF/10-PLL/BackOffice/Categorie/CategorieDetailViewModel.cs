using Hulkey.DAL.DTO;
using Hulkey.DAL.Entities;
using Hulkey.PLL.MVVM;
using Hulkey.PLL.PresentationCommon;
using Hulkey.SLL.Services;
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
using MessageDialog = Hulkey.PLL.MVVM.MessageDialog;

namespace Hulkey.PLL.BackOffice
{
    /// <summary>
    /// View model pour la page de detaille d'une categorie 
    /// </summary>
    public sealed class CategorieDetailViewModel : DetailViewModel
    {
        public CategorieDetailViewModel(int iCategorieID)
            : base()
        {
            this.Titre = "Categorie détail";
            this.Service = new CategorieBS(UserContext.Instance);
            this.SousCategorieService = new SousCategorieBS(UserContext.Instance, this.Service.uow);

            Load(iCategorieID);
        }

        #region ACTIONS
        /// <summary>
        /// Charge les données de la view de detaille
        /// </summary>
        public override void Load(int iCategorieID)
        {
            this.CategorieID = iCategorieID;
            if (this.CategorieID == 0)
            {
                // Mode Creation d'un categorie
                this.Categorie = new Categorie();
                this.MarkAsTransiant();
            }
            else 
            {
                // Mode Edit, d'un categorie existant d'un categorie existant
                this.Categorie = Service.Read(this.CategorieID);
                this.MarkAsPersistant();
            }

            LoadDependences();
            
            NotifyChanges();
        }

        /// <summary>
        /// Charge les données necessaire a l'affiche de la données courante
        /// par exemple les listes pour les combobox, ou les listes de données liée
        /// à la données courante
        /// </summary>
        protected override void LoadDependences()
        {
            this.SousCategories = new ObservableCollection<SousCategorie>(SousCategorieService.GetListForCategorie(this.CategorieID));
        }

        /// <summary>
        ///  Sauvegarde les modification de la view de detaille
        /// </summary>
        public override void Save()
        {
            if (this.PersistanteState == ePersistantState.Transiant)
            {
                // Mode Creation d'un categorie
                Service.Create(this.Categorie);
            }
            else
            {
                // Mode Edit d'un categorie existant
                Service.Update(this.Categorie);
            }
            this.MarkAsPersistant();

            ViewNavigationService.Instance.Navigate(typeof(CategorieListView));
        }

        /// <summary>
        ///  Ferme la vue de detaille
        /// </summary>
        public override void Close()
        {
            ConfirmClose();
            ViewNavigationService.Instance.Navigate(typeof(CategorieListView));
        }

        #endregion

        #region GESTION DES SOUS CATEGORIE
        /// <summary>
        /// Pointeur de fonction qui sera utiliser pour finaliser le mode edition d'une sous categorie
        /// </summary>
        private Action FinalizeEdition { get; set; }

        /// <summary>
        /// Activation du flyout edition en mode
        /// Ajouter une sous categorie au début de la liste des sous categorie
        /// </summary>
        public void SousCategorieAddAbove()
        {
            FinalizeEdition = FinalizeEditionForSousCategorieAddAbove;
            this.SousCategorieName = null;
            this.SousCategorieDescription = null;
            IsOpenFlyoutSousCategorie = true;
        }

        /// <summary>
        /// Ajouter une sous categorie au début de la liste des sous categorie de la categorie
        /// </summary>
        public void FinalizeEditionForSousCategorieAddAbove()
        {
            // Réordonné la liste des sous-categorie pour pouvoir ajouter au début
            int iNewOrder = 20;
            foreach (SousCategorie subCateg in this.SousCategories)
            {
                subCateg.Ordre = iNewOrder;
                this.SousCategorieService.Update(subCateg);
                iNewOrder += 10;
            }

            // ajouter au debut
            SousCategorie newSubCateg = new SousCategorie()
            {
                CategorieID = this.CategorieID,
                Name = this.SousCategorieName,
                Description = this.SousCategorieDescription,
                Ordre = 10,
            };
            this.SousCategories.Insert(0, newSubCateg);
            this.SousCategorieService.Create(newSubCateg, bDoSaveChange: false);

            MarkAsModified();
        }

        /// <summary>
        /// Activation du flyout edition en mode
        /// Ajouter une sous categorie à la place de la sous categorie selectionné dans la liste des
        /// sous categorie de la categorie
        /// </summary>
        public void SousCategorieAdd()
        {
            FinalizeEdition = FinalizeEditionForSousCategorieAdd;
            this.SousCategorieName = null;
            this.SousCategorieDescription = null;
            IsOpenFlyoutSousCategorie = true;
        }

        /// <summary>
        /// Ajouter une sous categorie à la place de la sous categorie selectionné dans la liste des
        /// sous categorie de la categorie
        /// </summary>
        public void FinalizeEditionForSousCategorieAdd()
        {
            // ajouter la nouvelle composition, a la place de la composition selectionné
            int iPosToInsert = this.SousCategories.IndexOf(this.SelectedSousCategorie);
            SousCategorie subCategToCreate = new SousCategorie()
            {
                CategorieID = this.CategorieID,
                Name = this.SousCategorieName,
                Description = this.SousCategorieDescription,
                Ordre = this.SelectedSousCategorie.Ordre,
            };
            this.SousCategories.Insert(iPosToInsert, subCategToCreate);
            this.SousCategorieService.Create(subCategToCreate, bDoSaveChange: false);

            // reordonnées les compostions apres la position d'insertion
            int iNewOrder = subCategToCreate.Ordre + 10;
            for (int iPos = iPosToInsert + 1; iPos < this.SousCategories.Count; iPos++)
            {
                SousCategorie subCategToUpdate = this.SousCategories[iPos];
                subCategToUpdate.Ordre = iNewOrder;
                iNewOrder += 10;
                this.SousCategorieService.Update(subCategToUpdate);
            }

            MarkAsModified();
        }

        /// <summary>
        /// On peut ajouter une sous categorie a la place d'une sous categorie
        /// s'il existe une sous categorie de selectionné
        /// </summary>
        public bool SousCategorieCanAdd()
        {
            return this.SelectedSousCategorie != null;
        }

        /// <summary>
        /// Activation du flyout edition en mode
        /// Ajouter une sous categorie à la fin dans la liste des
        /// sous categorie de la categorie
        /// </summary>
        public void SousCategorieAddBelow()
        {
            FinalizeEdition = FinalizeEditionForSousCategorieAddBelow;
            this.SousCategorieName = null;
            this.SousCategorieDescription = null;
            IsOpenFlyoutSousCategorie = true;
        }

        /// <summary>
        /// Ajouter une sous categorie à la fin dans la liste des
        /// sous categorie de la categorie
        /// </summary>
        public void FinalizeEditionForSousCategorieAddBelow()
        {
            // recuperer la postion du derniere elements de la liste des sous categories
            SousCategorie lastSubCateg = this.SousCategories.LastOrDefault();
            int iLaFinOrder = (lastSubCateg != null) ? lastSubCateg.Ordre + 10 : 10;

            // ajouter a la fin
            SousCategorie subCategToAdd = new SousCategorie()
            {
                CategorieID = this.CategorieID,
                Name = this.SousCategorieName,
                Description = this.SousCategorieDescription,
                Ordre = iLaFinOrder,
            };
            this.SousCategorieService.Create(subCategToAdd, bDoSaveChange: false);
            this.SousCategories.Add(subCategToAdd);

            MarkAsModified();
        }

        /// <summary>
        /// Activation du flyout edition en mode
        /// Modifier la sous categorie selectionnée
        /// </summary>
        public void SousCategorieUpdate()
        {
            FinalizeEdition = FinalizeEditionForSousCategorieUpdate;

            if (SelectedSousCategorie != null)
            {
                this.SousCategorieName = SelectedSousCategorie.Name;
                this.SousCategorieDescription = SelectedSousCategorie.Description;
            }

            this.IsOpenFlyoutSousCategorie = true;
        }

        /// <summary>
        /// Modifier la sous categorie selectionnée
        /// </summary>
        public void FinalizeEditionForSousCategorieUpdate()
        {
            this.SelectedSousCategorie.Name = this.SousCategorieName;
            this.SelectedSousCategorie.Description = this.SousCategorieDescription;

            this.SousCategorieService.Update(this.SelectedSousCategorie,bDoSaveChange:false);

            MarkAsModified();
        }

        /// <summary>
        /// On peut modifié une sous categorie, s'il y en a une de selectionnée
        /// </summary>
        public bool SousCategorieCanUpdate()
        {
            return this.SelectedSousCategorie != null;
        }

        /// <summary>
        /// Supprimer une sous categorie de la liste des sous categorie de la categorie
        /// c'est la sous categorie selectionné qui est supprimée
        /// </summary>
        public void SousCategorieDelete()
        {
            this.SousCategorieService.DeleteById(this.SelectedSousCategorie.ID);
            this.SousCategories.Remove(this.SelectedSousCategorie);
            this.SelectedSousCategorie = null;
        }

        /// <summary>
        /// On peut supprimé une composistion s'il existe une composition de selectionné
        /// </summary>
        public bool SousCategorieCanDelete()
        {
            return this.SelectedSousCategorie != null;
        }

        /// <summary>
        /// Valider le mode edition
        /// Appelle la fonction de finalisation de la saisie en fonction
        /// de ce qui est mis en place via la fonction FinlazieEdition
        /// Le flyout est egalement fermé
        /// </summary>
        public void SousCategorieValid()
        {
            FinalizeEdition();
            FinalizeEdition = null;
            this.IsOpenFlyoutSousCategorie = false;
        }

        /// <summary>
        /// On peut valider le mode edition, 
        /// si la flyout d'edition est ouverte
        /// </summary>
        public bool SousCategorieCanValid()
        {
            return this.IsOpenFlyoutSousCategorie == true &&
                    string.IsNullOrEmpty(this.SousCategorieName) == false &&
                    string.IsNullOrEmpty(this.SousCategorieDescription) == false ;
        }

        /// <summary>
        /// Abandonner le mode edition
        /// Le flyout est egalement fermé
        /// </summary>
        public void SousCategorieCancel()
        {
            FinalizeEdition = null;
            this.IsOpenFlyoutSousCategorie = false;
        }

        /// <summary>
        /// On peut Abandonner le mode edition, 
        /// si la flyout d'edition est ouverte
        /// </summary>
        public bool SousCategorieCanCancel()
        {
            return this.IsOpenFlyoutSousCategorie == true;
        }
        #endregion

        #region PROPERTIES
        /// <summary>
        /// ID de l'categorie, 0 pour une creation, >0 pour un categorie existant
        /// </summary>
        public int CategorieID
        {
            get => m_CategorieID;
            set => Set(ref m_CategorieID, value,bMarkAsModified:false);
        }
        private int m_CategorieID;

        /// <summary>
        /// Information de l'categorie courant
        /// </summary>
        public Categorie Categorie
        {
            get => m_Categorie;
            set => Set(ref m_Categorie, value, bMarkAsModified: false);
        }
        private Categorie m_Categorie;

        public string Name
        {
            get => Categorie.Name;
            set
            {
                if (value != Categorie.Name)
                {
                    Categorie.Name = value;
                    MarkAsModified();
                    NotifyChanges();
                }
            }
        }
        public string Description
        {
            get => Categorie.Description;
            set
            {
                if (value != Categorie.Description)
                {
                    Categorie.Description = value;
                    MarkAsModified();
                    NotifyChanges();
                }
            }
        }
        public int Ordre
        {
            get => Categorie.Ordre;
            set
            {
                if (value != Categorie.Ordre)
                {
                    Categorie.Ordre = value;
                    MarkAsModified();
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Nom pour une sous categorie en cours de creation ou d'edition
        /// </summary>
        public string SousCategorieName
        {
            get => m_SousCategorieName;
            set => Set(ref m_SousCategorieName, value, bMarkAsModified: false);
        }
        private string m_SousCategorieName;

        /// <summary>
        /// Description pour une sous categorie en cours de creation ou d'edition
        /// </summary>
        public string SousCategorieDescription
        {
            get => m_SousCategorieDescription;
            set => Set(ref m_SousCategorieDescription, value, bMarkAsModified: false);
        }
        private string m_SousCategorieDescription;

        /// <summary>
        /// True si le Flyout permettant la saisie des données d'une sous categorie
        /// </summary>
        public bool IsOpenFlyoutSousCategorie
        {
            get => m_IsOpenFlyoutSousCategorie;
            set => Set(ref m_IsOpenFlyoutSousCategorie, value, bMarkAsModified: false);
        }
        private bool m_IsOpenFlyoutSousCategorie;

        /// <summary>
        /// Liste des sous categories de la categorie
        /// </summary>
        public ObservableCollection<SousCategorie> SousCategories
        {
            get => m_SousCategories;
            set => Set(ref m_SousCategories, value, bMarkAsModified: false);
        }
        private ObservableCollection<SousCategorie> m_SousCategories;

        /// <summary>
        /// La sous-categorie selectionné dans la liste des sous-categories
        /// </summary>
        public SousCategorie SelectedSousCategorie
        {
            get => m_SelectedSousCategorie;
            set =>Set(ref m_SelectedSousCategorie, value, bMarkAsModified: false);
        }
        private SousCategorie m_SelectedSousCategorie;

        /// <summary>
        /// Le service de gestion des categories
        /// </summary>
        private CategorieBS Service
        {
            get;set;
        }

        /// <summary>
        /// Le service de sous categorie
        /// </summary>
        private SousCategorieBS SousCategorieService
        {
            get; set;
        }
        #endregion

        #region COMMAND
        public ICommand SousCategorieAddAboveCmd
        {
            get
            {
                return m_SousCategorieAddAboveCmd ?? (m_SousCategorieAddAboveCmd = new RelayCommand(SousCategorieAddAbove));
            }
        }
        private ICommand m_SousCategorieAddAboveCmd;
        public ICommand SousCategorieAddCmd
        {
            get
            {
                return m_SousCategorieAddCmd ?? (m_SousCategorieAddCmd = new RelayCommand(SousCategorieAdd, SousCategorieCanAdd));
            }
        }
        private ICommand m_SousCategorieAddCmd;
        public ICommand SousCategorieAddBelowCmd
        {
            get
            {
                return m_SousCategorieAddBelowCmd ?? (m_SousCategorieAddBelowCmd = new RelayCommand(SousCategorieAddBelow));
            }
        }
        private ICommand m_SousCategorieAddBelowCmd;
        public ICommand SousCategorieUpdateCmd
        {
            get
            {
                return m_SousCategorieUpdateCmd ?? (m_SousCategorieUpdateCmd = new RelayCommand(SousCategorieUpdate, SousCategorieCanUpdate));
            }
        }
        private ICommand m_SousCategorieUpdateCmd;
        public ICommand SousCategorieDeleteCmd
        {
            get
            {
                return m_SousCategorieDeleteCmd ?? (m_SousCategorieDeleteCmd = new RelayCommand(SousCategorieDelete, SousCategorieCanDelete));
            }
        }
        private ICommand m_SousCategorieDeleteCmd;

        public ICommand SousCategorieValidCmd
        {
            get
            {
                return m_SousCategorieValidCmd ?? (m_SousCategorieValidCmd = new RelayCommand(SousCategorieValid, SousCategorieCanValid));
            }
        }
        private ICommand m_SousCategorieValidCmd;
        public ICommand SousCategorieCancelCmd
        {
            get
            {
                return m_SousCategorieCancelCmd ?? (m_SousCategorieCancelCmd = new RelayCommand(SousCategorieCancel, SousCategorieCanCancel));
            }
        }
        private ICommand m_SousCategorieCancelCmd;
        #endregion
    }
}
