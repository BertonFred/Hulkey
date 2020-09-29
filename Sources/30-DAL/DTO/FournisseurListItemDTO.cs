using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hulkey.DAL.DTO
{
    /// <summary>
    /// Fournisseur pour un affichage en liste
    /// </summary>
    public class FournisseurListItemDTO : IListItemDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Adresse { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string eMail { get; set; }
        public string Telephonne { get; set; }
        public string Telecopie { get; set; }
    }
}
