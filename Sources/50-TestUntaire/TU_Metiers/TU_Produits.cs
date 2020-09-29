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
    public class TU_Produits
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
            Categories = new CategoriesSeeding(uow);
            Categories.LoadCategories("3-");
        }

        private static CategoriesSeeding Categories { get; set; }

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
        public void TU_010_CreerLesProduits()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();
            ProduitsSeeding builder = new ProduitsSeeding(uow);

            builder.CreateProduits("3-");
        }

        [TestMethod]
        public void TU_900_FaireLaListeProduit()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();
            Log.Info("TU_PRODUITS : LISTE DES PRODUITS");
            var rCateg = uow.GetRepository<CategorieRepository>();
            List<Categorie> lst = rCateg.GetListWithSousCategorie()
                                        .Where(c=>c.Name.StartsWith("3-") == true)
                                        .ToList();
            foreach (Categorie categ in lst)
            {
                Log.Info($"CATEG. : - {categ.Ordre} {categ.Name} {categ.Description}");
                if (categ.SousCategories.Count > 0)
                {
                    foreach (SousCategorie scateg in categ.SousCategories)
                    {
                        Log.Info($"SCATEG : -- {scateg.Ordre} {scateg.Name} {scateg.Description}");
                        DumpProduit(uow, categ.ID, scateg.ID);
                    }
                }
                else
                {
                    Log.Info($"CATEG. : - Pas de Sous-categories");
                }
            }
            Log.Info("FIN DE LA LISTE");
        }

        private void DumpProduit(HulkeyUnitOfWork uow,int iCategorieID, int iSousCategorieID)
        {
            var rProd = uow.GetRepository<ProduitRepository>();
            List<Produit> lst = rProd.GetListForCategorieSousCategorie(iCategorieID, iSousCategorieID);
            if (lst.Count > 0)
            {
                foreach (Produit produit in lst)
                {
                    Log.Info($"PROD.. : --- {produit.Name} {produit.Description} {produit.PrixVenteTTC}");
                }
            }
            else
            {
                Log.Info($"PROD.. : --- Pas de produit pour Catégorie/Sous-Categorie");
            }
        }
    }
}
