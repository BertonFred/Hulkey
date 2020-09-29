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
    public class CarteItemRepository : Repository<CarteItem,CarteItemListItemDTO>
    {
        public CarteItemRepository()
            : base(null)
        {
        }
        public CarteItemRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// Retourne une liste de CarteElement, pour un affichage liste
        /// </summary>
        public override List<CarteItemListItemDTO> GetList(string SearchText = null)
        {
            List<CarteItemListItemDTO> lst;
            IQueryable<CarteItem> query = FindAll();
            query = query.Where(a => a.Deleted == false);
            lst = query.Select(a => new CarteItemListItemDTO()
                        {
                            ID = a.ID,
                CarteID = a.CarteID,
                ParentID = a.ParentID,
                            Ordre = a.Ordre,
                        })
                        .ToList();

            return lst;
        }
    }
}
