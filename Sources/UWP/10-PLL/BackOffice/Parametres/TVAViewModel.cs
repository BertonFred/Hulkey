using Hulkey.DAL;
using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Hulkey.PLL.PresentationCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Hulkey.DAL.DTO;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Hulkey.PLL.BackOffice
{
    /// <summary>
    /// ViewModel pour la TVA 
    /// </summary>
    public class TVAViewModel : Observable
    {
        public TVAViewModel(TVA tva)
        {
            this.Value = tva;
        }

        /// <summary>
        /// Gets or sets the current 
        /// </summary>
        public TVA Value
        {
            get => m_Value;
            set
            {
                Set(ref m_Value, value);
                NotifyPropertyChanged(nameof(ID));
                NotifyPropertyChanged(nameof(Description));
                NotifyPropertyChanged(nameof(Taux));
            }
        }
        private TVA m_Value;

        public int ID
        {
            get
            {
                return Value.ID;
            }
        }

        public string Description
        {
            get
            {
                if (Value == null) return string.Empty;
                return Value.Name;
            }
            set
            {
                if (value != Value.Name)
                {
                    Value.Name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public decimal Taux
        {
            get
            {
                return Value.Taux;
            }
            set
            {
                if (value != Value.Taux)
                {
                    Value.Taux = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
