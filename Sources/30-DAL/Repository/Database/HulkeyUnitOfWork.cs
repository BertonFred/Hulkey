using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Hulkey.DAL.Repository
{
    public class HulkeyUnitOfWork : UnitOfWork
    {
        public HulkeyUnitOfWork()
            : this(new HulkeyDbContext())
        {
        }

        public HulkeyUnitOfWork(HulkeyDbContext context) 
            : base(context)
        {
        }

        public ProduitRepository GetRepositoryArticle()
        {
            return new ProduitRepository(this);
        }
        public CategorieRepository GetRepositoryCategorie()
        {
            return new CategorieRepository(this);
        }
        public FournisseurRepository GetRepositoryFournisseur()
        {
            return new FournisseurRepository(this);
        }
        public SousCategorieRepository GetRepositorySousCategorie()
        {
            return new SousCategorieRepository(this);
        }
        public TPFRepository GetRepositoryTPF()
        {
            return new TPFRepository(this);
        }
        public TVARepository GetRepositoryTVA()
        {
            return new TVARepository(this);
        }
        public UtilisateurRepository GetRepositoryUtilisateur()
        {
            return new UtilisateurRepository(this);
        }
    }
}
