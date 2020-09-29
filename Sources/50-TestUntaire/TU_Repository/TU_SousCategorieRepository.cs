using Hulkey.DAL.DTO;
using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TU_Repository
{
    [TestClass]
    public class TU_SousCategorieRepository
    {
        #region
        /// <summary>
        /// Utilisez ClassInitialize pour exécuter le code avant l'exécution du premier test de la classe.
        /// </summary>
        [ClassInitialize()]
        public static void TU_ClassInitialize(TestContext tc)
        {
            DatabaseHelpers.EnsureCreated();

            // Creation de la categorie parent des sous categorie de ce test
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();
            var rCateg = uow.GetRepository<CategorieRepository>();
            var categoie = new Categorie() {
                Name = "TCATEG",
                Description = "Catégorie de test",
                Ordre = 90
            };
            rCateg.Create(categoie);
            uow.SaveChanges();
            iCategorieRecord = categoie.ID;
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

        static int iCreatedRecord;
        static int iCategorieRecord;

        [TestMethod]
        public void TU_000_Creation_SousCategorie()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            var repo = uow.GetRepository<SousCategorieRepository>();
               SousCategorie obj = new SousCategorie()
                {
                    Version = 1,
                    Name = "SousCategorieName",
                    Description ="SousCategorie description",
                    Ordre = 10,
                    CategorieID = iCategorieRecord,
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                };
            repo.Create(obj);
            int irts = uow.SaveChanges();
            iCreatedRecord = obj.ID;
            Assert.AreEqual(irts, 1);
            Assert.IsTrue(iCreatedRecord > 0);
        }

        [TestMethod]
        public void TU_010_Lecture_SousCategorie()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            var repo = uow.GetRepository<SousCategorieRepository>();
            SousCategorie obj = repo.Read(iCreatedRecord);
            Assert.IsNotNull(obj);
            Assert.IsTrue(iCreatedRecord > 0);
            Assert.IsTrue(obj.Version == 1);
            Assert.IsFalse(obj.Deleted);
        }

        [TestMethod]
        public void TU_020_MiseAJour_SousCategorie()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de obj
            var repo = uow.GetRepository<SousCategorieRepository>();
            SousCategorie obj = repo.Read(iCreatedRecord);
            obj.Ordre= 20;
            repo.Update(obj);
            int irts = uow.SaveChanges();
            Assert.AreEqual(irts, 1);

            // Verifier la modification
            SousCategorie objUpdated = repo.Read(iCreatedRecord);
            Assert.IsNotNull(objUpdated);
            Assert.AreEqual(objUpdated.Ordre, 20);
            Assert.AreEqual(objUpdated.Version, 2);
        }

        [TestMethod]
        public void TU_030_Requete_FindAll_SousCategorie()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de obj
            var repo = uow.GetRepository<SousCategorieRepository>();
            List<SousCategorie> objs = repo.FindAll().ToList();

            Assert.IsNotNull(objs);
            Assert.IsTrue(objs.Count >= 1);
        }

        [TestMethod]
        public void TU_040_Requete_FindBy_SousCategorie()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de obj
            var repo = uow.GetRepository<SousCategorieRepository>();
            List<SousCategorie> objs = repo.FindBy(q => q.Version >= 1).ToList();

            Assert.IsNotNull(objs);
            Assert.IsTrue(objs.Count >= 1);
        }

        [TestMethod]
        public void TU_050_Requete_GetList_SousCategorie()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de obj
            var repo = uow.GetRepository<SousCategorieRepository>();
            List<SousCategorieListItemDTO> objs = repo.GetList();

            Assert.IsNotNull(objs);
            Assert.IsTrue(objs.Count >= 1);
        }

        [TestMethod]
        public void TU_060_Suppresion_SousCategorie()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // Test de la suppression
            var repo = uow.GetRepository<SousCategorieRepository>();
            repo.DeleteById(iCreatedRecord);
            int irts = uow.SaveChanges();
            Assert.AreEqual(irts,1);

            // Verifier la suppression
            SousCategorie objDeleted = repo.Read(iCreatedRecord);
            Assert.IsNull(objDeleted);
        }
    }
}
