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
    public class ClientRepository : Repository<Client,ClientListItemDTO>
    {
        public ClientRepository()
            : base(null)
        {
        }

        public ClientRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// Retourne une liste des clients, pour un affichage liste
        /// </summary>
        public override List<ClientListItemDTO> GetList(string SearchText = null)
        {
            List<ClientListItemDTO> lst;
            IQueryable<Client> query = FindAll();
            query = query.Where(a => a.Deleted == false);
            if (SearchText != null)
                query = query.Where(a => a.Nom.ToUpper().Contains(SearchText.ToUpper()) == true);
            lst = query.OrderBy(a => a.Nom)
                    .Select(a => new ClientListItemDTO()
                    {
                        ID = a.ID,
                        Nom = a.Nom,
                        Prenom = a.Prenom,
                        eMail = a.eMail,
                        TelephonneFixe = a.TelephonneFixe,
                        TelephonneMobile = a.TelephonneMobile,
                    })
                    .ToList();

            return lst;
        }
    }
}
