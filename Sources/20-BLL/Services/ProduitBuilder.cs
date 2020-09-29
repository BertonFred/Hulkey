using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Hulkey.SLL.ServiceCommon;
using System;
using System.Linq;

namespace Hulkey.SLL.Services
{
    /// <summary>
    /// Builder pour une entités de type Produit
    /// </summary>
    public sealed class ProduitBuilder 
    {
        public ProduitBuilder(HulkeyUnitOfWork _uow=null)
        {
            this.uow = _uow;
        }

        // Retourne une instance de l'objet a fabriquer
        // L'objet est initialisée en fonction du parametrage fait
        public Produit Build()
        {
            Produit obj = new Produit();

            ComputePrixVente();
            ComputePrixAchat();

            if (this.ID.HasValue == true)           obj.ID = this.ID.Value;
            if (this.Deleted.HasValue == true)      obj.Deleted = this.Deleted.Value;
            if (this.Version.HasValue == true)      obj.Version = this.Version.Value;
            if (this.ModifiedBy != null)      obj.ModifiedBy = this.ModifiedBy;
            if (this.ModifiedOn.HasValue == true)      obj.ModifiedOn = this.ModifiedOn;
            if (this.CreatedBy != null)      obj.CreatedBy = this.CreatedBy;
            if (this.CreatedOn.HasValue == true)      obj.CreatedOn = this.CreatedOn;
            if (this.DeletedBy != null)      obj.DeletedBy = this.DeletedBy;
            if (this.DeletedOn.HasValue == true)      obj.DeletedOn = this.DeletedOn;
            if (this.Composition.HasValue == true)      obj.Composition = this.Composition.Value;
            if (this.Name != null)      obj.Name = this.Name;
            if (this.Description != null)      obj.Description = this.Description;
            if (this.CategorieID.HasValue == true)      obj.CategorieID = this.CategorieID;
            if (this.SousCategorieID.HasValue == true)      obj.SousCategorieID = this.SousCategorieID;
            if (this.PrixVenteHT.HasValue == true)      obj.PrixVenteHT = this.PrixVenteHT.Value;
            if (this.PrixVenteTTC.HasValue == true)      obj.PrixVenteTTC = this.PrixVenteTTC.Value;
            if (this.TVAID.HasValue == true)      obj.TVAID = this.TVAID.Value;
            if (this.PrixAchatHT.HasValue == true)      obj.PrixAchatHT = this.PrixAchatHT.Value;
            if (this.PrixAchatTTC.HasValue == true)      obj.PrixAchatTTC = this.PrixAchatTTC.Value;

            return obj;
        }

        // Initialise le builder a partir d'une instance d'un produit existant en base de données
        public ProduitBuilder WithExistingProduit(int iID)
        {
            if (uow == null)
                throw new NullReferenceException("Impossible de faire une initialisation depuis un produit existant, car le UnitOfWork est null.");

            if (iID <= 0)
                throw new ArgumentOutOfRangeException("iID", "ID d'objet incorrecte, doit être supérieur à 0.");

            var repo = uow.GetRepository<ProduitRepository>();
            Produit produit = repo.Read(iID);

            if (produit == null)
                throw new ArgumentOutOfRangeException("iID", "ID d'objet incorrecte, l'objet n'existe pas en base de données.");

            this.ID = produit.ID;
            this.Deleted = produit.Deleted;
            this.Version = produit.Version;
            this.ModifiedBy = produit.ModifiedBy;
            this.ModifiedOn = produit.ModifiedOn;
            this.CreatedBy = produit.CreatedBy;
            this.CreatedOn = produit.CreatedOn;
            this.DeletedBy = produit.DeletedBy;
            this.DeletedOn = produit.DeletedOn;
            this.Composition = produit.Composition;
            this.Name = produit.Name;
            this.Description = produit.Description;
            this.CategorieID = produit.CategorieID;
            this.SousCategorieID = produit.SousCategorieID;
            this.PrixVenteHT = produit.PrixVenteHT;
            this.PrixVenteTTC = produit.PrixVenteTTC;
            this.TVAID = produit.TVAID;
            this.PrixAchatHT = produit.PrixAchatHT;
            this.PrixAchatTTC = produit.PrixAchatTTC;

            return this;
        }

        // Initialise le builder a partir de valeur par défaut
        public ProduitBuilder WithDefaultValue()
        {
            Version = 1;
            Deleted = false;
            Composition = eProduitComposition.NonCompose;

            return this;
        }

        public ProduitBuilder WithName(string sName)
        {
            Name = sName;

            return this;
        }

        public ProduitBuilder WithDescription(string sDescription)
        {
            Description = sDescription;

            return this;
        }

        public ProduitBuilder WithComposition(eProduitComposition typeComposition)
        {
            this.Composition = typeComposition;

            return this;
        }

        // Initialise le Parent du produit dans le cas d'un produit composé
        public ProduitBuilder WithParent(int iParentID)
        {
            this.ParentID = iParentID;
            LoadParent();
            if (this.Parent.Composition == eProduitComposition.NonCompose)
                throw new ArgumentOutOfRangeException("Impossible d'ajouter un composé à un produit qui n'est pas un composé.");

            this.Composition = eProduitComposition.NonCompose;

            return this;
        }

        public ProduitBuilder WithCategorie(int iCategorieID)
        {
            this.CategorieID = iCategorieID;

            LoadCategorie();

            return this;
        }

