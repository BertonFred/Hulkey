using Hulkey.Common;
using Hulkey.DAL;
using Hulkey.DAL.DTO;
using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Hulkey.SLL.ServiceCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hulkey.SLL.Services
{
    /// <summary>
    /// Services TPFs
    /// </summary>
    public sealed class TPFBS : BusinessService<TPF,TPFListItemDTO,TPFRepository>
    {
        public TPFBS(IUserContext _UserContext)
            : base(_UserContext,new HulkeyUnitOfWork())
        {
            ActivateGetListCache = true;
        }

        public TPFBS(IUserContext _UserContext, IUnitOfWork _uow) 
            : base(_UserContext,_uow)
        {
            ActivateGetListCache = true;
        }
    }
}

