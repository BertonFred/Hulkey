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
    /// Services Utilisateurs
    /// </summary>
    public sealed class UtilisateurBS : BusinessService<Utilisateur,UtilisateurListItemDTO,UtilisateurRepository>
    {
        public UtilisateurBS(IUserContext _UserContext)
            : base(_UserContext,new HulkeyUnitOfWork())
        {
        }

        public UtilisateurBS(IUserContext _UserContext, IUnitOfWork _uow) 
            : base(_UserContext,_uow)
        {
        }

        /// <summary>
        /// Retourne true si l'utilisateur specifié par son ID utilise le mot de passé indiqué
        /// </summary>
        /// <param name="iUserID">L'ID de l'utilisateur >0</param>
        /// <param name="sPassword">Le password à verifié</param>
        /// <returns>True si le password est celui de l'utilisateur</returns>
        public bool CheckPassword(int iUserID, string sPassword)
        {
            bool bCheckPassword;
            
            try
            {
                var repo = this.uow.GetRepository<UtilisateurRepository>();
                bCheckPassword = repo.CheckPassword(iUserID, sPassword);
            }
            catch (Exception e)
            {
                Log.Error($"Probléme pendant la suppresion de l'utilisateur : {iUserID}", e);
                bCheckPassword = false;
            }

            return bCheckPassword;
        }
    }
}

