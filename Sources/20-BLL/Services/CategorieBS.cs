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
    /// Services Categories
    /// </summary>
    public sealed class CategorieBS : BusinessService<Categorie,CategorieListItemDTO,CategorieRepository>
    {
        public CategorieBS(IUserContext _UserContext)
            : base(_UserContext,new HulkeyUnitOfWork())
        {
            ActivateGetListCache = true;
        }

        public CategorieBS(IUserContext _UserContext, IUnitOfWork _uow) 
            : base(_UserContext,_uow)
        {
            ActivateGetListCache = true;
        }
    }
}

