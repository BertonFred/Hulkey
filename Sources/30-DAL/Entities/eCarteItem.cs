using Hulkey.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hulkey.DAL.Entities
{
    /// <summary>
    /// Indique le type de d'un CarteItem
    /// </summary>
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum eCarteItem
    {
        [Description("Carte")]
        Carte = 10,     // Carte item de type titre de carte

        [Description("Sous niveau Carte")]
        SubCarte = 15,     // Carte item de type titre de carte

        [Description("Menu")]
        Menu=20, // Carte item de type menu 

        [Description("Sous niveau Menu")]
        SubMenu = 25, // Carte item de type menu 

        [Description("Produit")]
        LienProduit=30, // Carte item de lien produit

        [Description("Texte")]
        Texte = 40,     // Carte item de type Texte meme niveau que produit
    }
}
