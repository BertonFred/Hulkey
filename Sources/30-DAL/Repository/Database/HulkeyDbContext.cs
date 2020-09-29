using Hulkey.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hulkey.DAL.Repository
{
    /// <summary>
    /// Context de la base e données Hulkey
    /// gerant : SQLite
    /// </summary>
    public partial class HulkeyDbContext : DbContext
    {
        private string _connectionString = "Data Source=hulkey.db";

        public HulkeyDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public HulkeyDbContext()
        {
        }

        //
        // Summary:
        //     Override this method to configure the database (and other options) to be used
        //     for this context. This method is called for each instance of the context that
        //     is created.
        //     In situations where an instance of Microsoft.EntityFrameworkCore.DbContextOptions
        //     may or may not have been passed to the constructor, you can use Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured
        //     to determine if the options have already been set, and skip some or all of the
        //     logic in Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder).
        //
        // Parameters:
        //   optionsBuilder:
        //     A builder used to create or modify options for this context. Databases (and other
        //     extensions) typically define extension methods on this object that allow you
        //     to configure the context.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }

        public DbSet<Produit> Produits { get; set; }
        public DbSet<ProduitComposition> ProduitCompositions { get; set; }
        public DbSet<TVA> TVAs { get; set; }
        public DbSet<TPF> TPFs { get; set; }
        public DbSet<TypePaiement> TypePaiements { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<SousCategorie> SousCategories { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Carte> Cartes { get; set; }
        public DbSet<CarteItem> CarteElements { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<Journal> Journals { get; set; }
    }
}
