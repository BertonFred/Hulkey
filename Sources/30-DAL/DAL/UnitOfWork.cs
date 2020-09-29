using Hulkey.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulkey.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DbContext context)
        {
            Context = context;

            //Context.Database.Log = Console.Write;
       //TODO $$$      Context.ConfigureLogging( = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public T_REPOSITORY GetRepository<T_REPOSITORY>()
            where T_REPOSITORY : IRepository, new()
        {
            T_REPOSITORY repo = new T_REPOSITORY() ;
            repo.UnitOfWork = this;
            return repo;
        }

        #region CONTEXT
        public DbContext Context { get; }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            var result = await Context.SaveChangesAsync();
            return result;
        }

        public void RollbackChanges()
        {
            Context
                .ChangeTracker
                .Entries()
                .ToList()
                .ForEach(x => x.Reload());
        }
        #endregion

        #region --- TRANSACTION --
        // Utilisation : 
        //HulkeyUnitOfWork uow = new HulkeyUnitOfWork();
        //uow.ExecuteInTransaction(u =>
        //    {
        //        var repo = u.GetRepository<CategorieRepository>();
        //        repo.Delete(1);
        //        u.SaveChanges();
        //        repo.DeleteById(2);
        //        u.SaveChanges();

        //        u.RollbackTransaction();
        //    });
        public int ExecuteInTransaction(Action<IUnitOfWork> actionInTransaction)
        {
            int iNbChanges = 0;

            try
            {
                if (CurrentTransaction != null)
                {
                    actionInTransaction(this);

                    iNbChanges = Context.SaveChanges();
                }
                else
                {
                    BeginTransaction();

                    actionInTransaction(this);

                    iNbChanges = Context.SaveChanges();

                    CommitTransaction();
                }
            }
            catch (Exception ex1)
            {
                // Attempt to roll back the transaction.
                Log.Error(ex1.Message);
                try
                {
                    RollbackTransaction();
                }
                catch (Exception ex2)
                {
                    Log.Error(ex2.Message);
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    return 0;
                }
                return 0;
            }

            return iNbChanges;
        }

        public void BeginTransaction()
        {
            if (CurrentTransaction == null)
                CurrentTransaction = Context.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            if (CurrentTransaction == null)
                return;
            CurrentTransaction.Commit();

            CurrentTransaction = null;
        }
        public void RollbackTransaction()
        {
            if (CurrentTransaction == null)
                return;
            CurrentTransaction.Rollback();
            CurrentTransaction = null;
        }
        public IDbContextTransaction CurrentTransaction { get; private set; }
        #endregion --- TRANSACTION --

        public string UserName { get; set; }

        #region dispose
        private bool Disposed { get; set; }

        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed) return;
            if (disposing)
            {
                try
                {
                    Log.Trace("UnitOfWork.Disposed");
                    Context?.Dispose();
                }
                catch (ObjectDisposedException)
                {
                    // do nothing, the objectContext has already been disposed
                }
            }

            Disposed = true;
        }

        #endregion

    }
}
