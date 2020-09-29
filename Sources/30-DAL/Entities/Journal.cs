using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hulkey.DAL.Entities
{
    /// <summary>
    /// Un item de journal
    /// permet de tracer les actions réalisées avec le logiciel
    /// </summary>
    public class Journal : PersistentObject
    {
        [Required]
        public string Action { get; set; }

        public string Parametre { get; set; }
    }
}
