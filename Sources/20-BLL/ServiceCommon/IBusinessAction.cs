using Hulkey.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulkey.SLL.ServiceCommon
{
    public interface IBusinessAction
    {
        bool CanExecute();
        bool Execute();

        IBusinessServiceBase ParentBS { get; set; }
        IUnitOfWork uow { get; }
    }
}
