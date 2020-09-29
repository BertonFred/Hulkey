using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hulkey.DAL.Entities;

namespace Hulkey.DAL.DTO
{
    /// <summary>
    /// TPF pour un affichage en liste
    /// </summary>
    public class TPFListItemDTO : IListItemDTO
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Taux { get; set; }
    }
}
