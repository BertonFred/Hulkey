using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TU_Repository
{
    [TestClass]
    public class TU_DatabaseHelpers
    {
        #region
        /// <summary>
        /// Utilisez ClassInitialize pour exécuter le code avant l'exécution du premier test de la classe.
        /// </summary>
        [ClassInitialize()]
        public static void TU_ClassInitialize(TestContext tc)
        {
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

        [TestMethod]
        public void TU_000_CreateDatabase()
        {
            bool bRts ;

            // la base de données ne devrait pas existe
            // True if the database is created, false if it already existed.
            bRts = DatabaseHelpers.EnsureCreated();
            if (bRts == false)
            {
                bRts = DatabaseHelpers.EnsureDeleted();
                bRts = DatabaseHelpers.EnsureCreated();
            }
            Assert.IsTrue(bRts, "Erreur : La base de données existe déjà.");
        }

        [TestMethod]
        public void TU_010_VerifierLesTVAParDefaut()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            var repository2 = uow.GetRepository<TVARepository>();
            TVA tva1 = repository2.Read(1);
            Assert.AreEqual(tva1.Taux, 20.0M);

            TVA tva2 = repository2.Read(2);
            Assert.AreEqual(tva2.Taux, 2.10M);

            TVA tva3 = repository2.Read(3);
            Assert.AreEqual(tva3.Taux, 5.50M);

            TVA tva4 = repository2.Read(4);
            Assert.AreEqual(tva4.Taux, 10.0M);
        }

        [TestMethod]
        public void TU_020_VerifierLesTPFParDefaut()
        {
            // rien pour le momment
        }

        [TestMethod]
        public void TU_030_VerifierLesTypePaiementParDefaut()
        {
            HulkeyUnitOfWork uow = new HulkeyUnitOfWork();

            var repository2 = uow.GetRepository<TypePaiementRepository>();
            TypePaiement tp1 = repository2.Read(1);
            Assert.AreEqual(tp1.Name, "Cash");

            TypePaiement tp2 = repository2.Read(2);
            Assert.AreEqual(tp2.Name, "CB");

            TypePaiement tp3 = repository2.Read(3);
            Assert.AreEqual(tp3.Name, "CHQ");

            TypePaiement tp4 = repository2.Read(4);
            Assert.AreEqual(tp4.Name, "TR");
        }

        [TestMethod]
        public void TU_999_DeleteDatabase()
        {
            bool bRts;

            bRts = DatabaseHelpers.EnsureDeleted();
            Assert.IsTrue(bRts);

            bRts = DatabaseHelpers.EnsureDeleted();
            Assert.IsFalse(bRts);
        }
    }
}
