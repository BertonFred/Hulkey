using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hulkey.DAL.Entities
{
    public class SousCategorie : PersistentObject
    {
        [Required]
        public int CategorieID { get; set; }
        public Categorie Categorie { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(400)]
        public string Description { get; set; }

        /// <summary>
        /// Ordre d'affichage des sous-categories
        /// </summary>
        public int Ordre { get; set; }

        List<Produit> Produits { get; set; }
        public override string ToString()
        {
            return $"{this.GetType().Name} ID={ID} Name={Name} CategorieID={CategorieID} Version={Version} Deleted={Deleted}";
        }
    }
}
