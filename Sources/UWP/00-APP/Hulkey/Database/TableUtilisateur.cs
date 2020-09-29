
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
    public class TableUtilisateur
    {
        public static void CreationDesUtilisateurs(ListBox lbOutput)
        {
            Log.Info("Début CreationDesUtilisateurs");

            using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
            {
                var repo = uow.GetRepositoryUtilisateur();

                int iID = 1;
                repo.Create(Create_Utilisateur(iID++, "berton"));
                repo.Create(Create_Utilisateur(iID++, "bertin"));
                repo.Create(Create_Utilisateur(iID++, "perdoux"));
                repo.Create(Create_Utilisateur(iID++, "augry"));
                repo.Create(Create_Utilisateur(iID++, "test"));
                repo.Create(Create_Utilisateur(iID++, "bob"));

                int icount = uow.SaveChanges();
            }

            Log.Info("Fin CreationDesUtilisateurs");
        }

        private static Utilisateur Create_Utilisateur(int iID, string Text)
        {
            return new Utilisateur()
            {
                ID = iID,
                Nom = $"{Text} {iID}",
                Prenom = $"Prénom {Text} {iID}",
                Password = $"{iID}",
                eMail = $"email {Text} {iID}",
                Telephonne = $"{iID}-{iID}-{iID}-{iID}",
            };
        }
    }
}
