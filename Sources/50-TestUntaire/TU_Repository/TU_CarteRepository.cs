using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TU_Repository
{
    [TestClass]
    public class TU_CarteRepository
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
        public void TU_000_Creation_Carte()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            var repo = uow.GetRepository<CarteRepository>();
               Carte obj = new Carte()
                {
                    Version = 1,
                    Name = "Carte 1",
                    ActiveCaisse = true,
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
        public void TU_010_Lecture_Carte()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            var repo = uow.GetRepository<CarteRepository>();
            Carte obj = repo.Read(iCreatedRecord);
            Assert.IsNotNull(obj);
            Assert.IsTrue(iCreatedRecord > 0);
            Assert.IsTrue(obj.Version == 1);
            Assert.IsFalse(obj.Deleted);
        }

        [TestMethod]
        public void TU_020_MiseAJour_Carte()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de obj
            var repo = uow.GetRepository<CarteRepository>();
            Carte obj = repo.Read(iCreatedRecord);
            obj.ActiveCaisse = false;
            repo.Update(obj);
            int irts = uow.SaveChanges();
            Assert.AreEqual(irts, 1);

            // Verifier la modification
            Carte objUpdated = repo.Read(iCreatedRecord);
            Assert.IsNotNull(objUpdated);
            Assert.AreEqual(objUpdated.ActiveCaisse,false);
            Assert.AreEqual(objUpdated.Version, 2);
        }

        [TestMethod]
        public void TU_030_Requete_FindAll_Carte()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de obj
            var repo = uow.GetRepository<CarteRepository>();
            List<Carte> objs = repo.FindAll().ToList();

            Assert.IsNotNull(objs);
            Assert.IsTrue(objs.Count >= 1);
        }

        [TestMethod]
        public void TU_040_Requete_FindBy_Carte()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de obj
            var repo = uow.GetRepository<CarteRepository>();
            List<Carte> objs = repo.FindBy(q => q.ActiveCaisse == false).ToList();

            Assert.IsNotNull(objs);
            Assert.IsTrue(objs.Count >= 1);
        }

        [TestMethod]
        public void TU_050_Requete_GetList_Carte()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de obj
            var repo = uow.GetRepository<CarteRepository>();
            List<Carte> objs = repo.GetList();

            Assert.IsNotNull(objs);
            Assert.IsTrue(objs.Count >= 1);
        }

        [TestMethod]
        public void TU_060_Suppresion_Carte()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // Test de la suppression
            var repo = uow.GetRepository<CarteRepository>();
            repo.DeleteById(iCreatedRecord);
            int irts = uow.SaveChanges();
            Assert.AreEqual(irts,1);

            // Verifier la suppression
            Carte objDeleted = repo.Read(iCreatedRecord);
            Assert.IsNull(objDeleted);
        }
    }
}
