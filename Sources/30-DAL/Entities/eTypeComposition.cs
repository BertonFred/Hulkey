using Hulkey.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hulkey.DAL.Entities
{
    /// <summary>
    /// Pour un produit composé, indiqué la nature de la composition
    /// si ET alors tous les composés sont necessaire
    /// si OU alors seul un des composé est necessaire
    /// </summary>
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum eTypeComposition
    {
        [Description("Composant")]
        SousNiveau,     // La composition est un sous niveau

        [Description("Composant ET")]
        ComposantET,     // Le produit n'est pas composé

        [Description("Composant OU")]
        ComposantOU  // Le produit est composé 
    }
}
