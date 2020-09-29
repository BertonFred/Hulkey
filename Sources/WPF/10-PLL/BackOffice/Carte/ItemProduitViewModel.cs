using Hulkey.DAL.DTO;
using Hulkey.DAL.Entities;
using Hulkey.PLL.PresentationCommon;
using Hulkey.SLL.Services;
using Hulkey.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Hulkey.PLL.MVVM;
using MahApps.Metro.Controls.Dialogs;

namespace Hulkey.PLL.BackOffice
{
    /// <summary>
    /// Item View model 
    /// Utilser par CarteViewModel pour represente les données d'un controle Item
    /// </summary>
    public sealed class ItemProduitViewModel
        : ItemViewModelBase
    {
        public ItemProduitViewModel()
        {
            this.Service = new ProduitBS(UserContext.Instance);
        }

        public ItemProduitViewModel(int produitID)
        {
            this.Service = new ProduitBS(UserContext.Instance);
            this.ProduitID = produitID;
        }

        #region ACTIONS
        #endregion

        #region PROPERTIES
        /// <summary>
        /// ID de la données, 0 pour une creation, >0 pour un tva existant
        /// </summary>
        public int ProduitID
        {
            get => m_ProduitID;
            set
            {
                Set(ref m_ProduitID, value, bMarkAsModified: false);
                this.Produit = Service.Read(m_ProduitID);
            }
        }
        private int m_ProduitID;

        /// <summary>
        /// Information de la données courante
        /// </summary>
        public Produit Produit
        {
            get => m_Produit;
            private set => Set(ref m_Produit, value, bMarkAsModified: false);
        }
        private Produit m_Produit;

        public new string Titre
        {
            get => Produit.Name;
        }
        public string Name
        {
            get => Produit.Name;
        }
        public string Description
        {
            get => Produit.Description;
        }

        public decimal PrixVenteTTC
        {
            get => Produit.PrixVenteTTC;
        }

        /// <summary>
        /// Le service de gestion des produis
        /// </summary>
        private ProduitBS Service
        {
            get; set;
        }
        #endregion

        #region COMMAND
        #endregion
    }
}
