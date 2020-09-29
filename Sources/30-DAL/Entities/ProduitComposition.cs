using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hulkey.DAL.Entities
{
    /// <summary>
    /// Relation entre les produits et leurs compositions
    /// </summary>
    public class ProduitComposition : PersistentObject
    {
        public int ParentID { get; set; }
        public Produit Parent { get; set; }

        public int EnfantID { get; set; }
        public Produit Enfant { get; set; }

        public eTypeComposition TypeComposition { get; set; }

        /// <summary>
        /// Ordre d'affichage
        /// </summary>
        public int Ordre { get; set; }

        /// <summary>
        /// Quantite du produit a utilisé dans la compsoition
        /// </summary>
        public int Quantite { get; set; }
    }
}
