using Hulkey.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hulkey.DAL.Entities
{
    /// <summary>
    /// Indique si un produit est de type composée
    /// </summary>
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum  eProduitComposition
    {
        [Description("Non composé")]
        NonCompose,     // Le produit n'est pas composé

        [Description("Composé")]
        Compose  // Le produit est composé 
    }
}
