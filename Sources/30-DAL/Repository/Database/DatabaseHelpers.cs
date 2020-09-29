using Hulkey.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hulkey.DAL.Repository
{
    public class DatabaseHelpers
    {
        /// <summary>
        ///     Ensures that the database for the context exists. If it exists, no action is
        ///     taken. If it does not exist then the database and all its schema are created.
        ///     If the database exists, then no effort is made to ensure it is compatible with
        ///     the model for this context.
        ///     Note that this API does not use migrations to create the database. In addition,
        ///     the database that is created cannot be later updated using migrations. If you
        ///     are targeting a relational database and using migrations, you can use the DbContext.Database.Migrate()
        ///     method to ensure the database is created and all migrations are applied.
        /// </summary>
        /// <returns>
        /// True if the database is created, false if it already existed.
        /// </returns>
        public static bool EnsureCreated()
        {
            bool bRts;

            Log.Info("Verification de la présence de la base de données");

            using (HulkeyDbContext context = new HulkeyDbContext())
            {
                bRts = context.Database.EnsureCreated();
                if (bRts == true)
                    Log.Info("La base de données n'existe pas, elle est créee.");
                else
                    Log.Info("La base de données existe.");
            }

            return bRts;
        }

        /// <summary>
        /// Ensures that the database for the context does not exist. If it does not exist,
        /// no action is taken. If it does exist then the database is deleted.
        /// Warning: The entire database is deleted, and no effort is made to remove just
        /// the database objects that are used by the model for this context.
        /// </summary>
        /// <returns>True if the database is deleted, false if it did not exist.</returns>
        public static bool EnsureDeleted()
        {
            bool bRts;

            Log.Info("Suppression de la base de données");

            using (HulkeyDbContext context = new HulkeyDbContext())
            {
                bRts = context.Database.EnsureDeleted();
                if (bRts == true)
                    Log.Info("La base de données est supprimé.");
                else
                    Log.Info("La base de données n'existé pas.");
            }

            return bRts;
        }
    }
}
