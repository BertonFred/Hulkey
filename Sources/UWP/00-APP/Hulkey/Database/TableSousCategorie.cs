
using System;
using Hulkey.DAL;
using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Windows.UI.Xaml.Controls;
using Hulkey.Common;

namespace Hulkey.CreateDatabase
{
    public class TableSousCategorie
    {
        public static void CreationsDesSousCategories(ListBox lbOutput)
        {
            Log.Info("Début CreationsDesSousCategories");

            using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
            {
                LoadCategories(uow);
                var repoSubCateg = uow.GetRepositorySousCategorie();

                int iSousCategorieID = 1;
                repoSubCateg.Create(Create_SousCategorie(iSousCategorieID++, idCategorieEntrees, "Entrées froides"));
                repoSubCateg.Create(Create_SousCategorie(iSousCategorieID++, idCategorieEntrees, "Entrées Chaude"));
                repoSubCateg.Create(Create_SousCategorie(iSousCategorieID++, idCategoriePlats, "Burger"));
                repoSubCateg.Create(Create_SousCategorie(iSousCategorieID++, idCategoriePlats, "Viandes"));
                repoSubCateg.Create(Create_SousCategorie(iSousCategorieID++, idCategoriePlats, "Poissons"));
                repoSubCateg.Create(Create_SousCategorie(iSousCategorieID++, idCategorieDeserts, "Glaces"));
                repoSubCateg.Create(Create_SousCategorie(iSousCategorieID++, idCategorieDeserts, "Gateaux"));
                repoSubCateg.Create(Create_SousCategorie(iSousCategorieID++, idCategorieBoissons, "Non alcolisé"));
                repoSubCateg.Create(Create_SousCategorie(iSousCategorieID++, idCategorieBoissons, "Alcolisé"));
                repoSubCateg.Create(Create_SousCategorie(iSousCategorieID++, idCategorieBoissons, "Chaude"));
                repoSubCateg.Create(Create_SousCategorie(iSousCategorieID++, idCategorieMenu, "Menu Midi"));
                repoSubCateg.Create(Create_SousCategorie(iSousCategorieID++, idCategorieMenu, "Menu Soir"));
                repoSubCateg.Create(Create_SousCategorie(iSousCategorieID++, idCategorieMenu, "Menu Luxe"));

                int icount = uow.SaveChanges();
            }

            Log.Info("Fin CreationsDesSousCategories");
        }

        private static  SousCategorie Create_SousCategorie(int iID, int idCategorie, string sLib)
        {
            SousCategorie obj = new SousCategorie()
            {
                ID = iID,
                CategorieID = idCategorie,
                Version = 1,
                Name = sLib.ToUpper(),
                Description = sLib,
                CreatedBy = "Hulkey",
                CreatedOn = DateTime.Now
            };

            return obj;
        }

        static int idCategorieEntrees;
        static int idCategoriePlats;
        static int idCategorieDeserts;
        static int idCategorieBoissons;
        static int idCategorieMenu;

        private static void LoadCategories(HulkeyUnitOfWork uow)
        {
            var repoCateg = uow.GetRepositoryCategorie();

            idCategorieEntrees = repoCateg.FindBy(c => c.Name.Contains("Entrées")).Select(i=>i.ID).FirstOrDefault();
            idCategoriePlats = repoCateg.FindBy(c => c.Name.Contains("Plats")).Select(i => i.ID).FirstOrDefault();
            idCategorieDeserts = repoCateg.FindBy(c => c.Name.Contains("Deserts")).Select(i => i.ID).FirstOrDefault();
            idCategorieBoissons = repoCateg.FindBy(c => c.Name.Contains("Boissons")).Select(i => i.ID).FirstOrDefault();
            idCategorieMenu = repoCateg.FindBy(c => c.Name.Contains("Menu")).Select(i => i.ID).FirstOrDefault();
        }
    }
}

