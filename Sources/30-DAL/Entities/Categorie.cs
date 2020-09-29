using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hulkey.DAL.Entities
{
    /// <summary>
    /// Categorie pour les Articles
    /// </summary>
    public class Categorie : PersistentObject
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(400)]
        public string Description { get; set; }

        /// <summary>
        /// Ordre d'affichage des categories
        /// </summary>
        public int Ordre { get; set; }

        public List<SousCategorie> SousCategories { get; set; }

        public List<Produit> Produits {get; set;}

        public override string ToString()
        {
            return $"{this.GetType().Name} ID={ID} Name={Name} Version={Version} Deleted={Deleted}";
        }
    }
}
