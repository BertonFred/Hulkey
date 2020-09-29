using Hulkey.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hulkey.DAL.Repository
{
    /// <summary>
    /// Creation des données initiale de la base de données
    /// </summary>
    public partial class HulkeyDbContext : DbContext
    {
        //
        // Summary:
        //     Override this method to further configure the model that was discovered by convention
        //     from the entity types exposed in Microsoft.EntityFrameworkCore.DbSet`1 properties
        //     on your derived context. The resulting model may be cached and re-used for subsequent
        //     instances of your derived context.
        //
        // Parameters:
        //   modelBuilder:
        //     The builder being used to construct the model for this context. Databases (and
        //     other extensions) typically define extension methods on this object that allow
        //     you to configure aspects of the model that are specific to a given database.
        //
        // Remarks:
        //     If a model is explicitly set on the options for this context (via Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel))
        //     then this method will not be run.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InitUtilisateur(modelBuilder);
            InitTVA(modelBuilder);
            InitTPF(modelBuilder);
            InitTypePaiement(modelBuilder);
            InitCategorieAnsSouscategorie(modelBuilder);
        }

        private void InitUtilisateur(ModelBuilder modelbuilder)
        {
            int id = 1;

            modelbuilder.Entity<Utilisateur>()
                .HasData(new Utilisateur()
                {
                    ID = id++,
                    Nom = "BERTON",
                    Prenom = "Frederic",
                    Password = "1234",
                    eMail = "berton.frederic@gmail.com",
                    Role = eRoleUtilisateur.Administrateur,
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });

            modelbuilder.Entity<Utilisateur>()
                .HasData(new Utilisateur()
                {
                    ID = id++,
                    Nom = "AUGRY",
                    Prenom = "Julian",
                    Password = "1234",
                    eMail = "Augry.Julian@gmail.com",
                    Role = eRoleUtilisateur.Administrateur,
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });

            for(int i=0;i<10;i++)
            {
                modelbuilder.Entity<Utilisateur>()
                    .HasData(new Utilisateur()
                    {
                        ID = id+i,
                        Nom = $"USER{i}",
                        Prenom = $"prenom{i}",
                        Password = "1234",
                        Role = eRoleUtilisateur.ResponsableSalle,
                        CreatedBy = "Hulkey",
                        CreatedOn = DateTime.Now
                    });
            }
        }

        private void InitTypePaiement(ModelBuilder modelBuilder)
        {
            int iID = 1;

            modelBuilder.Entity<TypePaiement>()
                .HasData(new TypePaiement()
                {
                    ID = iID++,
                    Version = 1,
                    Code = "Cash",
                    Name = "Cash",
                    Libelle = "Argent liquide",
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });

            modelBuilder.Entity<TypePaiement>()
                .HasData(new TypePaiement()
                {
                    ID = iID++,
                    Version = 1,
                    Code = "CB",
                    Name = "CB",
                    Libelle = "Carte Bleu",
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });

            modelBuilder.Entity<TypePaiement>()
                .HasData(new TypePaiement()
                {
                    ID = iID++,
                    Version = 1,
                    Code = "CHQ",
                    Name = "CHQ",
                    Libelle = "Chéque",
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });

            modelBuilder.Entity<TypePaiement>()
                .HasData(new TypePaiement()
                {
                    ID = iID++,
                    Version = 1,
                    Code = "TR",
                    Name = "TR",
                    Libelle = "Ticket Restaurant",
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });
        }

        /// <summary>
        /// Initialisation de la table de TPF
        /// </summary>
        protected void InitTPF(ModelBuilder modelBuilder)
        {
        }

        /// <summary>
        /// Initialisation de la table de TVA
        /// </summary>
        protected void InitTVA(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TVA>()
                .HasData(new TVA()
                {
                    ID = 1,
                    Version = 1,
                    Code="TN",
                    Name = "Taux normal (le plus utilisé)",
                    Taux = 20.0M,
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });
            modelBuilder.Entity<TVA>()
                .HasData(new TVA()
                {
                    ID = 2,
                    Version = 1,
                    Code = "TSR",
                    Name = "Taux super-réduit (médicaments, presse)",
                    Taux = 2.10M,
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });
            modelBuilder.Entity<TVA>()
                .HasData(new TVA()
                {
                    ID = 3,
                    Version = 1,
                    Code = "TR",
                    Name = "Taux réduit (alimentaire première nécessité, gaz, électricité, ...)",
                    Taux = 5.50M,
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });
            modelBuilder.Entity<TVA>()
                .HasData(new TVA()
                {
                    ID = 4,
                    Version = 1,
                    Code = "TI",
                    Name = "Taux intermédiaire (restauration, hôtellerie, ...)",
                    Taux = 10.0M,
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });
        }

        /// <summary>
        /// Initialisation de la table de Categorie et sous categorie
        /// </summary>
        protected void InitCategorieAnsSouscategorie(ModelBuilder modelBuilder)
        {
            int idCategorie = 1;
            int idSousCategorie = 1;

            // Entrees
            modelBuilder.Entity<Categorie>()
                .HasData(new Categorie()
                {
                    ID = idCategorie,
                    Version = 1,
                    Name = "ENTREES",
                    Description = "Entrées",
                    Ordre = idCategorie,
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });

            modelBuilder.Entity<SousCategorie>()
                .HasData(new SousCategorie()
                {
                    ID = idSousCategorie,
                    CategorieID = idCategorie,
                    Version = 1,
                    Name = "SOUPES",
                    Description = "Soupes",
                    Ordre = idSousCategorie,
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });

            idSousCategorie++;
            modelBuilder.Entity<SousCategorie>()
                .HasData(new SousCategorie()
                {
                    ID = idSousCategorie,
                    CategorieID = idCategorie,
                    Version = 1,
                    Name = "SALADES",
                    Description = "Salades",
                    Ordre = idSousCategorie,
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });

            idSousCategorie++;
            modelBuilder.Entity<SousCategorie>()
                .HasData(new SousCategorie()
                {
                    ID = idSousCategorie,
                    CategorieID = idCategorie,
                    Version = 1,
                    Name = "ENTREECHAUDES",
                    Description = "Entrée chaudes",
                    Ordre = idSousCategorie,
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });


            // Plats
            idCategorie++;
            modelBuilder.Entity<Categorie>()
                .HasData(new Categorie()
                {
                    ID = idCategorie,
                    Version = 1,
                    Name = "PLATS",
                    Description = "Plats",
                    Ordre = idCategorie,
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });

            idSousCategorie++;
            modelBuilder.Entity<SousCategorie>()
                .HasData(new SousCategorie()
                {
                    ID = idSousCategorie,
                    CategorieID = idCategorie,
                    Version = 1,
                    Name = "VIANDES",
                    Description = "Viandes",
                    Ordre = idSousCategorie,
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });

            idSousCategorie++;
            modelBuilder.Entity<SousCategorie>()
                .HasData(new SousCategorie()
                {
                    ID = idSousCategorie,
                    CategorieID = idCategorie,
                    Version = 1,
                    Name = "POISSONS",
                    Description = "Possons",
                    Ordre = idSousCategorie,
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });
            idSousCategorie++;
            modelBuilder.Entity<SousCategorie>()
                .HasData(new SousCategorie()
                {
                    ID = idSousCategorie,
                    CategorieID = idCategorie,
                    Version = 1,
                    Name = "PATES",
                    Description = "Pates",
                    Ordre = idSousCategorie,
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });

            // Deserts
            idCategorie++;
            modelBuilder.Entity<Categorie>()
                .HasData(new Categorie()
                {
                    ID = idCategorie,
                    Version = 1,
                    Name = "DESERTS",
                    Description = "Deserts",
                    Ordre = idCategorie,
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });
            idSousCategorie++;
            modelBuilder.Entity<SousCategorie>()
                .HasData(new SousCategorie()
                {
                    ID = idSousCategorie,
                    CategorieID = idCategorie,
                    Version = 1,
                    Name = "GATEAUX",
                    Description = "Gateaux",
                    Ordre = idSousCategorie,
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });
            idSousCategorie++;
            modelBuilder.Entity<SousCategorie>()
                .HasData(new SousCategorie()
                {
                    ID = idSousCategorie,
                    CategorieID = idCategorie,
                    Version = 1,
                    Name = "GLACES",
                    Description = "Glaces",
                    Ordre = idSousCategorie,
                    CreatedBy = "Hulkey",
                    CreatedOn = DateTime.Now
                });
        }
    }
}
