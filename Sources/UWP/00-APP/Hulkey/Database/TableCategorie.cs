
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
    public class TableCategorie
    {
        public static void CreationsDesCategories(ListBox lbOutput)
        {
            Log.Info("Début CreationsDesCategories");

            using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
            {
                var repoCateg = uow.GetRepositoryCategorie();

                int iCategorieID = 1;
                repoCateg.Create(Create_Categorie(iCategorieID++, "Entrées"));
                repoCateg.Create(Create_Categorie(iCategorieID++, "Plats"));
                repoCateg.Create(Create_Categorie(iCategorieID++, "Deserts"));
                repoCateg.Create(Create_Categorie(iCategorieID++, "Boissons"));
                repoCateg.Create(Create_Categorie(iCategorieID++, "Menu"));

                int icount = uow.SaveChanges();
            }

            Log.Info("Fin CreationsDesCategories");
        }

        private static  Categorie Create_Categorie(int iID, string sLib)
        {
            Categorie obj = new Categorie()
            {
                ID = iID,
                Version = 1,
                Name = sLib.ToUpper(),
                Description = sLib,
                CreatedBy = "Hulkey",
                CreatedOn = DateTime.Now
            };

            return obj;
        }
    }
}
