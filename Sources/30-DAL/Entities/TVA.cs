using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hulkey.DAL.Entities
{
    /// <summary>
    /// Définition d'une Taxe Valeur Ajouter
    /// </summary>
    public class TVA : PersistentObject
    {
        [Required]
        public string Code { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public decimal Taux { get; set; }

        /// <summary>
        /// ID de la Tva par défaut
        /// </summary>
        public const int TvaParDefautID = 1;
    }
}
