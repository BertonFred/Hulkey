using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hulkey.DAL.Entities;

namespace Hulkey.DAL.DTO
{
    /// <summary>
    /// Produit Composition pour un affichage en liste
    /// </summary>
    public class ProduitCompositionListItemDTO : IListItemDTO
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public int EnfantID { get; set; }
        public int Ordre { get; set; }
        public int Quantite { get; set; }
    }
}
