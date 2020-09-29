using Hulkey.DAL.DTO;
using Hulkey.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hulkey.DAL.Repository
{
    public class UtilisateurRepository : Repository<Utilisateur,UtilisateurListItemDTO>
    {
        public UtilisateurRepository()
            : base(null)
        {
        }
        public UtilisateurRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// Retourne true si le password est celui de l'utilisateur specifié par son ID.
        /// Le user ne doit pas être marqué deleted
        /// </summary>
        /// <param name="iUserID">ID de l'utilisateur</param>
        /// <param name="sPassword">Le password a verifier</param>
        /// <returns>true si le password est le bon</returns>
        public bool CheckPassword(int iUserID, string sPassword)
        {
            return this.Set.Any(u => u.ID == iUserID && u.Deleted == false && u.Password == sPassword);
        }

        /// <summary>
        /// Retourne une liste de TVA, pour un affichage liste
        /// </summary>
        public override List<UtilisateurListItemDTO> GetList(string SearchText = null)
        {
            List<UtilisateurListItemDTO> lst;
            IQueryable<Utilisateur> query = FindAll();
            query = query.Where(a => a.Deleted == false);
            if (SearchText != null)
                query = query.Where(a => a.Nom.ToUpper().Contains(SearchText.ToUpper()) == true);
            lst = query.OrderBy(a => a.Nom)
                        .Select(a => new UtilisateurListItemDTO()
                        {
                            ID = a.ID,
                            Nom = a.Nom,
                            Prenom = a.Prenom,
                            Password = a.Password,
                            eMail = a.eMail,
                            Telephonne = a.Telephonne,
                            Role = a.Role
                        })
                        .ToList();

            return lst;
        }
    }
}
