using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hulkey.DAL.Entities;

namespace Hulkey.DAL.DTO
{
    /// <summary>
    /// Affiche des infos de journalisation pour une affichage liste
    /// </summary>
    public class JournalListItemDTO : IListItemDTO
    {
        public int ID { get; set; }
        public string Action { get; set; }
        public string Parametre { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
    }
}
