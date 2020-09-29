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
    public class TypePaiementRepository : Repository<TypePaiement,TypePaiementListItemDTO>
    {
        public TypePaiementRepository()
            : base(null)
        {
        }
        public TypePaiementRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// Retourne une liste de TypePaiement, pour un affichage liste
        /// </summary>
        public override List<TypePaiementListItemDTO> GetList(string SearchText = null)
        {
            List<TypePaiementListItemDTO> lst;
            IQueryable<TypePaiement> query = FindAll();
            query = query.Where(a => a.Deleted == false);
            if (SearchText != null)
                query = query.Where(a => a.Name.ToUpper().Contains(SearchText.ToUpper()) == true ||
                                         a.Code.ToUpper().Contains(SearchText.ToUpper()) == true);
            lst = query.OrderBy(a => a.Name)
                        .Select(a => new TypePaiementListItemDTO()
                        {
                            ID = a.ID,
                            Code = a.Code,
                            Name = a.Name,
                            Libelle = a.Libelle
                        })
                        .ToList();
            return lst;
        }
    }
}
