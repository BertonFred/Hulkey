using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TU_Repository
{
    [TestClass]
    public class TU_TypePaiementRepository
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
        public void TU_000_Creation_TypePaiement()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            var repo = uow.GetRepository<TypePaiementRepository>();
               TypePaiement typePaiement = new TypePaiement()
                {
                   Version = 1,
                   Name = "Test",
                   Libelle = "Argent liquide",
                   CreatedBy = "Hulkey",
                   CreatedOn = DateTime.Now
               };
            repo.Create(typePaiement);
            int irts = uow.SaveChanges();
            iCreatedRecord = typePaiement.ID;
            Assert.AreEqual(irts, 1);
            Assert.IsTrue(iCreatedRecord > 0);
        }

        [TestMethod]
        public void TU_010_Lecture_TypePaiement()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            var repo = uow.GetRepository<TypePaiementRepository>();
            TypePaiement typePaiement = repo.Read(iCreatedRecord);
            Assert.IsNotNull(typePaiement);
            Assert.IsTrue(iCreatedRecord > 0);
            Assert.IsTrue(typePaiement.Version == 1);
            Assert.IsFalse(typePaiement.Deleted);
        }

        [TestMethod]
        public void TU_020_MiseAJour_TypePaiement()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de typePaiement
            var repo = uow.GetRepository<TypePaiementRepository>();
            TypePaiement typePaiement = repo.Read(iCreatedRecord);
            typePaiement.Libelle = "test";
            repo.Update(typePaiement);
            int irts = uow.SaveChanges();
            Assert.AreEqual(irts, 1);

            // Verifier la modification
            TypePaiement tpUpdated = repo.Read(iCreatedRecord);
            Assert.IsNotNull(tpUpdated);
            Assert.AreEqual(tpUpdated.Libelle, "test");
            Assert.AreEqual(tpUpdated.Version, 2);
        }

        [TestMethod]
        public void TU_030_Requete_FindAll_TypePaiement()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de typePaiement
            var repo = uow.GetRepository<TypePaiementRepository>();
            List<TypePaiement> res = repo.FindAll().ToList();

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count > 1);
        }

        [TestMethod]
        public void TU_040_Requete_FindBy_TypePaiement()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de typePaiement
            var repo = uow.GetRepository<TypePaiementRepository>();
            List<TypePaiement> res = repo.FindBy(q => q.Name == "Test").ToList();

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count >= 1);
        }

        [TestMethod]
        public void TU_050_Requete_GetListAsync_TypePaiement()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de typePaiement
            var repo = uow.GetRepository<TypePaiementRepository>();
            List<TypePaiement> res = repo.GetListAsync().Result;

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count >= 1);
        }

        [TestMethod]
        public void TU_060_Suppresion_TypePaiement()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // Test de la suppression
            var repo = uow.GetRepository<TypePaiementRepository>();
            repo.DeleteById(iCreatedRecord);
            int irts = uow.SaveChanges();
            Assert.AreEqual(irts,1);

            // Verifier la suppression
            TypePaiement tvaDeleted = repo.Read(iCreatedRecord);
            Assert.IsNull(tvaDeleted);
        }
    }
}
