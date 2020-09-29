using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TU_Repository
{
    [TestClass]
    public class TU_CarteElementElementRepository
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

        static int iCreatedRecord;

        [TestMethod]
        public void TU_000_Creation_CarteElement()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            var repo = uow.GetRepository<CarteElementRepository>();
               CarteElement obj = new CarteElement()
                {
                    CarteID = null,
                    ParentID = null,
                    ProduitID = null,
                    Version = 1,
                    Texte = "CarteElement 1",
                    Ordre = 10,
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
        public void TU_005_Creation_CarteElementChilds()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            var repo = uow.GetRepository<CarteElementRepository>();
            CarteElement objParent = new CarteElement()
            {
                CarteID = null,
                ParentID = null,
                ProduitID = null,
                Version = 1,
                Texte = "CarteElement parent",
                Ordre = 1,
                CreatedBy = "Hulkey",
                CreatedOn = DateTime.Now
            };
            repo.Create(objParent);
            int irts = uow.SaveChanges();

            CarteElement obj = new CarteElement()
            {
                CarteID = null,
                ParentID = objParent.ID,
                ProduitID = null,
                Version = 1,
                Texte = $"CarteElement 1 child de {objParent.ID}",
                Ordre = 1,
                CreatedBy = "Hulkey",
                CreatedOn = DateTime.Now
            };
            repo.Create(obj);

            CarteElement obj2 = new CarteElement()
            {
                CarteID = null,
                ParentID = objParent.ID,
                ProduitID = null,
                Version = 3,
                Texte = $"CarteElement 2 child de {objParent.ID}",
                Ordre = 2,
                CreatedBy = "Hulkey",
                CreatedOn = DateTime.Now
            };
            repo.Create(obj2);
            irts = uow.SaveChanges();

            Assert.AreEqual(irts, 2);
            Assert.IsTrue(iCreatedRecord > 0);
        }

        [TestMethod]
        public void TU_010_Lecture_CarteElement()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            var repo = uow.GetRepository<CarteElementRepository>();
            CarteElement obj = repo.Read(iCreatedRecord);
            Assert.IsNotNull(obj);
            Assert.IsTrue(iCreatedRecord > 0);
            Assert.IsTrue(obj.Version == 1);
            Assert.IsFalse(obj.Deleted);
        }

        [TestMethod]
        public void TU_020_MiseAJour_CarteElement()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de obj
            var repo = uow.GetRepository<CarteElementRepository>();
            CarteElement obj = repo.Read(iCreatedRecord);
            obj.Ordre = 10;
            repo.Update(obj);
            int irts = uow.SaveChanges();
            Assert.AreEqual(irts, 1);

            // Verifier la modification
            CarteElement objUpdated = repo.Read(iCreatedRecord);
            Assert.IsNotNull(objUpdated);
            Assert.AreEqual(objUpdated.Ordre,10);
            Assert.AreEqual(objUpdated.Version, 2);
        }

        [TestMethod]
        public void TU_030_Requete_FindAll_CarteElement()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de obj
            var repo = uow.GetRepository<CarteElementRepository>();
            List<CarteElement> objs = repo.FindAll().ToList();

            Assert.IsNotNull(objs);
            Assert.IsTrue(objs.Count >= 1);
        }

        [TestMethod]
        public void TU_040_Requete_FindBy_CarteElement()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de obj
            var repo = uow.GetRepository<CarteElementRepository>();
            List<CarteElement> objs = repo.FindBy(q => q.Ordre >= 10).ToList();

            Assert.IsNotNull(objs);
            Assert.IsTrue(objs.Count >= 1);
        }

        [TestMethod]
        public void TU_045_Requete_FindBy_CarteElementChilds()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de obj
            var repo = uow.GetRepository<CarteElementRepository>();
            List<CarteElement> objs = repo.FindBy(q => q.ParentID.HasValue == true).ToList();

            Assert.IsNotNull(objs);
            Assert.IsTrue(objs.Count >= 1);
        }

        [TestMethod]
        public void TU_050_Requete_GetList_CarteElement()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de obj
            var repo = uow.GetRepository<CarteElementRepository>();
            List<CarteElement> objs = repo.GetList();

            Assert.IsNotNull(objs);
            Assert.IsTrue(objs.Count >= 1);
        }

        [TestMethod]
        public void TU_060_Suppresion_CarteElement()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // Test de la suppression
            var repo = uow.GetRepository<CarteElementRepository>();
            repo.DeleteById(iCreatedRecord);
            int irts = uow.SaveChanges();
            Assert.AreEqual(irts,1);

            // Verifier la suppression
            CarteElement objDeleted = repo.Read(iCreatedRecord);
            Assert.IsNull(objDeleted);
        }
    }
}
