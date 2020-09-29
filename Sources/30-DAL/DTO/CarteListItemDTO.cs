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
    public class CarteListItemDTO : IListItemDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool ActiveCaisse { get; set; }
        public int Ordre { get; set; }
    }
}
