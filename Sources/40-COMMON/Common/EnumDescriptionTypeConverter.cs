using System;
using System.ComponentModel;
using System.Reflection;

namespace Hulkey.Common
{
    /// <summary>
    /// Fonction qui permet de convertir les valeurs d'une enum pour les affichés propremement 
    /// 
    /// explication:
    /// http://brianlagunas.com/a-better-way-to-data-bind-enums-in-wpf/
    /// sources:
    /// https://github.com/brianlagunas/BindingEnumsInWpf
    /// Usage:
    ///     // --- ACTEURS
    ///[TypeConverter(typeof(EnumDescriptionTypeConverter))]
    ///public enum eNatureActeur
    ///{
    ///    [Description("Indéfini")]
    ///    Indefini = 0,
    ///    [Description("Individu")]
    ///    Individu,
    ///    [Description("Groupe ou Organisation")]
    ///    GroupeOuOrganisation,
    ///    [Description("Population")]
    ///    Population
    ///}
    /// </summary>
    public class EnumDescriptionTypeConverter : EnumConverter
    {
        public EnumDescriptionTypeConverter(Type type)
            : base(type)
        {
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value != null)
                    return value.DescriptionAttr();

                return string.Empty;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
