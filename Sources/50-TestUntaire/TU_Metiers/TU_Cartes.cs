using Hulkey.Common;
using Hulkey.DAL;
using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Hulkey.SLL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TU_Metiers
{
    /// <summary>
    /// Test de gestion d'une cartes
    /// </summary>
    [TestClass]
    public class TU_Cartes
    {
        #region
        /// <summary>
        /// Utilisez ClassInitialize pour exécuter le code avant l'exécution du premier test de la classe.
        /// </summary>
        [ClassInitialize()]
        public static void TU_ClassInitialize(TestContext tc)
        {
            DatabaseHelpers.EnsureCreated();

            // Chargement des categories
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();
            Produits = new ProduitsSeeding(uow);
            Produits.LoadProduits("4-");
        }

        private static ProduitsSeeding Produits { get; set; }

        /// <summary>
        /// Utilisez ClassCleanup pour exécuter le code après l'exécution de tous les tests d'une classe.
        /// </summary>
        [ClassCleanup()]
        public static void TU_ClassCleanUp()
        {
        }

        /// <summary>
        /// Utilisez TestInitialize pour exécuter le code avant l'exécution de chaque test.
        /// </summary>
        [TestInitialize()]
        public void TU_TestInitialize()
        {
        }

        /// <summary>
        /// Utilisez TestCleanup pour exécuter le code après l'exécution de chaque test.
        /// </summary>
        [TestCleanup()]
        public void TU_TestCleanUp()
        {
        }
        #endregion

        [TestMethod]
        public void TU_010_CreerLaCarte()
        {
            Log.Info("TU_CARTES : CREATION DE LA CARTES");
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();
            var rCateg = uow.GetRepository<CategorieRepository>();
            List<Categorie> lst = rCateg.GetListWithSousCategorie()
                                        .Where(c => c.Name.StartsWith("4-") == true)
                                        .ToList();
            var rCarte = uow.GetRepository<CarteRepository>();
            var rCarteElts = uow.GetRepository<CarteElementRepository>();

            Carte carte = new Carte()
            {
                ActiveCaisse = true,
                Name="LaCarte",
                Ordre=10,
            };
            rCarte.Create(carte);
            uow.SaveChanges();

            int iOrdreSection = 10;
            foreach (Categorie categ in lst)
            {
                CarteElement elts = new CarteElement()
                { 
                    CarteID = carte.ID,
                    ParentID = null,
                    Ordre = iOrdreSection,
                    Texte = categ.Description
                };
                iOrdreSection += 10;
                rCarteElts.Create(elts);
                uow.SaveChanges();

                Log.Info($"CATEG. : - {categ.Ordre} {categ.Name} {categ.Description}");
                if (categ.SousCategories.Count > 0)
                {
                    foreach (SousCategorie scateg in categ.SousCategories)
                    {
                        Log.Info($"SCATEG : -- {scateg.Ordre} {scateg.Name} {scateg.Description}");
                        DumpProduit(uow, categ.ID, scateg.ID,elts.ID,rCarteElts);
                    }
                }
            }
        }

        [TestMethod]
        public void TU_010_CreerLesMenus()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            var repo = uow.GetRepository<ProduitRepository>();
        }


        private void DumpProduit(HulkeyUnitOfWork uow,int iCategorieID, int iSousCategorieID,int EltsID, CarteElementRepository rCarteElts)
        {
            var rProd = uow.GetRepository<ProduitRepository>();
            List<Produit> lst = rProd.GetListForCategorieSousCategorie(iCategorieID, iSousCategorieID);
            if (lst.Count > 0)
            {
                int iOrdreProduit = 10;
                foreach (Produit produit in lst)
                {
                    Log.Info($"PROD.. : --- {produit.Name} {produit.Description} {produit.PrixVenteTTC}");
                    CarteElement eltProduit = new CarteElement()
                    {
                        CarteID = null,
                        ParentID = EltsID,
                        Ordre = iOrdreProduit,
                        Texte = produit.Description,
                        ProduitID = produit.ID
                       
                    };
                    iOrdreProduit += 10;
                    rCarteElts.Create(eltProduit);
                    uow.SaveChanges();
                }
            }
            else
            {
                Log.Info($"PROD.. : --- Pas de produit pour Catégorie/Sous-Categorie");
            }
        }
    }
}
