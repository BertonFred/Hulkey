using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hulkey.DAL.Entities
{
    /// <summary>
    /// Définition d'un type de paiement TR, CB, Cash, avoir, Cheque
    /// </summary>
    public class TypePaiement : PersistentObject
    {
        [Required]
        public string Code { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Libelle { get; set; }
    }
}
