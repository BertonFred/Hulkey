using Hulkey.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hulkey.DAL.Entities
{
    /// <summary>
    /// Definition des roles d'utilisateurs
    /// </summary>
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum eRoleUtilisateur
    {
        [Description("Administrateur")]
        Administrateur,
        [Description("Responsable de salle")]
        ResponsableSalle,
        [Description("Serveur")]
        Serveur
    }
}
