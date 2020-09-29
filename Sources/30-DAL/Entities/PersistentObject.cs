using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hulkey.DAL.Entities
{
    public class PersistentObject : IPersistentObject
    {
        [Key]
        public int ID { get; set; }

        public bool Deleted { get; set; }
        public long Version { get; set; }

        public string ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public string DeletedBy { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }

        public override string ToString()
        {
            return $"{this.GetType().Name} ID={ID} Version={Version} Deleted={Deleted}";
        }
    }
}
