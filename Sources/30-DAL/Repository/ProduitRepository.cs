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
    /// <summary>
    /// Repository des produits
    /// </summary>
    public class ProduitRepository : Repository<Produit,ProduitListItemDTO>
    {
        public ProduitRepository()
            : base(null)
        {
        }

        public ProduitRepository(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// Retourne une liste d'article, pour un affichage liste
        /// </summary>
        public override List<ProduitListItemDTO> GetList(string SearchText = null)
        {
            List<ProduitListItemDTO> lst;
            IQueryable<Produit> query = FindAll()
                    .Include(a => a.Categorie)
                    .Include(a => a.SousCategorie)
                    .Include(a => a.TVA)
                    ;

            query = query.Where(a => a.Deleted == false);
            if (SearchText != null)
                query = query.Where(a => a.Name.ToUpper().Contains(SearchText.ToUpper()) == true);

            lst = query.OrderBy(a => a.Categorie.Name)
                        .ThenBy(a => a.Name)
                        .Select(a => new ProduitListItemDTO()
                        {
                            ID = a.ID,
                            Name = a.Name,
                            Description = a.Description,
                            Composition = a.Composition,
                            CategorieID = a.CategorieID,
                            CategorieName = a.Categorie.Name,
                            SousCategorieID = a.SousCategorieID,
                            SousCategorieName = a.SousCategorie.Name,
                            TVAID = a.TVAID,
                            TVAName = a.TVA.Name,
                            TVATaux = a.TVA.Taux,
                            PrixVenteHT = a.PrixVenteHT,
                            PrixVenteTTC = a.PrixVenteTTC
                        })
                        .ToList();

            return lst;
        }

        /// <summary>
        /// Retourne la liste des produits utilisable pour faire une composition dans un 
        /// produit composée.
        /// C'est a dire un produit non supprimée, et non composée
        /// </summary>
        public List<Produit> GetListProduitForComposition(string SearchText = null)
        {
            List<Produit> lst;
            IQueryable<Produit> query = FindAll()
                    .Include(a => a.Categorie)
                    .Include(a => a.SousCategorie)
                    ;

            query = query.Where(a => a.Deleted == false && a.Composition == eProduitComposition.NonCompose);
            if (SearchText != null)
                query = query.Where(a => a.Name.ToUpper().Contains(SearchText.ToUpper()) == true);

            lst = query.OrderBy(a => a.Categorie.Ordre)
                        .ThenBy(a => a.SousCategorie.Ordre)
                        .ThenBy(a => a.Name)
                        .ToList();

            return lst;
        }

        public List<Produit> GetListForCategorieSousCategorie(int iCategorieID, int iSousCategorieID)
        {
            List<Produit> lst;

            lst = FindAll()
                    .Include(a => a.Categorie)
                    .Include(a => a.SousCategorie)
                    .Include(a => a.TVA)
                    .Where(a => a.Deleted == false &&
                           a.CategorieID == iCategorieID && 
                           a.SousCategorieID == iSousCategorieID)
                    .OrderBy(a => a.Name)
                    .ToList();

            return lst;
        }

    }
}
