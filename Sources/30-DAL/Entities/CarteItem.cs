using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hulkey.DAL.Entities
{
    /// <summary>
    /// Represente une carte / un menu
    /// La carte est composé de CarteItem qui sont structurées hiearchiquement pour construire la carte.
    /// 
    /// Il existe 2 types d'entrées de niveau racine (attaché directement a une carte) : ItemCarte ou ItemMenu
    /// Dans ce cas le CarteID est l'ID de la carte parent de l'item, et le ParentID = null
    /// 
    /// Un ItemCarte ou un ItemMenu peut egalement avoir comme parent un objet du même type que lui meme
    /// pour constitué un sous niveau
    /// 
    /// Un ItemTexte ou un ItemParent n'a pas d'enfants, et doit avoir comme parent un ItemCarte ou un ItemMenu
    /// 
    /// Exemple:
    /// Un ItemCarte ou un ItemMenu peut avoir comme enfant un ou plusieurs ItemTexte, ItemProduit, ItemCarte, ItemMenu
    /// Un Item Carte
    ///     Un Item Carte
    ///         Un Texte
    ///         Un Produit
    ///     Un Item Carte
    ///         Un Texte
    ///         Un Produit
    ///     Un Texte
    ///     Un Produit
    ///     
    /// Un Item Menu
    ///     Un Texte
    ///     Un Produit
    /// Un Item Menu
    ///         Un Texte
    ///         Un Produit
    /// Un Item Menu
    ///     Un Item Menu
    ///         Un Texte
    ///         Un Produit
    /// </summary>
    public class CarteItem : PersistentObject
    {
        /// <summary>
        /// Type de carte element
        /// Le type element permet de savoir quelle attribut utilisé dans cette classe
        /// </summary>
        eCarteItem TypeElement { get; set; }

        /// <summary>
        /// Ordre d'affichage
        /// </summary>
        public int Ordre { get; set; }

        /// <summary>
        /// Le carte element est rattaché à la Carte.
        /// </summary>
        public int? CarteID { get; set; }
        public Carte Carte { get; set; }

        /// <summary>
        /// ID d'un carte elements parent du carte elements courant
        /// null s'il n'y a pas de parent
        /// </summary>
        public int? ParentID { get; set; }
        public CarteItem Parent { get; set; }
        public virtual ICollection<CarteItem> Enfants { get; set; }

        #region ELEMENTS DE CONTENU D'UN CARTE ELEMENT au choix Texte ou Produit en fonction de Type Element
        /// <summary>
        ///  Le texte de l'element de carte, ou null si c'est un pointeur vers un produit
        /// </summary>
        public string Texte { get; set; }

        /// <summary>
        ///  L'id du produit , ou null si c'est un texte
        /// </summary>
        public int? ProduitID { get; set; }
        public Produit Produit { get; set; }
        #endregion ELEMENTS DE CONTENU D'UN CARTE ELEMENT
    }
}
