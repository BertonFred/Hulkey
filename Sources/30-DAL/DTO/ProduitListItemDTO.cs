using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hulkey.DAL.Entities;

namespace Hulkey.DAL.DTO
{
    /// <summary>
    /// Un Produit pour un affichage dans une liste
    /// </summary>
    public class ProduitListItemDTO : IListItemDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public eProduitComposition Composition { get; set; }

        public int? CategorieID { get; set; }
        public string CategorieName { get; set; }

        public int? SousCategorieID { get; set; }
        public string SousCategorieName { get; set; }

        public decimal PrixVenteHT { get; set; }
        public decimal PrixVenteTTC { get; set; }

        public int TVAID { get; set; }
        public string TVAName { get; set; }
        public decimal TVATaux { get; set; }
    }
}
