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
    public class SousCategorieRepository : Repository<SousCategorie,SousCategorieListItemDTO>
    {
        public SousCategorieRepository()
            : base(null)
        {
        }

        public SousCategorieRepository(IUnitOfWork uow)
            : base(uow)
        {
        }
        /// <summary>
        /// Retourne une liste de TVA, pour un affichage liste
        /// </summary>
        public override List<SousCategorieListItemDTO> GetList(string SearchText = null)
        {
            List<SousCategorieListItemDTO> lst;
            IQueryable<SousCategorie> query = FindAll();
            query = query.Where(a => a.Deleted == false);
            if (SearchText != null)
                query = query.Where(a => a.Name.ToUpper().Contains(SearchText.ToUpper()) == true);
            lst = query.OrderBy(a => a.Name)
                    .Include(i=>i.Categorie)
                    .Select(a => new SousCategorieListItemDTO()
                    {
                        ID = a.ID,
                        CategorieID = a.CategorieID, 
                        CategorieName = a.Categorie.Name,
                        Name = a.Name,
                        Description = a.Description,
                        Ordre = a.Ordre,
                    })
                    .ToList();

            return lst;
        }

        /// <summary>
        /// Retourne la liste des sous categorie associée à une categorie
        /// </summary>
        /// <param name="iCategorieID">La categorie</param>
        /// <returns>La liste des sous categories associées à la categorie</returns>
        public List<SousCategorie> GetListForCategorie(int iCategorieID)
        {
            List<SousCategorie> lst;

            lst = FindAll()
                    .Where(a => a.Deleted == false && a.CategorieID == iCategorieID)
                    .OrderBy(a => a.Ordre)
                    .ToList();

            return lst;
        }

    }
}
