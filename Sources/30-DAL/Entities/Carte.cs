using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hulkey.DAL.Entities
{
    /// <summary>
    /// Represente une carte / un menu
    /// La carte est composé de CarteItem
    /// </summary>
    public class Carte : PersistentObject
    {
        /// <summary>
        ///  Nom de la carte
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Indique si la carte est active pour la caisse
        /// </summary>
        public bool ActiveCaisse { get; set; }

        /// <summary>
        /// Ordre d'affichage
        /// </summary>
        public int Ordre { get; set; }

        public virtual ICollection<CarteItem> CarteItems { get; set; }
    }
}
