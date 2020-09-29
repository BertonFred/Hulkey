using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hulkey.DAL.DTO
{
    /// <summary>
    /// Sous Categorie pour un affichage de liste
    /// </summary>
    public class CategorieListItemDTO : IListItemDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Ordre { get; set; }
    }
}
