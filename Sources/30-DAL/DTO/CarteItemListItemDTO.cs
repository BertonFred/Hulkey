using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hulkey.DAL.Entities;

namespace Hulkey.DAL.DTO
{
    /// <summary>
    /// Carte pour un affichage en liste
    /// </summary>
    public class CarteItemListItemDTO : IListItemDTO
    {
        public int ID { get; set; }

        public int? CarteID { get; set; }

        public int? ParentID { get; set; }

        public string Texte { get; set; }

        public int? ProduitID { get; set; }

        public int Ordre { get; set; }
    }
}