        public ProduitBuilder WithSousCategorie(int iSousCategorieID)
        {
            if (CategorieID.HasValue == false)
                throw new InvalidOperationException("Il faut d'abord indiquer la Categorie avant de fixer la categorie.");

            SousCategorieID = iSousCategorieID;

            LoadCategorie();

            return this;
        }

        // Initialise la TVA
        public ProduitBuilder WithTVA(int iTVAID)
        {
            TVAID = iTVAID;

            LoadTVA();

            return this;
        }

        // Initialise le prix de vente HT et calcul le TTC si possible, il existe une TVA
        public ProduitBuilder WithPrixVenteHT(decimal dPrixVenteHT)
        {
            PrixVenteHT = dPrixVenteHT;
            ComputePrixVente();

            return this;
        }

        // Initialise le prix de Achat HT et calcul le TTC si possible, il existe une TVA
        public ProduitBuilder WithPrixAchatHT(decimal dPrixAchatHT)
        {
            PrixAchatHT = dPrixAchatHT;
            ComputePrixAchat();

            return this;
        }

        private void ComputePrixAchat()
        {
            if (this.PrixAchatHT.HasValue == false)
                return;

            if (TVAID.HasValue == true)
            {
                LoadTVA(bForceLoad:false);
                this.PrixAchatTTC = this.PrixAchatHT * (1.0m + (this.TauxTVA.Value / 100.0m));                   
            }
            else
            {
                this.PrixAchatTTC = this.PrixAchatHT ;                   
            }
        }

        private void ComputePrixVente()
        {
            if (this.PrixVenteHT.HasValue == false)
                return;

            if (TVAID.HasValue == true)
            {
                LoadTVA(bForceLoad:false);
                this.PrixVenteTTC = this.PrixVenteHT * (1.0m + (this.TauxTVA.Value / 100.0m));                   
            }
            else
            {
                this.PrixVenteTTC = this.PrixVenteHT ;                   
            }
        }

        // Charge la TVA et affecte le TauxTVA a partir de l'ID de TVA
        private void LoadTVA(bool bForceLoad=true)
        {
            if (this.TVAID.HasValue == false)
            {
                this.TVA = null;
                this.TauxTVA = null;
            }
            else
            {
                if (this.TVA != null && bForceLoad == false)
                    return; // ne pas recharger si dejà fait

                if (uow != null)
                {
                    var repo = uow.GetRepository<TVARepository>();
                    this.TVA = repo.Read(this.TVAID.Value);
                    this.TauxTVA = this.TVA.Taux;
                }
            }
        }

        // Charge la categorie et la sous categorie a partir de leurs ID respective
        // Garantie la coherence de la sous categorie par rapport a la categorire
        // La sous categorie courante doit être membre des sous categories de la categorie courante
        private void LoadCategorie()
        {
            if (this.CategorieID.HasValue == false)
            {
                this.CategorieID = null;
                this.Categorie = null;
                this.SousCategorieID = null;
                this.SousCategorie = null;
            }
            else
            {
                    if (uow != null)
                    {
                        var repo = uow.GetRepository<CategorieRepository>();
                        this.Categorie = repo.ReadWithSousCategorie(this.CategorieID.Value);

                        // Chargement de la sous categorie
                        if (this.SousCategorieID.HasValue == true)
                        {
                            // Verifier que la sous categorie est membre des sous categorie de la categorie
                            SousCategorie sousCategorie = this.Categorie.SousCategories
                                                            .FirstOrDefault(sc => sc.ID == this.SousCategorieID.Value);
                            if (sousCategorie != null)
                            {
                                // la sous categorie courante est membre des sous categories de la categorie
                                this.SousCategorie = sousCategorie;
                            }
                            else
                            {
                                // la sous categorie courante n'est pas membre des sous categories de la categorie
                                this.SousCategorieID = null;
                                this.SousCategorie = null;
                            }
                        }
                        else
                        {
                            this.SousCategorieID = null;
                            this.SousCategorie = null;
                        }
                }
            }
        }

        private void LoadParent()
        {
            if (uow != null)
            {
                var repo = uow.GetRepository<ProduitRepository>();
                this.Parent = repo.Read(this.TVAID.Value);
            }
        }


        private HulkeyUnitOfWork uow { get; set; }

        #region PRODUIT PROPERTIES
        private int? ID { get; set; }

        private bool? Deleted { get; set; }
        private long? Version { get; set; }

        private string ModifiedBy { get; set; }
        private DateTimeOffset? ModifiedOn { get; set; }
        private string CreatedBy { get; set; }
        private DateTimeOffset? CreatedOn { get; set; }
        private string DeletedBy { get; set; }
        private DateTimeOffset? DeletedOn { get; set; }

        private int? ParentID { get; set; }
        private Produit Parent { get; set; }
        private eProduitComposition? Composition { get; set; }

        private string Name { get; set; }
        private string Description { get; set; }

        private int? CategorieID { get; set; }
        private Categorie Categorie { get; set; }

        private int? SousCategorieID { get; set; }
        private SousCategorie SousCategorie { get; set; }

        private decimal? PrixVenteHT { get; set; }
        private decimal? PrixVenteTTC { get; set; }
        private decimal? TauxTVA { get; set; }
        private int? TVAID { get; set; }
        private TVA TVA { get; set; }
        private decimal? PrixAchatHT { get; set; }
        private decimal? PrixAchatTTC { get; set; }
        #endregion PRODUIT PROPERTIES
    }
}
