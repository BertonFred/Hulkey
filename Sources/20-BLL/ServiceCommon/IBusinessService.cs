using Hulkey.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulkey.SLL.ServiceCommon
{
    /// <summary>
    /// TODO il faudra rennomer ça en CRUDBusinessService
    /// </summary>
    public interface IBusinessService<T_DATA,T_DATALIST> 
        : IBusinessServiceBase
        where T_DATA : IPersistentObject
        where T_DATALIST : IListItemDTO
    {
        T_DATA Create(T_DATA data, bool bDoSaveChange = true);
        void Update(T_DATA data, bool bDoSaveChange = true);
        void DeleteById(int ID, bool bDoSaveChange = true);
        List<T_DATALIST> GetList(string SearchText = null);

        void SaveChanges();
        void RollbackChanges();

        void ClearCache();
    }

    public interface IBusinessServiceBase
    {
        IUnitOfWork uow { get; set; }
    }
}
