using Hulkey.DAL;
using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TU_Metiers
{
    /// <summary>
    /// Cette permet la gestion des Categorie / Sous Categorie
    /// pour un prefixe donné de categories
    /// On peut : Creer ou Charger des categories
    /// </summary>
    public class CategoriesSeeding
    {
        public CategoriesSeeding(HulkeyUnitOfWork _uow)
        {
            uow = _uow;
        }

        public void LoadCategories(string sCategoriePrefixe)
        {
            LoadCategorieEntree(sCategoriePrefixe);
            LoadCategoriePlat(sCategoriePrefixe);
            LoadCategorieDesert(sCategoriePrefixe);
            LoadCategorieMenu(sCategoriePrefixe);
        }

        private void LoadCategorieEntree(string sCategoriePrefixe)
        {
            var repoCategorie = uow.GetRepository<CategorieRepository>();
            Categorie categorie;
            var repoSousCategorie = uow.GetRepository<SousCategorieRepository>();
            SousCategorie sousCategorie;

            categorie = repoCategorie.FindBy(i => i.Name.Equals($"{sCategoriePrefixe}ENTREES") == true).FirstOrDefault();
            if (categorie == null)
            {
                // Si les entrée n'existe pas, on lance le process de creation des toutes les caegories
                CreateCategories(sCategoriePrefixe);
                categorie = repoCategorie.FindBy(i => i.Name.Equals($"{sCategoriePrefixe}ENTREES") == true).FirstOrDefault();
            }
            EntreeID = categorie.ID;

            sousCategorie = repoSousCategorie.FindBy(i => i.CategorieID == EntreeID && i.Name.Equals("SOUPE") == true).FirstOrDefault();
            EntreeSoupeID = sousCategorie.ID;
            sousCategorie = repoSousCategorie.FindBy(i => i.CategorieID == EntreeID && i.Name.Equals("SALADE") == true).FirstOrDefault();
            EntreeSaladeID = sousCategorie.ID;
            sousCategorie = repoSousCategorie.FindBy(i => i.CategorieID == EntreeID && i.Name.Equals("CHAUDE") == true).FirstOrDefault();
            EntreeChaudeID = sousCategorie.ID;
        }

        private void LoadCategoriePlat(string sCategoriePrefixe)
        {
            var repoCategorie = uow.GetRepository<CategorieRepository>();
            Categorie categorie;
            var repoSousCategorie = uow.GetRepository<SousCategorieRepository>();
            SousCategorie sousCategorie;

            categorie = repoCategorie.FindBy(i => i.Name.Equals($"{sCategoriePrefixe}PLATS") == true).FirstOrDefault();
            PlatID = categorie.ID;
            sousCategorie = repoSousCategorie.FindBy(i => i.CategorieID == PlatID && i.Name.Equals("VIANDES") == true).FirstOrDefault();
            PlatViandeID = sousCategorie.ID;
            sousCategorie = repoSousCategorie.FindBy(i => i.CategorieID == PlatID && i.Name.Equals("POISSONS") == true).FirstOrDefault();
            PlatPoissonID = sousCategorie.ID;
            sousCategorie = repoSousCategorie.FindBy(i => i.CategorieID == PlatID && i.Name.Equals("PATES") == true).FirstOrDefault();
            PlatPateID = sousCategorie.ID;
        }

        private void LoadCategorieDesert(string sCategoriePrefixe)
        {
            var repoCategorie = uow.GetRepository<CategorieRepository>();
            Categorie categorie;
            var repoSousCategorie = uow.GetRepository<SousCategorieRepository>();
            SousCategorie sousCategorie;

            categorie = repoCategorie.FindBy(i => i.Name.Equals($"{sCategoriePrefixe}DESERTS") == true).FirstOrDefault();
            DesertID = categorie.ID;
            sousCategorie = repoSousCategorie.FindBy(i => i.CategorieID == DesertID && i.Name.Equals("DESERTS") == true).FirstOrDefault();
            DesertDesertID = sousCategorie.ID;
            sousCategorie = repoSousCategorie.FindBy(i => i.CategorieID == DesertID && i.Name.Equals("GLACES") == true).FirstOrDefault();
            DesertGlaceID = sousCategorie.ID;
        }

        private void LoadCategorieMenu(string sCategoriePrefixe)
        {
            var repoCategorie = uow.GetRepository<CategorieRepository>();
            Categorie categorie;
            var repoSousCategorie = uow.GetRepository<SousCategorieRepository>();
            SousCategorie sousCategorie;

            categorie = repoCategorie.FindBy(i => i.Name.Equals($"{sCategoriePrefixe}MENUS") == true).FirstOrDefault();
            MenuID = categorie.ID;
            sousCategorie = repoSousCategorie.FindBy(i => i.CategorieID == MenuID && i.Name.Equals("MMIDI") == true).FirstOrDefault();
            MenuMidiID = sousCategorie.ID;
            sousCategorie = repoSousCategorie.FindBy(i => i.CategorieID == MenuID && i.Name.Equals("MSOIR") == true).FirstOrDefault();
            MenuSoirID = sousCategorie.ID;
        }

        public void CreateCategories(string sCategoriePrefixe)
        {
            CreateCategorieEntree(sCategoriePrefixe);
            CreateCategoriePlat(sCategoriePrefixe);
            CreateCategorieDesert(sCategoriePrefixe);
            CreateCategorieMenu(sCategoriePrefixe);
        }

        private void CreateCategorieEntree(string sCategoriePrefixe)
        {
            EntreeID = CreateCategorie(10,$"{sCategoriePrefixe}ENTREES", "Nos Entrees");
            EntreeSoupeID = CreateSousCategorie(EntreeID, 10, "SOUPE", "Les soupes");
            EntreeSaladeID = CreateSousCategorie(EntreeID, 20, "SALADE", "Les salades");
            EntreeChaudeID = CreateSousCategorie(EntreeID, 30, "CHAUDE", "Les entrées Chaude");
        }

        private void CreateCategoriePlat(string sCategoriePrefixe)
        {
            PlatID = CreateCategorie(20,$"{sCategoriePrefixe}PLATS", "Nos Plats");
            PlatViandeID = CreateSousCategorie(PlatID, 10, "VIANDES", "Les viandes");
            PlatPoissonID = CreateSousCategorie(PlatID, 20, "POISSONS", "Les poissons");
            PlatPateID = CreateSousCategorie(PlatID, 30, "PATES", "Les pates");
        }

        private void CreateCategorieDesert(string sCategoriePrefixe)
        {
            DesertID = CreateCategorie(30,$"{sCategoriePrefixe}DESERTS", "Nos Deserts");
            DesertDesertID = CreateSousCategorie(DesertID, 10, "DESERTS", "Les deserts");
            DesertGlaceID = CreateSousCategorie(DesertID, 20, "GLACES", "Les glaces");
        }

        private void CreateCategorieMenu(string sCategoriePrefixe)
        {
            MenuID = CreateCategorie(40, $"{sCategoriePrefixe}MENUS", "Nos Menus");
            MenuMidiID = CreateSousCategorie(MenuID, 10, "MMIDI", "Menu du midi");
            MenuSoirID = CreateSousCategorie(MenuID, 20, "MSOIR", "Menu du soir");
        }

        /// <summary>
        /// Creation d'une catégorie
        /// </summary>
        /// <returns>l'ID de la categoirie</returns>
        private int CreateCategorie(int iOrdre,string sName, string sDescription)
        {
            var repo = uow.GetRepository<CategorieRepository>();
            Categorie categorie = new Categorie()
            {
                Version = 1,
                Name = sName,
                Description = sDescription,
                Ordre = iOrdre,
                CreatedBy = "Hulkey",
                CreatedOn = DateTime.Now
            };

            repo.Create(categorie);
            int irts = uow.SaveChanges();

            return categorie.ID;
        }

        /// <summary>
        /// Creation d'une sous categorie pour une categorie qui doit existé
        /// </summary>
        /// <returns>L'id de la sous categorie</returns>
        private int CreateSousCategorie(int iCategorieID, int iOrdre, string sName, string sDescription)
        {
            var repo = uow.GetRepository<SousCategorieRepository>();
            SousCategorie sousCategorie = new SousCategorie()
            {
                CategorieID = iCategorieID,
                Version = 1,
                Name = sName,
                Description = sDescription,
                Ordre = iOrdre,
                CreatedBy = "Hulkey",
                CreatedOn = DateTime.Now
            };

            repo.Create(sousCategorie);
            int irts = uow.SaveChanges();
            return sousCategorie.ID;
        }

        public HulkeyUnitOfWork uow { get; set; }

        public  int EntreeID { get; set; }
        public  int EntreeSoupeID { get; set; }
        public  int EntreeSaladeID { get; set; }
        public  int EntreeChaudeID { get; set; }

        public  int PlatID { get; set; }
        public  int PlatViandeID { get; set; }
        public  int PlatPoissonID { get; set; }
        public  int PlatPateID { get; set; }

        public  int DesertID { get; set; }
        public  int DesertDesertID { get; set; }
        public  int DesertGlaceID { get; set; }
        public int MenuID { get; set; }
        public int MenuMidiID { get; set; }
        public int MenuSoirID { get; set; }
    }
}
