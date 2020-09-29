using Hulkey.DAL;
using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Hulkey.PLL.PresentationCommon;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Hulkey.DAL.DTO;

namespace Hulkey.PLL.BackOffice
{
    /// <summary>
    /// ViewModel pour la page liste des Articles
    /// </summary>
    public class ArticleListViewModel : Observable
    {
        public ArticleListViewModel()
        {
        }

        /// <summary>
        /// Charger la liste des articles
        /// </summary>
        public void LoadArticles()
        {
            List<ProduitListItemDTO> lst;
            this.Articles.Clear();

            using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
            {
                ProduitRepository repArticle = uow.GetRepositoryArticle();
                lst = repArticle.GetList();
            }

            lst.ForEach(a => this.Articles.Add(a));
        }

        /// <summary>
        /// Gets the articles to display.
        /// </summary>
        public ObservableCollection<ProduitListItemDTO> Articles { get; private set; } = new ObservableCollection<ProduitListItemDTO>();

        /// <summary>
        /// Gets or sets the selected article.
        /// </summary>
        public ProduitListItemDTO SelectedArticle
        {
            get => m_SelectedArticle;
            set => Set(ref m_SelectedArticle, value);
        }
        private ProduitListItemDTO m_SelectedArticle;
    }
}
