using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hulkey.DAL.Entities
{
    /// <summary>
    /// Un Client
    /// </summary>
    public class Client : PersistentObject
    {
        [Required]
        [MaxLength(50)]
        public string Nom { get; set; }
        [MaxLength(50)]
        public string Prenom { get; set; }

        public string Adresse { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string eMail { get; set; }
        public string TelephonneFixe { get; set; }
        public string TelephonneMobile { get; set; }
    }
}
