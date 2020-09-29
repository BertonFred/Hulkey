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
    public class CategorieRepository : Repository<Categorie,CategorieListItemDTO>
    {
        public CategorieRepository()
            : base(null)
        {
        }

        public CategorieRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        public Categorie ReadWithSousCategorie(int Id)
        {
            return Set.Where(i => i.ID == Id)
                      .Include("SousCategories")  
                      .FirstOrDefault();
        }

        /// <summary>
        /// Retourne une liste de TVA, pour un affichage liste
        /// </summary>
        public override List<CategorieListItemDTO> GetList(string SearchText = null)
        {
            List<CategorieListItemDTO> lst;
            IQueryable<Categorie> query = FindAll();
            query = query.Where(a => a.Deleted == false);
            if (SearchText != null)
                query = query.Where(a => a.Name.ToUpper().Contains(SearchText.ToUpper()) == true );
            lst = query.OrderBy(a => a.Name)
                    .Select(a => new CategorieListItemDTO()
                    {
                        ID = a.ID,
                        Name = a.Name,
                        Description = a.Description,
                        Ordre = a.Ordre,
                    })
                    .ToList();

            return lst;
        }

        /// <summary>
        /// Retourne une liste des Categoies avec leurs sous categories
        /// </summary>
        public List<Categorie> GetListWithSousCategorie()
        {
            List<Categorie> lst;

            lst = FindAll()
                    .Where(a => a.Deleted == false)
                    .Include(c => c.SousCategories)
                    .OrderBy(c => c.Ordre)
                    .ThenBy(sc => sc.Ordre )
                    .ToList();

            return lst;
        }
    }
}
