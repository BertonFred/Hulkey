using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hulkey.DAL
{
    public interface IRepository<T_ENTITY, T_DATALIST> : IRepository
        where T_ENTITY : class
        where T_DATALIST : IListItemDTO
    {
        T_ENTITY Create(T_ENTITY entity);
        Task<T_ENTITY> CreateAsync(T_ENTITY entity);

        T_ENTITY Read(int Id);
        Task<T_ENTITY> ReadAsync(int Id);

        void Update(T_ENTITY entity);
        void Delete(T_ENTITY entity);
        void DeleteById(int Id);
        Task DeleteByIdAsync(int Id);

        IQueryable<T_ENTITY> FindAll();
        IQueryable<T_ENTITY> FindBy(System.Linq.Expressions.Expression<Func<T_ENTITY, bool>> predicate);

        List<T_DATALIST> GetList(string SearchText = null);
    }

    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; set; }
    }
}
