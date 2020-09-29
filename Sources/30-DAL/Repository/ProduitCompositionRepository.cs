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
    public class ProduitCompositionRepository : Repository<ProduitComposition, ProduitCompositionListItemDTO>
    {
        public ProduitCompositionRepository()
            : base(null)
        {
        }

        public ProduitCompositionRepository(IUnitOfWork uow)
            : base(uow)
        {
        }
        
        public override List<ProduitCompositionListItemDTO> GetList(string SearchText = null)
        {
            List<ProduitCompositionListItemDTO> lst;
            IQueryable<ProduitComposition> query = FindAll();
            query = query.Where(a => a.Deleted == false);
            if (SearchText != null)
                query = query.Where(a => a.ParentID.ToString().Contains(SearchText.ToUpper()) == true);
            lst = query.OrderBy(a => a.ParentID)
                        .Select(a => new ProduitCompositionListItemDTO()
                        {
                            ID = a.ParentID,
                            ParentID = a.ParentID,
                            EnfantID = a.EnfantID,
                            Ordre = a.Ordre,
                            Quantite = a.Quantite
                        })
                        .ToList();
            return lst;
        }

        /// <summary>
        /// Retourne la liste de composition d'un produit
        /// </summary>
        /// <param name="iParentID">le produit parent de la composition</param>
        /// <returns>La composition du produit</returns>
        public  List<ProduitComposition> GetListProduitComposition(int iParentID)
        {
            List<ProduitComposition> lst;
            lst = FindBy(a => a.Deleted == false && a.ParentID == iParentID)
                    .Include(c => c.Enfant)
                    .OrderBy(a => a.Ordre)
                    .ToList();
            return lst;
        }
    }
}
