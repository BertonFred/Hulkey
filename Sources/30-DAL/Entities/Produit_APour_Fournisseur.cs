using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hulkey.DAL.Entities
{
    /// <summary>
    /// relation entre les produits et leurs fournisseurs
    /// </summary>
    public class ProduitFournisseur : PersistentObject
    {
        public int ProduitID { get; set; }
        public Produit Produit { get; set; }

        public int FournisseurID { get; set; }
        public Fournisseur Fournisseur { get; set; }
    }
}
