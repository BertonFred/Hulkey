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
    /// Services TVAs
    /// </summary>
    public sealed class TVABS : BusinessService<TVA,TVAListItemDTO,TVARepository>
    {
        public TVABS(IUserContext _UserContext)
            : base(_UserContext,new HulkeyUnitOfWork())
        {
            ActivateGetListCache = true;
        }

        public TVABS(IUserContext _UserContext, IUnitOfWork _uow) 
            : base(_UserContext,_uow)
        {
            ActivateGetListCache = true;
        }
    }
}

