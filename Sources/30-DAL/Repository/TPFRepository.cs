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
    public class TPFRepository : Repository<TPF,TPFListItemDTO>
    {
        public TPFRepository()
            : base(null)
        {
        }
        public TPFRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// Retourne une liste de TPF, pour un affichage liste
        /// </summary>
        public override List<TPFListItemDTO> GetList(string SearchText = null)
        {
            List<TPFListItemDTO> lst;
            IQueryable<TPF> query = FindAll();
            query = query.Where(a => a.Deleted == false);
            if (SearchText != null)
                query = query.Where(a => a.Name.ToUpper().Contains(SearchText.ToUpper()) == true ||
                                         a.Code.ToUpper().Contains(SearchText.ToUpper()) == true);
            lst = query.OrderBy(a => a.Name)
                        .Select(a => new TPFListItemDTO()
                        {
                            ID = a.ID,
                            Code = a.Code,
                            Name = a.Name,
                            Taux = a.Taux
                        })
                        .ToList();
            return lst;
        }
    }
}
