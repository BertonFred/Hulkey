using Hulkey.DAL;
using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TU_Metiers
{
    /// <summary>
    /// Test sur Categorie et sous categorie
    /// on en profite pour test aussi pour testé la gestion des transactions 
    /// et du context du UnitOfWork
    /// </summary>
    [TestClass]
    public class TU_CategorieSousCategorie
    {
        #region
        /// <summary>
        /// Utilisez ClassInitialize pour exécuter le code avant l'exécution du premier test de la classe.
        /// </summary>
        [ClassInitialize()]
        public static void TU_ClassInitialize(TestContext tc)
        {
            DatabaseHelpers.EnsureCreated();
        }

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

        /// <summary>
        /// on test la creation dans une transaction
        /// </summary>
        [TestMethod]
        public void TU_000_Creation_Categorie_InTransaction()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();
            var builder = new CategoriesSeeding(uow);
            
            // Creation
            uow.ExecuteInTransaction( u =>
                {
                    builder.CreateCategories("1-");
                });

            // Verification
            var repo = uow.GetRepository<CategorieRepository>();
            List<Categorie> lst = repo.FindBy(i => i.Name.StartsWith("1-") == true).ToList();
            Assert.IsNotNull(lst);
            Assert.AreEqual(lst.Count, 4);
        }

        /// <summary>
        /// on test la suppression aborté via une transaction
        /// </summary>
        [TestMethod]
        public void TU_010_Delete_Categorie_InTransaction_RollBack()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();
            uow.ExecuteInTransaction(u =>
            {
                // Recupere les données
                var repo = u.GetRepository<CategorieRepository>();
                List<Categorie> lst = repo.FindBy(i => i.Name.StartsWith("1-") == true).ToList();
                Assert.IsNotNull(lst);
                Assert.AreEqual(lst.Count, 4);

                // Faire des suppressions, puis un rollback
                repo.Delete(lst[0]);
                u.SaveChanges();
                repo.DeleteById(lst[1].ID);
                u.SaveChanges();

                u.RollbackTransaction();
            });

            // Verification
            var repo2 = uow.GetRepository<CategorieRepository>();
            List<Categorie> lst2 = repo2.FindBy(i => i.Name.StartsWith("1-") == true).ToList();
            Assert.IsNotNull(lst2);
            Assert.AreEqual(lst2.Count, 4);
        }

        /// <summary>
        /// on test la creation dans le context
        /// </summary>
        [TestMethod]
        public void TU_020_Creation_Categorie_InContext()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();
            var builder = new CategoriesSeeding(uow);

            // Creation
            builder.CreateCategories("2-");

            // Verification
            var repo = uow.GetRepository<CategorieRepository>();
            List<Categorie> lst = repo.FindBy(i => i.Name.StartsWith("2-") == true).ToList();
            Assert.IsNotNull(lst);
            Assert.AreEqual(lst.Count, 4);
        }

        /// <summary>
        /// on test la suppression aborté dans le context
        /// </summary>
        [TestMethod]
        public void TU_030_Delete_Categorie_InContext_RollBack()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // Recupere les données
            var repo = uow.GetRepository<CategorieRepository>();
            List<Categorie> lst = repo.FindBy(i => i.Name.StartsWith("2-") == true).ToList();
            Assert.IsNotNull(lst);
            Assert.AreEqual(lst.Count, 4);

            // Faire des suppressions, puis un rollback dans le context
            repo.Delete(lst[0]);
            repo.DeleteById(lst[1].ID);

            uow.RollbackChanges();
            int irts = uow.SaveChanges();
            Assert.AreEqual(irts, 0); // pas de sauvegarde a cause du rollback

            // Verification
            var repo2 = uow.GetRepository<CategorieRepository>();
            List<Categorie> lst2 = repo2.FindBy(i => i.Name.StartsWith("2-") == true).ToList();
            Assert.IsNotNull(lst2);
            Assert.AreEqual(lst2.Count, 4);
        }
    }
}
