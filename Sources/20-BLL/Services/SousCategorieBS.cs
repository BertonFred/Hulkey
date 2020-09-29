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
    /// Services SousCategories
    /// </summary>
    public sealed class SousCategorieBS : BusinessService<SousCategorie,SousCategorieListItemDTO,SousCategorieRepository>
    {
        public SousCategorieBS(IUserContext _UserContext)
            : base(_UserContext,new HulkeyUnitOfWork())
        {
            ActivateGetListCache = true;
        }

        public SousCategorieBS(IUserContext _UserContext, IUnitOfWork _uow) 
            : base(_UserContext,_uow)
        {
            ActivateGetListCache = true;
        }

        /// <summary>
        /// Retourne la liste des sous categorie associée à une categorie
        /// </summary>
        /// <param name="iCategorieID">La categorie</param>
        /// <returns>La liste des sous categories associées à la categorie</returns>
        public List<SousCategorie> GetListForCategorie(int iCategorieID)
        {
            List<SousCategorie> lst;

            try
            {
                Log.Trace($"SousCategorieBS GetListForCategorie iCategorieID={iCategorieID}");

                var repo = this.uow.GetRepository<SousCategorieRepository>();
                lst = repo.GetListForCategorie(iCategorieID);
            }
            catch (Exception e)
            {
                Log.Error($"Probléme pendant la recherche des données GetListForCategorie iCategorieID={iCategorieID}", e);
                lst = new List<SousCategorie>();
            }

            return lst;
        }
    }
}

