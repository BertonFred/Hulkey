using Hulkey.DAL;
using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Hulkey.PLL.PresentationCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Hulkey.PLL.BackOffice
{
    /// <summary>
    /// ViewModel pour la page Produit Détaille
    /// </summary>
    public class ArticleDetailViewModel : Observable
    {
        public ArticleDetailViewModel()
        {
        }

        /// <summary>
        /// Charge l'article specifié
        /// </summary>
        public void LoadArticle(int ArticleID)
        {
            using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
            {
                IRepository<Produit> repArticle = uow.GetRepositoryArticle();
                this.Article = repArticle.Read(ArticleID);
            }
        }

        /// <summary>
        /// Charge les dependences
        /// Les listes annexe utilisées  
        /// </summary>
        public void LoadDependencies()
        {
            using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
            {
                this.Categories = uow.GetRepositoryCategorie().FindAll().ToList();
                this.SousCategories = uow.GetRepositorySousCategorie().FindAll().ToList();
                this.TVAs = uow.GetRepositoryTVA().FindAll().ToList();
            }
        }

        public string Name
        {
            get => Article.Name;
            set
            {
                if (value != Article.Name)
                {
                    Article.Name = value;
                    IsModified = true;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Description
        {
            get => Article.Description;
            set
            {
                if (value != Article.Description)
                {
                    Article.Description = value;
                    IsModified = true;
                    NotifyPropertyChanged();
                }
            }
        }
        public int? CategorieID
        {
            get => Article.CategorieID;
            set
            {
                if (value != Article.CategorieID)
                {
                    Article.CategorieID = value;
                    IsModified = true;
                    NotifyPropertyChanged();
                }
            }
        }
        public Categorie Categorie
        {
            get => Article.Categorie;
            set
            {
                if (value != Article.Categorie)
                {
                    Article.Categorie = value;
                    IsModified = true;
                    NotifyPropertyChanged();
                }
            }
        }
        public int? SousCategorieID
        {
            get => Article.SousCategorieID;
            set
            {
                if (value != Article.SousCategorieID)
                {
                    Article.SousCategorieID = value;
                    IsModified = true;
                    NotifyPropertyChanged();
                }
            }
        }
        public SousCategorie SousCategorie
        {
            get => Article.SousCategorie;
            set
            {
                if (value != Article.SousCategorie)
                {
                    Article.SousCategorie = value;
                    IsModified = true;
                    NotifyPropertyChanged();
                }
            }
        }

        public int TVAID
        {
            get => Article.TVAID;
            set
            {
                if (value != Article.TVAID)
                {
                    Article.TVAID = value;
                    IsModified = true;
                    NotifyPropertyChanged();
                }
            }
        }
        public TVA TVA
        {
            get => Article.TVA;
            set
            {
                if (value != Article.TVA)
                {
                    Article.TVA = value;
                    IsModified = true;
                    NotifyPropertyChanged();
                }
            }
        }

        public decimal PrixVenteHT
        {
            get => Article.PrixVenteHT;
            set
            {
                if (value != Article.PrixVenteHT)
                {
                    Article.PrixVenteHT = value;
                    IsModified = true;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal PrixVenteTTC
        {
            get => Article.PrixVenteTTC;
            set
            {
                if (value != Article.PrixVenteTTC)
                {
                    Article.PrixVenteTTC = value;
                    IsModified = true;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal PrixAchatHT
        {
            get => Article.PrixAchatHT;
            set
            {
                if (value != Article.PrixAchatHT)
                {
                    Article.PrixAchatHT = value;
                    IsModified = true;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the article.
        /// </summary>
        public Produit Article
        {
            get => m_Article;
            set => Set(ref m_Article, value);
        }
        private Produit m_Article;

        public List<Categorie> Categories { get; set; }
        public List<SousCategorie> SousCategories { get; set; }
        public List<TVA> TVAs { get; set; }
    }
}
