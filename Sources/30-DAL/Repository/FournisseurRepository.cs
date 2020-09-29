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
    public class FournisseurRepository : Repository<Fournisseur,FournisseurListItemDTO>
    {
        public FournisseurRepository()
            : base(null)
        {
        }

        public FournisseurRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// Retourne une liste de TVA, pour un affichage liste
        /// </summary>
        public override List<FournisseurListItemDTO> GetList(string SearchText = null)
        {
            List<FournisseurListItemDTO> lst;
            IQueryable<Fournisseur> query = FindAll();
            query = query.Where(a => a.Deleted == false);
            if (SearchText != null)
                query = query.Where(a => a.Name.ToUpper().Contains(SearchText.ToUpper()) == true);
            lst = query.OrderBy(a => a.Name)
                    .Select(a => new FournisseurListItemDTO()
                    {
                        ID = a.ID,
                        Name = a.Name,
                        Description = a.Description,
                        eMail = a.eMail,
                        Telephonne = a.Telephonne,
                        Telecopie = a.Telecopie,
                    })
                    .ToList();

            return lst;
        }
    }
}
