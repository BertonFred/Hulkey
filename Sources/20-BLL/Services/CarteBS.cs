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
    /// Services gestion d'une carte de restaurant
    /// </summary>
    public sealed class CarteBS : BusinessService<Carte,CarteListItemDTO,CarteRepository>
    {
        public CarteBS(IUserContext _UserContext)
            : base(_UserContext,new HulkeyUnitOfWork())
        {
            ActivateGetListCache = false;
        }

        public CarteBS(IUserContext _UserContext, IUnitOfWork _uow) 
            : base(_UserContext,_uow)
        {
            ActivateGetListCache = false;
        }
    }
}

