﻿using Hulkey.Common;
using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Hulkey.SLL.ServiceCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Hulkey.PLL.PresentationCommon
{
    /// <summary>
    /// Fonction qui permet de convertir les valeurs d'une enum pour les affichés propremement 
    /// 
    /// explication:
    /// http://brianlagunas.com/a-better-way-to-data-bind-enums-in-wpf/
    /// sources:
    /// https://github.com/brianlagunas/BindingEnumsInWpf
    /// Usage:
    ///    <ComboBox Grid.Row="6" Grid.Column= "1"
    ///              ItemsSource= "{Binding Source={pcom:EnumBindingSource {x:Type entities:eRoleUtilisateur}}}"
    ///              SelectedItem= "{Binding Role}"
    ///              Width= "250" HorizontalAlignment= "Left" Margin= "3" />
    /// </summary>
    public class EnumBindingSourceExtension : MarkupExtension
    {
        private Type _enumType;
        public Type EnumType
        {
            get { return this._enumType; }
            set
            {
                if (value != this._enumType)
                {
                    if (null != value)
                    {
                        Type enumType = Nullable.GetUnderlyingType(value) ?? value;

                        if (!enumType.IsEnum)
                            throw new ArgumentException("Type must be for an Enum.");
                    }

                    this._enumType = value;
                }
            }
        }

        public EnumBindingSourceExtension() { }

        public EnumBindingSourceExtension(Type enumType)
        {
            this.EnumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (null == this._enumType)
                throw new InvalidOperationException("The EnumType must be specified.");

            Type actualEnumType = Nullable.GetUnderlyingType(this._enumType) ?? this._enumType;
            Array enumValues = Enum.GetValues(actualEnumType);

            if (actualEnumType == this._enumType)
                return enumValues;

            Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
            enumValues.CopyTo(tempArray, 1);
            return tempArray;
        }
    }
}
