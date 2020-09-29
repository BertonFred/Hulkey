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
    public class TVARepository : Repository<TVA,TVAListItemDTO>
    {
        public TVARepository()
            : base(null)
        {
        }

        public TVARepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// Retourne une liste de TVA, pour un affichage liste
        /// </summary>
        public override List<TVAListItemDTO> GetList(string SearchText = null)
        {
            List<TVAListItemDTO> lst;
            IQueryable<TVA> query = FindAll();
            query = query.Where(a => a.Deleted == false);
            if (SearchText != null)
                query = query.Where(a => a.Name.ToUpper().Contains(SearchText.ToUpper()) == true ||
                                         a.Code.ToUpper().Contains(SearchText.ToUpper()) == true);
            lst = query.OrderBy(a => a.Name)
                        .Select(a => new TVAListItemDTO()
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
