using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulkey.DAL
{
    public interface IUnitOfWork : IDisposable 
    {
        DbContext Context { get; }

        T_REPOSITORY GetRepository<T_REPOSITORY>()
            where T_REPOSITORY : IRepository, new();

        int SaveChanges();
        void RollbackChanges();

        int ExecuteInTransaction(Action<IUnitOfWork> actionInTransaction);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        IDbContextTransaction CurrentTransaction { get; }

        string UserName { get; set; }
    }
}
