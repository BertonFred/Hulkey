using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hulkey.DAL.DTO
{
    /// <summary>
    /// Client pour un affichage en liste
    /// </summary>
    public class ClientListItemDTO : IListItemDTO
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Prenom{ get; set; }
        public string eMail { get; set; }
        public string TelephonneFixe { get; set; }
        public string TelephonneMobile { get; set; }
    }
}
