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
    public class TU_UtilisateurRepository
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
        public void TU_000_Creation_Utilisateur()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            var repo = uow.GetRepository<UtilisateurRepository>();
               Utilisateur obj = new Utilisateur()
                {
                    Version = 1,
                    Nom = "UtilisateurName",
                    Prenom="Utilisateur Prenom",
                    eMail="UtilisateureMail",
                    Password="UtilisateurPassword",
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
        public void TU_010_Lecture_Utilisateur()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            var repo = uow.GetRepository<UtilisateurRepository>();
            Utilisateur obj = repo.Read(iCreatedRecord);
            Assert.IsNotNull(obj);
            Assert.IsTrue(iCreatedRecord > 0);
            Assert.IsTrue(obj.Version == 1);
            Assert.IsFalse(obj.Deleted);
        }

        [TestMethod]
        public void TU_020_MiseAJour_Utilisateur()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de obj
            var repo = uow.GetRepository<UtilisateurRepository>();
            Utilisateur obj = repo.Read(iCreatedRecord);
            obj.Nom = "UtilisateurNomModifier";
            repo.Update(obj);
            int irts = uow.SaveChanges();
            Assert.AreEqual(irts, 1);

            // Verifier la modification
            Utilisateur objUpdated = repo.Read(iCreatedRecord);
            Assert.IsNotNull(objUpdated);
            Assert.AreEqual(objUpdated.Nom, "UtilisateurNomModifier");
            Assert.AreEqual(objUpdated.Version, 2);
        }

        [TestMethod]
        public void TU_030_Requete_FindAll_Utilisateur()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de obj
            var repo = uow.GetRepository<UtilisateurRepository>();
            List<Utilisateur> objs = repo.FindAll().ToList();

            Assert.IsNotNull(objs);
            Assert.IsTrue(objs.Count >= 1);
        }

        [TestMethod]
        public void TU_040_Requete_FindBy_Utilisateur()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de obj
            var repo = uow.GetRepository<UtilisateurRepository>();
            List<Utilisateur> objs = repo.FindBy(q => q.Version >= 1).ToList();

            Assert.IsNotNull(objs);
            Assert.IsTrue(objs.Count >= 1);
        }

        [TestMethod]
        public void TU_050_Requete_GetList_Utilisateur()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // modifier un taux de obj
            var repo = uow.GetRepository<UtilisateurRepository>();
            List<UtilisateurListItemDTO> objs = repo.GetList();

            Assert.IsNotNull(objs);
            Assert.IsTrue(objs.Count >= 1);
        }

        [TestMethod]
        public void TU_060_Suppresion_Utilisateur()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            // Test de la suppression
            var repo = uow.GetRepository<UtilisateurRepository>();
            repo.DeleteById(iCreatedRecord);
            int irts = uow.SaveChanges();
            Assert.AreEqual(irts,1);

            // Verifier la suppression
            Utilisateur objDeleted = repo.Read(iCreatedRecord);
            Assert.IsNull(objDeleted);
        }
    }
}
