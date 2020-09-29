using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hulkey.DAL.Entities
{
    /// <summary>
    /// Un article, produit a vendre
    /// un produit a :
    /// * Peut avoir une categorie et une sous categorie
    /// * Peut avoir un fournisseur
    /// * Peut être composée avec d'autre produit
    /// </summary>
    public class Produit : PersistentObject
    {
        public Produit()
        {
            Composition = eProduitComposition.NonCompose;
            TVAID = TVA.TvaParDefautID;
        }
        public eProduitComposition Composition { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(400)]
        public string Description { get; set; }

        public int? CategorieID { get; set; }
        public Categorie Categorie { get; set; }

        public int? SousCategorieID { get; set; }
        public SousCategorie SousCategorie { get; set; }

        [Required]
        public decimal PrixVenteHT { get; set; }
        [Required]
        public decimal PrixVenteTTC { get; set; }
        [Required]
        public int TVAID { get; set; }
        public TVA TVA { get; set; }
        [Required]
        public decimal PrixAchatHT { get; set; }
        [Required]
        public decimal PrixAchatTTC { get; set; }

        public override string ToString()
        {
            return $"{this.GetType().Name} ID={ID} Name={Name} Version={Version} Deleted={Deleted}";
        }
    }
}
