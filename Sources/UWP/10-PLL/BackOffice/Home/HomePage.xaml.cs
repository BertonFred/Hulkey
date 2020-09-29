using Hulkey.DAL;
using Hulkey.DAL.DTO;
using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace Hulkey.PLL.BackOffice
{
    public sealed partial class HomePage : Page
    {
        public HomeViewModel ViewModel { get; } = new HomeViewModel();

        public HomePage()
        {
            InitializeComponent();
        }

        private void BtnCreateRecord_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
            {
                Produit article = new Produit();
                article.Name = "article 1 name";
                article.Description = "article 1 description";
                article.PrixVenteHT = 100;
                article.TauxTVA = 20.6M;
                article.PrixVenteTTC = article.PrixVenteHT * (article.TauxTVA / 100.0M);
                article.CreatedBy = "fred";
                article.CreatedOn = DateTime.Now;
                article.Version = 1;

                ProduitRepository repArticle = uow.GetRepositoryArticle();
                Produit articleFromDb = repArticle.Create(article);

                uow.SaveChanges();
            }
        }

        private void BtnReadRecords_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            using (HulkeyUnitOfWork uow = new HulkeyUnitOfWork())
            {
                ProduitRepository repArticle = uow.GetRepositoryArticle();
                List<ProduitListItemDTO> lst = repArticle.GetList();
            }
        }
    }
}
