using Hulkey.DAL.DTO;
using Hulkey.DAL.Entities;
using Hulkey.PLL.PresentationCommon;
using Hulkey.SLL.Services;
using Hulkey.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Hulkey.PLL.MVVM;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;

namespace Hulkey.PLL.BackOffice
{
    /// <summary>
    /// View model pour le view Liste des tpfs
    /// Permet:
    /// * La Recherche
    /// * La Creation
    /// * La consultation ou mise a jour
    /// * La suppression
    /// </summary>
    public sealed class CarteViewModel
        : ViewModelBase
    {
        public CarteViewModel()
            : base()
        {
            this.Titre = "Listes des Cartes";
            this.Service = new CarteBS(UserContext.Instance);
            this.TabControlVisibility = Visibility.Hidden;
            this.LoadCarteVisibility = Visibility.Hidden;
        }

        /// <summary>
        /// Indique à la vue que l'on viens de naviguer sur elle
        /// A ce moment la vue est crée, et elle est déjà la fenetre courante affiché
        /// C'est donc le momment ideale pour faire des initialisation dans le contenu de la vue
        /// </summary>
        /// <param name="parameter">Le aprametre porté par la navigation</param>
        public override void NavigateTo(object parameter)
        {
            SearchCarte();
        }

        /// <summary>
        /// Navigation vers la home page BackOffice
        /// </summary>
        public void Home()
        {
            ViewNavigationService.Instance.Navigate(typeof(HomeBackOfficeView));
        }

        #region ACTIONS : CARTE
        /// <summary>
        /// Rechercher les données de la liste
        /// Si le texte de SeachText est present, il est utilisé comme critere de recherche
        /// </summary>
        public void SearchCarte()
        {
            List<CarteListItemDTO> lst = Service.GetList();
            this.Cartes.Clear();
            lst.ForEach(item => this.Cartes.Add(item));

            this.SelectedCarte = this.Cartes.FirstOrDefault();
        }

        /// <summary>
        /// Creation d'une nouvelle données
        /// et Affichage de la page de saisie détails
        /// </summary>
        public void CreateCarte()
        {
            this.CarteName = null;
            this.CarteActiveCaisse = true;
            if (this.Cartes.Count > 0)
                this.CarteOrdre = this.Cartes[this.Cartes.Count-1].Ordre + 10;
            else
                this.CarteOrdre = 10;
            this.FinalizeEditionCarte = FinalizeEditionCarteForCreate;

            this.IsOpenFlyoutCarte = true;
        }

        /// <summary>
        /// Finalize le mode edition pour une carte
        /// creation en base et mise ajour de la liste des cartes
        /// </summary>
        public void FinalizeEditionCarteForCreate()
        {
            Carte carteToCreate = new Carte();
            carteToCreate.Name = this.CarteName;
            carteToCreate.ActiveCaisse = this.CarteActiveCaisse;
            carteToCreate.Ordre = this.CarteOrdre;
            Service.Create(carteToCreate);
        }

        /// <summary>
        /// Edition de la données courante.
        /// Pour entree en saisie SelectedData doit être different de null
        /// </summary>
        public void EditCarte()
        {
            if (CanEditCarte() == false) return;

            this.CarteName = this.SelectedCarte.Name;
            this.CarteActiveCaisse = this.SelectedCarte.ActiveCaisse;
            this.CarteOrdre = this.SelectedCarte.Ordre;
            this.FinalizeEditionCarte = FinalizeEditionCarteForEdit;

            this.IsOpenFlyoutCarte = true;
        }

        /// <summary>
        /// Retourne true si commande Edit peut être executée
        /// </summary>
        public bool CanEditCarte()
        {
            if (Cartes.Count == 0) return false;
            if (SelectedCarte == null) return false;
            return true;
        }

        /// <summary>
        /// Finalize le mode edition pour une carte
        /// update en base et mise a jour de la liste des cartes
        /// </summary>
        public void FinalizeEditionCarteForEdit()
        {
            Carte carteToUpdate = Service.Read(this.SelectedCarte.ID);
            carteToUpdate.Name = this.CarteName;
            carteToUpdate.ActiveCaisse = this.CarteActiveCaisse;
            carteToUpdate.Ordre = this.CarteOrdre;
            Service.Update(carteToUpdate);
        }

        /// <summary>
        /// Valide la saisie des informations Carte en mode saisie
        /// </summary>
        public void CarteValid()
        {
            this.FinalizeEditionCarte();
            this.FinalizeEditionCarte = null;
            this.SearchCarte();
            this.IsOpenFlyoutCarte = false;
        }

        /// <summary>
        /// Annuler la saisie des informations Carte en mode saisie
        /// </summary>
        public void CarteCancel()
        {
            this.FinalizeEditionCarte = null;
            this.SearchCarte();
            this.IsOpenFlyoutCarte = false;
        }

        /// <summary>
        /// Suppression de la données courante.
        /// La suppression n'est pas possible que si SelectedData est different de null
        /// </summary>
        public async void DeleteCarte()
        {
            if (CanDeleteCarte() == false) return;

            var result = await MVVM.MessageDialog.ShowAffirmativeAndNegative("Suppression", "Confirmer la suppression ?");
            if (result == MessageDialogResult.Affirmative)
            {
                Service.DeleteById(SelectedCarte.ID);
                SearchCarte();
            }
        }

        /// <summary>
        /// Retourne true si commande Delete peut être executée
        /// </summary>
        public  bool CanDeleteCarte()
        {
            if (Cartes.Count == 0) return false;
            if (SelectedCarte == null) return false;
            return true;
        }
        #endregion

        #region PROPERTIES : CARTE
        /// <summary>
        /// Liste des Cartes a afficher
        /// </summary>
        public ObservableCollection<CarteListItemDTO> Cartes { get; private set; } = new ObservableCollection<CarteListItemDTO>();

        /// <summary>
        /// Indique la données selectionné dans la liste des cartes.
        /// null si pas de selection
        /// </summary>
        public CarteListItemDTO SelectedCarte
        {
            get => m_SelectedCarte;
            set
            {
                Set(ref m_SelectedCarte, value, bMarkAsModified: false);

                if (this.TabItems.Count == 0)
                {
                    // s'il n'y a pas de carte items de charger
                    // il faut gerer l'affichage du bouton de chargement des items pour la carte de selectionné
                    if (m_SelectedCarte != null)
                        LoadCarteVisibility = Visibility.Visible; // pas de chargement si pas de selection
                    else
                        LoadCarteVisibility = Visibility.Hidden; // chargment possible si selection
                }
            }
        }
        private CarteListItemDTO m_SelectedCarte;

        /// <summary>
        /// En mode edition: Nom de la carte (creation ou d'edition)
        /// </summary>
        public string CarteName
        {
            get => m_CarteName;
            set => Set(ref m_CarteName, value, bMarkAsModified: false);
        }
        private string m_CarteName;

        /// <summary>
        /// En mode edition: Active pour l'affichage sur la caisse (creation ou d'edition)
        /// </summary>
        public bool CarteActiveCaisse
        {
            get => m_CarteActiveCaisse;
            set => Set(ref m_CarteActiveCaisse, value, bMarkAsModified: false);
        }
        private bool m_CarteActiveCaisse;

        /// <summary>
        /// En mode edition: Ordre d'affichage dans la liste des carte (creation ou d'edition)
        /// </summary>
        public int CarteOrdre
        {
            get => m_CarteOrdre;
            set => Set(ref m_CarteOrdre, value, bMarkAsModified: false);
        }
        private int m_CarteOrdre;

        /// <summary>
        /// Pointeur de fonction qui sera utiliser pour finaliser le mode edition d'une Carte
        /// </summary>
        private Action FinalizeEditionCarte { get; set; }

        /// <summary>
        /// Active le Flyout de saisie des parametre de la caisse
        /// </summary>
        public bool IsOpenFlyoutCarte
        {
            get => m_IsOpenFlyoutCarte;
            set => Set(ref m_IsOpenFlyoutCarte, value, bMarkAsModified: false);
        }
        private bool m_IsOpenFlyoutCarte;

        /// <summary>
        /// Le service de gestion des utilisateurs
        /// </summary>
        private CarteBS Service
        {
            get;
            set;
        }
        #endregion

        #region ACTIONS : CARTE ITEMS
        /// <summary>
        /// Charger les elements qui compose la carte
        /// </summary>
        public void LoadCarteItems()
        {
            if (CanLoadCarteItems() == false) return;

            this.TabItems.Clear();

            // todo
            this.TabItems.Add(new ItemTabViewModel() 
            { 
                Titre = "Tab item 1", 
                CarteID = this.SelectedCarte.ID, 
                Ordre = 10,
                CarteElementType=eCarteItem.Texte,
                Expanders=new ObservableCollection<ItemExpanderViewModel>()
                {
                    new ItemExpanderViewModel()
                    {
                        CarteID=this.SelectedCarte.ID,
                        Titre="Expander 1.1",
                        Ordre=10,
                        Items = new ObservableCollection<ItemViewModelBase>()
                        {
                            new ItemTexteViewModel(){Titre="item text 1"},
                            new ItemTexteViewModel(){Titre="item text 2"},
                            new ItemTexteViewModel(){Titre="item text 3"},
                            new ItemTexteViewModel(){Titre="item text 4"}
                        }
                    },
                    new ItemExpanderViewModel()
                    {
                        CarteID=this.SelectedCarte.ID,
                        Titre="Expander 1.2",
                        Ordre=20,
                        Items = new ObservableCollection<ItemViewModelBase>()
                        {
                            new ItemTexteViewModel(){Titre="item text 1"},
                            new ItemTexteViewModel(){Titre="item text 2"},
                            new ItemProduitViewModel(){ProduitID=1},
                            new ItemProduitViewModel(){ProduitID=2}
                        }
                    }
                }
            });

            this.TabItems.Add(new ItemTabViewModel() 
            { 
                Titre = "Tab item 2", 
                CarteID = this.SelectedCarte.ID, 
                Ordre = 20,
                CarteElementType = eCarteItem.Texte,
                Expanders = new ObservableCollection<ItemExpanderViewModel>()
                {
                    new ItemExpanderViewModel()
                    {
                        CarteID=this.SelectedCarte.ID,
                        Titre="Expander 2.1",
                        Ordre=10
                    },
                    new ItemExpanderViewModel()
                    {
                        CarteID=this.SelectedCarte.ID,
                        Titre="Expander 2.2",
                        Ordre=10
                    }
                }
            });

            // activation des controles
            if ((this.TabItems.Count > 0))
            {
                // s'il y a des elements dans la carte, il faut afficher le tabcontrol et caché le boutons de chargement de carte item
                this.LoadCarteVisibility = Visibility.Hidden ;
                this.TabControlVisibility = Visibility.Visible;
            }
            else
            {
                // s'il n'y a pas d'elements dans la carte, il faut caché le tabcontrol et affiché le boutons de chargement de carte item
                this.LoadCarteVisibility = Visibility.Visible ;
                this.TabControlVisibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Retourne true si commande load carte peut être executée
        /// </summary>
        public bool CanLoadCarteItems()
        {
            if (Cartes.Count == 0) return false;
            if (SelectedCarte == null) return false;
            return true;
        }

        /// <summary>
        /// Sauver les elements qui compose la carte
        /// </summary>
        public void SaveCarteItems()
        {
            if (CanSaveCarteItems() == false) return;

            // todo

        }

        /// <summary>
        /// Retourne true si commande save carte items peut être executée
        /// </summary>
        public bool CanSaveCarteItems()
        {
            return this.State == eState.Modified;
        }

        /// <summary>
        /// fermer la zone d'affichage du contenu de la carte
        /// </summary>
        public void CloseCarteItems()
        {
            this.TabItems.Clear();
            this.TabControlVisibility = Visibility.Hidden;
            this.SelectedCarte = SelectedCarte; // declenché la gestion de l'affichage du bouton de chargement de carte elements
        }
        #endregion

        #region PROPERTIES TABITEMS (CARTE ITEMS)
        /// <summary>
        /// Indique la vibilité du bouton de chargement d'une contenu d'une carte
        /// </summary>
        public Visibility TabControlVisibility
        {
            get => m_TabControlVisibility;
            set => Set(ref m_TabControlVisibility, value, bMarkAsModified: false);
        }
        private Visibility m_TabControlVisibility;

        /// <summary>
        /// Indique la vibilité du bouton de chargement d'une contenu d'une carte
        /// </summary>
        public Visibility LoadCarteVisibility
        {
            get => m_LoadCarteVisibility;
            set => Set(ref m_LoadCarteVisibility, value, bMarkAsModified: false);
        }
        private Visibility m_LoadCarteVisibility;

        /// <summary>
        /// Liste des TabItems
        /// </summary>
        public ObservableCollection<ItemTabViewModel> TabItems { get; private set; } = new ObservableCollection<ItemTabViewModel>();

        /// <summary>
        /// Indique la données selectionné dans la liste des TabItems.
        /// null si pas de selection
        /// </summary>
        public ItemTabViewModel SelectedTabItem
        {
            get => m_SelectedTabItem;
            set => Set(ref m_SelectedTabItem, value, bMarkAsModified: false);
        }
        private ItemTabViewModel m_SelectedTabItem;
        #endregion

        #region ACTIONS : CARTE ITEM 
        /// <summary>
        /// 
        /// </summary>
        public void CarteItemCreate(object obj)
        {
            // $$$ todo
        }
        public bool CanCarteItemCreate(object obj)
        {
            // $$$ todo
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CarteItemDelete(object obj)
        {
            // $$$ todo
        }
        public bool CanCarteItemDelete(object obj)
        {
            // $$$ todo
            return true;
        }
        #endregion

        #region COMMAND CARTE
        public ICommand HomeCmd
        {
            get
            {
                return m_HomeCmd ?? (m_HomeCmd = new RelayCommand(Home));
            }
        }
        private ICommand m_HomeCmd;
        public ICommand CreateCarteCmd
        {
            get
            {
                return m_CreateCarteCmd ?? (m_CreateCarteCmd = new RelayCommand(CreateCarte));
            }
        }
        private ICommand m_CreateCarteCmd;
        public ICommand EditCarteCmd
        {
            get
            {
                return m_EditCarteCmd ?? (m_EditCarteCmd = new RelayCommand(EditCarte, CanEditCarte));
            }
        }
        private ICommand m_EditCarteCmd;
        public ICommand DeleteCarteCmd
        {
            get
            {
                return m_DeleteCarteCmd ?? (m_DeleteCarteCmd = new RelayCommand(DeleteCarte, CanDeleteCarte));
            }
        }
        private ICommand m_DeleteCarteCmd;
        public ICommand CarteValidCmd
        {
            get
            {
                return m_CarteValidCmd ?? (m_CarteValidCmd = new RelayCommand(CarteValid));
            }
        }
        private ICommand m_CarteValidCmd;
        public ICommand CarteCancelCmd
        {
            get
            {
                return m_CarteCancelCmd ?? (m_CarteCancelCmd = new RelayCommand(CarteCancel));
            }
        }
        private ICommand m_CarteCancelCmd;
        #endregion

        #region COMMANDE CARTE ITEMS
        public ICommand LoadCarteItemsCmd
        {
            get
            {
                return m_LoadCarteItemsCmd ?? (m_LoadCarteItemsCmd = new RelayCommand(LoadCarteItems, CanLoadCarteItems));
            }
        }
        private ICommand m_LoadCarteItemsCmd;
        public ICommand CloseCarteItemsCmd
        {
            get
            {
                return m_CloseCarteItemsCmd ?? (m_CloseCarteItemsCmd = new RelayCommand(CloseCarteItems));
            }
        }
        private ICommand m_CloseCarteItemsCmd;
        public ICommand SaveCarteItemsCmd
        {
            get
            {
                return m_SaveCarteItemsCmd ?? (m_SaveCarteItemsCmd = new RelayCommand(SaveCarteItems, CanSaveCarteItems));
            }
        }
        private ICommand m_SaveCarteItemsCmd;
        #endregion

        #region COMMAND CARTE ITEM
        public ICommand CarteItemCreateCmd
        {
            get
            {
                return m_CarteItemCreateCmd ?? (m_CarteItemCreateCmd = new RelayCommand<object>(CarteItemCreate));
            }
        }
        private ICommand m_CarteItemCreateCmd;
        public ICommand CarteItemDeleteCmd
        {
            get
            {
                return m_CarteItemDeleteCmd ?? (m_CarteItemDeleteCmd = new RelayCommand<object>(CarteItemDelete));
            }
        }
        private ICommand m_CarteItemDeleteCmd;
        #endregion
    }
}
