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
    public class CarteRepository : Repository<Carte,CarteListItemDTO>
    {
        public CarteRepository()
            : base(null)
        {
        }
        public CarteRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// Retourne une liste de Carte, pour un affichage liste
        /// </summary>
        public override List<CarteListItemDTO> GetList(string SearchText = null)
        {
            List<CarteListItemDTO> lst;
            IQueryable<Carte> query = FindAll();
            query = query.Where(a => a.Deleted == false);
            lst = query.Select(a => new CarteListItemDTO()
            {
                ID = a.ID,
                Name = a.Name,
                ActiveCaisse = a.ActiveCaisse,
                Ordre = a.Ordre,
            })
                        .ToList();

            return lst;
        }
    }
}
