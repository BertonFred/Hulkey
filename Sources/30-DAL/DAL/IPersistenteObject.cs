using System;
using System.Collections.Generic;
using System.Text;

namespace Hulkey.DAL
{
    /// <summary>
    /// Interface de base des object persistant
    /// </summary>
    public interface IPersistentObject
    {
        int ID { get; set; }
        bool Deleted { get; set; }
        long Version { get; set; }
        string ModifiedBy { get; set; }
        DateTimeOffset? ModifiedOn { get; set; }
        string CreatedBy { get; set; }
        DateTimeOffset? CreatedOn { get; set; }
        string DeletedBy { get; set; }
        DateTimeOffset? DeletedOn { get; set; }
    }
}
