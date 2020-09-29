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
    public class JournalRepository : Repository<Journal,JournalListItemDTO>
    {
        public JournalRepository()
            : base(null)
        {
        }
        public JournalRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// Retourne une liste de Journal, pour un affichage liste
        /// </summary>
        public override List<JournalListItemDTO> GetList(string SearchText = null)
        {
            List<JournalListItemDTO> lst;
            IQueryable<Journal> query = FindAll();
            query = query.Where(a => a.Deleted == false);
            if (SearchText != null)
                query = query.Where(a => a.Action.ToUpper().Contains(SearchText.ToUpper()) == true);
            lst = query.OrderBy(a => a.CreatedBy)
                    .Select(a => new JournalListItemDTO()
                    {
                        ID = a.ID,
                        Action = a.Action,
                        Parametre = a.Parametre,
                        CreatedBy = a.CreatedBy,
                        CreatedOn = a.CreatedOn,
                    })
                    .ToList();

            return lst;
        }
    }
}
