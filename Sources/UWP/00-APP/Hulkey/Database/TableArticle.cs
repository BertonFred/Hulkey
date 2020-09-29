
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
    public class TableArticle
    {
        public static void CreationDesProduits(ListBox lbOutput)
        {
            Log.Info("Début CreationDesProduits");

            using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
            {
                ProduitRepository repo = uow.GetRepositoryArticle();
                int iArticleID = 1;
                for (int i = 1; i < 10; i++)
                {
                    iArticleID++;
                    repo.Create(Create_Produit(iArticleID, "Produit entrée",iCategorieID: 1, iTVAID:1));
                }

                for (int i = 1; i < 10; i++)
                {
                    iArticleID++;
                    repo.Create(Create_Produit(iArticleID, "Produit Plat", iCategorieID: 2, iTVAID: 1));
                }

                for (int i = 1; i < 10; i++)
                {
                    iArticleID++;
                    repo.Create(Create_Produit(iArticleID, "Produit Desert", iCategorieID: 3, iTVAID: 1));
                }

                int icount = uow.SaveChanges();
            }

            Log.Info("Fin CreationDesProduits");
        }

        private static Produit Create_Produit(int iID, string sLib, int iCategorieID, int iTVAID)
        {
            Produit obj = new Produit()
            {
                ID = iID,
                Version = 1,
                CategorieID = iCategorieID,
                Composition = eProduitComposition.NonCompose,
                Parent = null,
                Name = $"{sLib}{iID}",
                Description = $"{sLib} {iID}",
                PrixAchatHT = iID + 10,
                PrixVenteHT = iID + 12,
                TVAID = iTVAID,
                PrixAchatTTC = (iID + 10) * 1.20m,
                PrixVenteTTC = (iID + 12) * 1.20m,
                CreatedBy = "Hulkey",
                CreatedOn = DateTime.Now

            };
            return obj;
        }
    }
}
