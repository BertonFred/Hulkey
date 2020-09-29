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
    /// View model pour la page de detaille d'un produit 
    /// </summary>
    public sealed class ProduitDetailViewModel : DetailViewModel
    {
        public ProduitDetailViewModel(int iProduitID)
            : base()
        {
            this.Titre = "Produit détail";
            this.Service = new ProduitBS(UserContext.Instance);
            this.TvaService = new TVABS(UserContext.Instance, this.Service.uow);
            this.CategorieService = new CategorieBS(UserContext.Instance, this.Service.uow);
            this.SousCategorieService = new SousCategorieBS(UserContext.Instance, this.Service.uow);

            Load(iProduitID);
        }

        #region ACTIONS COMMUNES à un view detail
        /// <summary>
        /// Chargement de la donnée affiché par la view
        /// Le chargement est a prendre au sens chargement de la vue
        /// donc la données peut être issu de la base ou être une données transient
        /// </summary>
        public override void Load(int iProduitID)
        {
            this.ProduitID = iProduitID;
            if (this.ProduitID == 0)
            {
                // Mode Creation d'un tva
                this.Produit = new Produit();
                this.MarkAsTransiant();
            }
            else
            {
                // Mode Edit, d'un tva existant d'un tva existant
                this.Produit = Service.Read(this.ProduitID);
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
            this.TVAs = TvaService.GetList();
            this.SelectedTVA = this.TVAs.FirstOrDefault(t => t.ID == TVAID);

            this.Categories = CategorieService.GetList();
            this.SelectedCategorie = this.CategorieID != null ? this.Categories.FirstOrDefault(t => t.ID == CategorieID) : null;
            this.SelectedSousCategorie = this.SousCategories != null ? this.SousCategories.FirstOrDefault(t => t.ID == SousCategorieID) : null;

            this.ProduitCompositions = new ObservableCollection<ProduitComposition>(this.Service.GetListProduitComposition(this.ProduitID));
        }

        /// <summary>
        ///  Sauvegarde les modification de la view de detaille
        /// </summary>
        public override void Save()
        {
            if (this.PersistanteState == ePersistantState.Transiant)
            {
                // Mode Creation d'un tva
                Service.Create(this.Produit);
            }
            else
            {
                // Mode Edit d'un tva existant
                Service.Update(this.Produit);
            }
            this.MarkAsPersistant();

            ViewNavigationService.Instance.Navigate(typeof(ProduitListView));
        }

        /// <summary>
        ///  Ferme la vue de detaille
        /// </summary>
        public override void Close()
        {
            ConfirmClose();
            ViewNavigationService.Instance.Navigate(typeof(ProduitListView));
        }
        #endregion

        #region GESTION DES PRIX
        /// <summary>
        /// Calculer le HT depuis le TTC
        /// Prix HT = Prix TTC *100/(100+Taux)
        /// </summary>
        public void ComputeHT()
        {
            decimal value = this.PrixVenteTTC * 100m / (100m + SelectedTVA.Taux);
            this.PrixVenteHT = Math.Round(value, 2);
        }

        /// <summary>
        /// Cacluler le TTC depuis le HT
        /// </summary>
        public void ComputeTTC()
        {
            decimal value = this.PrixVenteHT / 100m * (100m + SelectedTVA.Taux);
            this.PrixVenteTTC = Math.Round(value,2);
        }
        #endregion

        #region GESTION DE LA COMPOSITION
        /// <summary>
        /// Recherche des produits pour la liste des produits 
        /// dans la section produit composée
        /// </summary>
        public void ProduitSearch()
        {
            this.Produits = Service.GetListAsProduit(this.ProduitSearchText);
        }

        /// <summary>
        /// Ajouter a produit a la composition en début de composition
        /// </summary>
        public void CompositionAddAbove()
        {
            // Réordonné la composition pour pouvoir ajouter au début
            int iNewOrder = 20;
            foreach(ProduitComposition compo in this.ProduitCompositions)
            {
                compo.Ordre = iNewOrder;
                this.Service.UpdateProduitComposition(compo);
                iNewOrder += 10;
            }

            // ajouter au debut
            ProduitComposition newCompo = new ProduitComposition()
            {
                ParentID = this.ProduitID,
                EnfantID = this.SelectedProduit.ID,
                Enfant = this.SelectedProduit,
                Ordre = 10,
                Quantite = 1,
            };
            this.Service.AddProduitComposition(newCompo);
            this.ProduitCompositions.Insert(0, newCompo);

            MarkAsModified();
        }

        /// <summary>
        /// On peut ajouter une composistion en début, si un produit a ajouté est selectionné
        /// </summary>
        public bool CompositionCanAddAbove()
        {
            return this.SelectedProduit != null;
        }

        /// <summary>
        /// Ajouter un produit a la composition
        /// le produit est ajoutée a la place du produit selectionnée dans la composition
        /// </summary>
        public void CompositionAdd()
        {
            // ajouter la nouvelle composition, a la place de la composition selectionné
            int iPosToInsert = this.ProduitCompositions.IndexOf(this.SelectedProduitComposition);
            ProduitComposition compoToAdd = new ProduitComposition()
            {
                ParentID = this.ProduitID,
                EnfantID = this.SelectedProduit.ID,
                Enfant = this.SelectedProduit,
                Ordre = this.SelectedProduitComposition.Ordre,
                Quantite = 1,
            };
            this.ProduitCompositions.Insert(iPosToInsert, compoToAdd);
            this.Service.AddProduitComposition(compoToAdd);

            // reordonnées les compostions apres la position d'insertion
            int iNewOrder = compoToAdd.Ordre + 10;
            for(int iPos=iPosToInsert+1; iPos<this.ProduitCompositions.Count; iPos++)
            {
                ProduitComposition compo = this.ProduitCompositions[iPos];
                compo.Ordre = iNewOrder;
                iNewOrder += 10;
                this.Service.UpdateProduitComposition(compo);
            }

            MarkAsModified();
        }

        /// <summary>
        /// On peut ajouter une composistion a la place d'une composition
        /// s'il existe une composition de selectionné
        /// </summary>
        public bool CompositionCanAdd()
        {
            return this.SelectedProduitComposition != null && this.SelectedProduit != null;
        }

        /// <summary>
        /// Ajouter a produit a la composition en fin de composition
        /// </summary>
        public void CompositionAddBelow()
        {
            // recuperer la postion du derniere elements de la composition
            ProduitComposition lastCompo = this.ProduitCompositions.LastOrDefault();
            int iLaFinOrder = (lastCompo != null) ? lastCompo.Ordre + 10 : 10;

            // ajouter a la fin
            ProduitComposition newCompo = new ProduitComposition()
            {
                ParentID = this.ProduitID,
                EnfantID = this.SelectedProduit.ID,
                Enfant = this.SelectedProduit,
                Ordre = iLaFinOrder,
                Quantite = 1,
            };
            this.Service.AddProduitComposition(newCompo);
            this.ProduitCompositions.Add(newCompo);

            MarkAsModified();
        }

        /// <summary>
        /// On peut ajouter une composition en fin, si un produit a ajouté est selectionné
        /// </summary>
        public bool CompositionCanAddBelow()
        {
            return this.SelectedProduit != null;
        }

        /// <summary>
        /// Supprimer un composant dans un produit composée
        /// c'est la composition selectionné qui est supprimée
        /// </summary>
        public void CompositionDelete()
        {
            this.Service.DeleteProduitComposition(this.SelectedProduitComposition);
            this.ProduitCompositions.Remove(this.SelectedProduitComposition);
            this.SelectedProduitComposition = null;
        }
        
        /// <summary>
        /// On peut supprimé une composistion s'il existe une composition de selectionné
        /// </summary>
        public bool CompositionCanDelete()
        {
            return this.SelectedProduitComposition != null;
        }
        #endregion

        #region PROPERTIES
        /// <summary>
        /// ID de la données, 0 pour une creation, >0 pour un tva existant
        /// </summary>
        public int ProduitID
        {
            get => m_ProduitID;
            set => Set(ref m_ProduitID, value, bMarkAsModified: false);
        }
        private int m_ProduitID;

        /// <summary>
        /// Information de la données courante
        /// </summary>
        public Produit Produit
        {
            get => m_Produit;
            set => Set(ref m_Produit, value, bMarkAsModified: false);
        }
        private Produit m_Produit;

        public string Name
        {
            get => Produit.Name;
            set
            {
                if (value != Produit.Name)
                {
                    Produit.Name = value;
                    MarkAsModified();
                    NotifyChanges();
                }
            }
        }
        public string Description
        {
            get => Produit.Description;
            set
            {
                if (value != Produit.Description)
                {
                    Produit.Description = value;
                    MarkAsModified();
                    NotifyChanges();
                }
            }
        }

        public decimal PrixVenteHT
        {
            get => Produit.PrixVenteHT;
            set
            {
                if (value != Produit.PrixVenteHT)
                {
                    Produit.PrixVenteHT = value;
                    MarkAsModified();
                    NotifyPropertyChanged();
                }
            }
        }

        public int TVAID
        {
            get => Produit.TVAID;
            set
            {
                if (value != Produit.TVAID)
                {
                    Produit.TVAID = value;
                    MarkAsModified();
                    NotifyPropertyChanged();
                }
            }
        }
        public TVAListItemDTO SelectedTVA
        {
            get => m_SelectedTVA;
            set
            {
                Set(ref m_SelectedTVA, value, bMarkAsModified: false);
                TVAID = m_SelectedTVA.ID;
            } 
        }
        private TVAListItemDTO m_SelectedTVA;

        public List<TVAListItemDTO> TVAs
        {
            get => m_TVAs;
            set => Set(ref m_TVAs, value, bMarkAsModified: false);
        }
        private List<TVAListItemDTO> m_TVAs;

        public decimal PrixVenteTTC
        {
            get => Produit.PrixVenteTTC;
            set
            {
                if (value != Produit.PrixVenteTTC)
                {
                    Produit.PrixVenteTTC = value;
                    MarkAsModified();
                    NotifyPropertyChanged();
                }
            }
        }

        public int? CategorieID
        {
            get => Produit.CategorieID;
            set
            {
                if (value != Produit.CategorieID)
                {
                    Produit.CategorieID = value;
                    MarkAsModified();
                    NotifyPropertyChanged();
                }
            }
        }

        public CategorieListItemDTO SelectedCategorie
        {
            get => m_SelectedCategorie;
            set
            {
                if (m_SelectedCategorie != value)
                {
                    Set(ref m_SelectedCategorie, value, bMarkAsModified: false);
                    if (m_SelectedCategorie == null)
                        CategorieID = null;
                    else
                        CategorieID = m_SelectedCategorie.ID;

                    LoadSousCategorie();
                }
            }
        }
        private CategorieListItemDTO m_SelectedCategorie;

        public List<CategorieListItemDTO> Categories
        {
            get => m_Categories;
            set => Set(ref m_Categories, value, bMarkAsModified: false);
        }
        private List<CategorieListItemDTO> m_Categories;

        public int? SousCategorieID
        {
            get => Produit.SousCategorieID;
            set
            {
                if (value != Produit.SousCategorieID)
                {
                    Produit.SousCategorieID = value;
                    MarkAsModified();
                    NotifyPropertyChanged();
                }
            }
        }

        public SousCategorieListItemDTO SelectedSousCategorie
        {
            get => m_SelectedSousCategorie;
            set
            {
                Set(ref m_SelectedSousCategorie, value, bMarkAsModified: false);
                if (m_SelectedSousCategorie == null)
                    SousCategorieID = null;
                else
                    SousCategorieID = m_SelectedSousCategorie.ID;
            }
        }
        private SousCategorieListItemDTO m_SelectedSousCategorie;

        /// <summary>
        /// Liste avec toutes les sous categories
        /// </summary>
        public List<SousCategorieListItemDTO> AllSousCategories
        {
            get => m_AllSousCategories;
            set => Set(ref m_AllSousCategories, value, bMarkAsModified: false);
        }
        private List<SousCategorieListItemDTO> m_AllSousCategories;

        /// <summary>
        /// Liste avec les sous categories de la categories courantes
        /// </summary>
        public List<SousCategorieListItemDTO> SousCategories
        {
            get => m_SousCategories;
            set => Set(ref m_SousCategories, value, bMarkAsModified: false);
        }
        private List<SousCategorieListItemDTO> m_SousCategories;

        /// <summary>
        /// Chargement des sous categories pour la categorie courante
        /// </summary>
        private void LoadSousCategorie()
        {

            if (this.AllSousCategories == null)
                this.AllSousCategories = SousCategorieService.GetList();

            if (this.SelectedCategorie == null)
                this.SousCategories = new List<SousCategorieListItemDTO>();
            else
                this.SousCategories = AllSousCategories.Where(sc => sc.CategorieID == this.CategorieID)
                                                        .OrderBy(o=>o.CategorieName)
                                                        .ToList();
            NotifyPropertyChanged("SousCategories");
        }

        /// <summary>
        /// Retourne true si le produit est composé
        /// </summary>
        public bool ProduitCompose
        {
            get => Produit.Composition == eProduitComposition.Compose;
            set
            {
                Produit.Composition = value == true ? eProduitComposition.Compose : eProduitComposition.NonCompose;
                MarkAsModified();
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Ligne selectionné dans la compostion du produit
        /// </summary>
        public ProduitComposition SelectedProduitComposition
        {
            get => m_SelectedProduitComposition;
            set
            {
                Set(ref m_SelectedProduitComposition, value, bMarkAsModified: false);
            }
        }
        private ProduitComposition m_SelectedProduitComposition;

        /// <summary>
        /// List des produits de la compositions du produits
        /// </summary>
        public ObservableCollection<ProduitComposition> ProduitCompositions
        {
            get => m_ProduitCompositions;
            set => Set(ref m_ProduitCompositions, value, bMarkAsModified: false);
        }
        private ObservableCollection<ProduitComposition> m_ProduitCompositions;

        /// <summary>
        /// Texte de recherche pour les produits
        /// </summary>
        public string ProduitSearchText
        {
            get => m_ProduitSearchText;
            set
            {
                Set(ref m_ProduitSearchText, value, bMarkAsModified: false);
            }
        }
        private string m_ProduitSearchText;

        /// <summary>
        /// Le produit selectionné dans la liste des produits
        /// </summary>
        public Produit SelectedProduit
        {
            get => m_SelectedProduit;
            set => Set(ref m_SelectedProduit, value, bMarkAsModified: false);
        }
        private Produit m_SelectedProduit;

        /// <summary>
        /// List des produits 
        /// </summary>
        public List<Produit> Produits
        {
            get => m_Produits;
            set => Set(ref m_Produits, value, bMarkAsModified: false);
        }
        private List<Produit> m_Produits;
        #endregion

        #region SERVICES
        /// <summary>
        /// Le service de gestion des produis
        /// </summary>
        private ProduitBS Service
        {
            get; set;
        }

        /// <summary>
        /// Service de TVA
        /// </summary>
        private TVABS TvaService
        {
            get; set;
        }

        /// <summary>
        /// Service de Categorie
        /// </summary>
        private CategorieBS CategorieService
        {
            get; set;
        }

        /// <summary>
        /// Service de Sous Categorie
        /// </summary>
        private SousCategorieBS SousCategorieService
        {
            get; set;
        }
        #endregion

        #region COMMAND
        public ICommand ComputeHTCmd
        {
            get
            {
                return m_ComputeHTCmd ?? (m_ComputeHTCmd = new RelayCommand(ComputeHT));
            }
        }
        private ICommand m_ComputeHTCmd;
        public ICommand ComputeTTCCmd
        {
            get
            {
                return m_ComputeTTCCmd ?? (m_ComputeTTCCmd = new RelayCommand(ComputeTTC));
            }
        }
        private ICommand m_ComputeTTCCmd;

        public ICommand ProduitSearchCmd
        {
            get
            {
                return m_ProduitSearchCmd ?? (m_ProduitSearchCmd = new RelayCommand(ProduitSearch));
            }
        }
        private ICommand m_ProduitSearchCmd;

        public ICommand CompositionAddAboveCmd
        {
            get
            {
                return m_CompositionAddAboveCmd ?? (m_CompositionAddAboveCmd = new RelayCommand(CompositionAddAbove,CompositionCanAddAbove));
            }
        }
        private ICommand m_CompositionAddAboveCmd;
        public ICommand CompositionAddCmd
        {
            get
            {
                return m_CompositionAddCmd ?? (m_CompositionAddCmd = new RelayCommand(CompositionAdd,CompositionCanAdd));
            }
        }
        private ICommand m_CompositionAddCmd;
        public ICommand CompositionAddBelowCmd
        {
            get
            {
                return m_CompositionAddBelowCmd ?? (m_CompositionAddBelowCmd = new RelayCommand(CompositionAddBelow,CompositionCanAddBelow));
            }
        }
        private ICommand m_CompositionAddBelowCmd;
        public ICommand CompositionDeleteCmd
        {
            get
            {
                return m_CompositionDeleteCmd ?? (m_CompositionDeleteCmd = new RelayCommand(CompositionDelete,CompositionCanDelete));
            }
        }
        private ICommand m_CompositionDeleteCmd;
        #endregion COMMAND
    }
}
