using Hulkey.DAL;
using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Hulkey.SLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TU_Metiers
{
    /// <summary>
    /// Cette permet la gestion des Produits
    /// pour un prefixe donné de categories
    /// On peut : Creer ou Charger des Produits
    /// </summary>
    public class ProduitsSeeding
    {
        public ProduitsSeeding(HulkeyUnitOfWork _uow)
        {
            uow = _uow;
        }

        public void LoadProduits(string sProduitPrefixe)
        {
            Categories = new CategoriesSeeding(uow);
            Categories.LoadCategories(sProduitPrefixe);

            LoadProduitEntree(sProduitPrefixe);
            LoadProduitPlat(sProduitPrefixe);
            LoadProduitDesert(sProduitPrefixe);
        }

        private void LoadProduitEntree(string sProduitPrefixe)
        {
            var repoProduit = uow.GetRepository<ProduitRepository>();
            Produit produit;

            produit = repoProduit.FindBy(i => i.Name.Equals($"{sProduitPrefixe}-SPLEG") == true).FirstOrDefault();
            if (produit == null)
            {
                // Si les entrée n'existe pas, on lance le process de creation des toutes les caegories
                CreateProduits(sProduitPrefixe);
                produit = repoProduit.FindBy(i => i.Name.Equals($"{sProduitPrefixe}-SPLEG") == true).FirstOrDefault();
            }
            EntreeSPLegume = produit.ID;
            EntreeSPRond = repoProduit.FindBy(i => i.Name.Equals($"{sProduitPrefixe}-SPROND") == true).FirstOrDefault().ID;
            EntreeSPCesar = repoProduit.FindBy(i => i.Name.Equals($"{sProduitPrefixe}-CESAR") == true).FirstOrDefault().ID;
            EntreeSPNicoise = repoProduit.FindBy(i => i.Name.Equals($"{sProduitPrefixe}-NICOISE") == true).FirstOrDefault().ID;
            EntreeSPHuitreC = repoProduit.FindBy(i => i.Name.Equals($"{sProduitPrefixe}-HUITREC") == true).FirstOrDefault().ID;
            EntreeSPMontC = repoProduit.FindBy(i => i.Name.Equals($"{sProduitPrefixe}-MONTC") == true).FirstOrDefault().ID;
        }

        private void LoadProduitPlat(string sProduitPrefixe)
        {
            var repoProduit = uow.GetRepository<ProduitRepository>();
            PlatPorc = repoProduit.FindBy(i => i.Name.Equals($"{sProduitPrefixe}-PORC") == true).FirstOrDefault().ID;
            PlatSteak = repoProduit.FindBy(i => i.Name.Equals($"{sProduitPrefixe}-STEAK") == true).FirstOrDefault().ID;
            PlatBar = repoProduit.FindBy(i => i.Name.Equals($"{sProduitPrefixe}-BAR") == true).FirstOrDefault().ID;
            PlatSardine = repoProduit.FindBy(i => i.Name.Equals($"{sProduitPrefixe}-SARDINE") == true).FirstOrDefault().ID;
            PlatPateC = repoProduit.FindBy(i => i.Name.Equals($"{sProduitPrefixe}-PATEC") == true).FirstOrDefault().ID;
            PlatPateB = repoProduit.FindBy(i => i.Name.Equals($"{sProduitPrefixe}-PATEB") == true).FirstOrDefault().ID;
            PlatPateBolo = repoProduit.FindBy(i => i.Name.Equals($"{sProduitPrefixe}-PATEBOLO") == true).FirstOrDefault().ID;
        }

        private void LoadProduitDesert(string sProduitPrefixe)
        {
            var repoProduit = uow.GetRepository<ProduitRepository>();
            DesertIirac = repoProduit.FindBy(i => i.Name.Equals($"{sProduitPrefixe}-TIRAC") == true).FirstOrDefault().ID;
            DesertTirar = repoProduit.FindBy(i => i.Name.Equals($"{sProduitPrefixe}-TIRAR") == true).FirstOrDefault().ID;
        }

        public void CreateProduits(string sProduitPrefixe)
        {
            Categories = new CategoriesSeeding(uow);
            Categories.LoadCategories(sProduitPrefixe);

            CreateProduitEntree(sProduitPrefixe);
            CreateProduitPlat(sProduitPrefixe);
            CreateProduitDesert(sProduitPrefixe);
        }

        private void CreateProduitEntree(string sProduitPrefixe)
        {
            var repo = uow.GetRepository<ProduitRepository>();
            ProduitBuilder builder = new ProduitBuilder(uow);
            Produit produit;

            // init des valeurs communes du builder
            builder.WithDefaultValue()
                    .WithCategorie(Categories.EntreeID)
                    .WithTVA(1);

            // SOUPES
            builder.WithSousCategorie(Categories.EntreeSoupeID);
            produit = builder.WithName($"{sProduitPrefixe}-SPLEG")
                            .WithDescription("Soupe de legumes")
                            .WithPrixVenteHT(10)
                            .Build();
            repo.Create(produit);
            produit = builder.WithName($"{sProduitPrefixe}-SPROND")
                            .WithDescription("Soupe de petit rond")
                            .WithPrixVenteHT(11)
                            .Build();
            repo.Create(produit);

            // SALADES
            builder.WithSousCategorie(Categories.EntreeSaladeID);
            produit = builder.WithName($"{sProduitPrefixe}-CESAR")
                            .WithDescription("Salade Cesar")
                            .WithPrixVenteHT(13)
                            .Build();
            repo.Create(produit);

            produit = builder.WithName($"{sProduitPrefixe}-NICOISE")
                            .WithDescription("Salade Nicoise")
                            .WithPrixVenteHT(14)
                            .Build();
            repo.Create(produit);

            // CHAUDES
            builder.WithSousCategorie(Categories.EntreeChaudeID);
            produit = builder.WithName($"{sProduitPrefixe}-HUITREC")
                            .WithDescription("Huitre chaudes")
                            .WithPrixVenteHT(15)
                            .Build();
            repo.Create(produit);

            produit = builder.WithName($"{sProduitPrefixe}-MONTC")
                            .WithDescription("Mont d'or chaud")
                            .WithPrixVenteHT(16)
                            .Build();
            repo.Create(produit);

            // Sauvegarde
            int irts = uow.SaveChanges();
        }

        private void CreateProduitPlat(string sProduitPrefixe)
        {
            var repo = uow.GetRepository<ProduitRepository>();
            ProduitBuilder builder = new ProduitBuilder(uow);
            Produit produit;

            // init des valeurs communes du builder
            builder.WithDefaultValue()
                    .WithCategorie(Categories.PlatID)
                    .WithTVA(1);

            // VIANDES
            builder.WithSousCategorie(Categories.PlatViandeID);
            produit = builder.WithName($"{sProduitPrefixe}-PORC")
                            .WithDescription("Côté de porc")
                            .WithPrixVenteHT(20)
                            .Build();
            repo.Create(produit);
            produit = builder.WithName($"{sProduitPrefixe}-STEAK")
                            .WithDescription("Steack")
                            .WithPrixVenteHT(21)
                            .Build();
            repo.Create(produit);

            // POISSONS
            builder.WithSousCategorie(Categories.PlatPoissonID);
            produit = builder.WithName($"{sProduitPrefixe}-BAR")
                            .WithDescription("Bar de ligne")
                            .WithPrixVenteHT(20)
                            .Build();
            repo.Create(produit);
            produit = builder.WithName($"{sProduitPrefixe}-SARDINE")
                            .WithDescription("4 Sardines grillée")
                            .WithPrixVenteHT(21)
                            .Build();
            repo.Create(produit);

            // PATES
            builder.WithSousCategorie(Categories.PlatPateID);
            produit = builder.WithName($"{sProduitPrefixe}-PATEC")
                            .WithDescription("Pates carbonara")
                            .WithPrixVenteHT(20)
                            .Build();
            repo.Create(produit);
            produit = builder.WithName($"{sProduitPrefixe}-PATEB")
                            .WithDescription("Pates au basilic")
                            .WithPrixVenteHT(21)
                            .Build();
            repo.Create(produit);
            produit = builder.WithName($"{sProduitPrefixe}-PATEBOLO")
                            .WithDescription("Pates Bolognése")
                            .WithPrixVenteHT(22)
                            .Build();
            repo.Create(produit);

            // Sauvegarde
            int irts = uow.SaveChanges();
        }

        private void CreateProduitDesert(string sProduitPrefixe)
        {
            var repo = uow.GetRepository<ProduitRepository>();
            ProduitBuilder builder = new ProduitBuilder(uow);
            Produit produit;

            // init des valeurs communes du builder
            builder.WithDefaultValue()
                    .WithCategorie(Categories.DesertID)
                    .WithTVA(1);

            // DESERTS
            builder.WithSousCategorie(Categories.DesertDesertID);
            produit = builder.WithName($"{sProduitPrefixe}-TIRAC")
                            .WithDescription("Tiramisu choco/café")
                            .WithPrixVenteHT(12)
                            .Build();
            repo.Create(produit);
            produit = builder.WithName($"{sProduitPrefixe}-TIRAR")
                            .WithDescription("Tiramisu aux fruit rouges")
                            .WithPrixVenteHT(13)
                            .Build();
            repo.Create(produit);

            // GLACES
            builder.WithSousCategorie(Categories.DesertGlaceID);

            // Sauvegarde
            int irts = uow.SaveChanges();
        }

        public HulkeyUnitOfWork uow { get; set; }
        public CategoriesSeeding Categories { get; set; }

        public int EntreeSPLegume { get; private set; }
        public int EntreeSPRond { get; private set; }
        public int EntreeSPCesar { get; private set; }
        public int EntreeSPNicoise { get; private set; }
        public int EntreeSPHuitreC { get; private set; }
        public int EntreeSPMontC { get; private set; }
        public int PlatPorc { get; private set; }
        public int PlatSteak { get; private set; }
        public int PlatBar { get; private set; }
        public int PlatSardine { get; private set; }
        public int PlatPateC { get; private set; }
        public int PlatPateB { get; private set; }
        public int PlatPateBolo { get; private set; }
        public int DesertIirac { get; private set; }
        public int DesertTirar { get; private set; }
    }
}
