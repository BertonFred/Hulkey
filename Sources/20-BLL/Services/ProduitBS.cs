using Hulkey.Common;
using Hulkey.DAL;
using Hulkey.DAL.DTO;
using Hulkey.DAL.Entities;
using Hulkey.DAL.Repository;
using Hulkey.SLL.ServiceCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hulkey.SLL.Services
{
    /// <summary>
    /// Services Produits
    /// </summary>
    public sealed class ProduitBS : BusinessService<Produit,ProduitListItemDTO,ProduitRepository>
    {
        public ProduitBS(IUserContext _UserContext)
            : base(_UserContext,new HulkeyUnitOfWork())
        {
        }

        public ProduitBS(IUserContext _UserContext, IUnitOfWork _uow) 
            : base(_UserContext,_uow)
        {
        }

        public List<Produit> GetListAsProduit(string SearchText)
        {
            List<Produit> lst;

            try
            {
                Log.Trace($"BusinessService GetListAsProduit SearchText={SearchText}");

                var repo = this.uow.GetRepository<ProduitRepository>();
                lst = repo.GetListProduitForComposition(SearchText);
            }
            catch (Exception e)
            {
                Log.Error($"Probléme pendant la recherche des données: {SearchText}", e);
                lst = new List<Produit>();
            }

            return lst;
        }

        #region GESTION DES PRODUITS COMPOSEE
        /// <summary>
        /// Retourne la composition du produit spécifié
        /// </summary>
        /// <param name="iProduitID">id du produit parent</param>
        /// <returns>Les enfants de la compostion</returns>
        public List<ProduitComposition> GetListProduitComposition(int iProduitID)
        {
            List<ProduitComposition> lst;

            try
            {
                Log.Trace($"ProduitBS GetProduitCompositions ProduitID={iProduitID}");

                var repo = this.uow.GetRepository<ProduitCompositionRepository>();
                lst = repo.GetListProduitComposition(iProduitID);
            }
            catch (Exception e)
            {
                Log.Error($"Probléme pendant la recherche des données GetProduitCompositions ProduitID={iProduitID}", e);
                lst = new List<ProduitComposition>();
            }

            return lst;
        }

        /// <summary>
        /// Ajout un produit a une composition d'un produit
        /// </summary>
        /// <param name="compo">La composition du produit</param>
        /// <returns>La composition ajouter</returns>
        public ProduitComposition AddProduitComposition(ProduitComposition compo)
        {
            try
            {
                Log.Trace($"ProduitBS AddProduitComposition {compo.ID} {compo.ParentID}/{compo.EnfantID}");

                var repo = this.uow.GetRepository<ProduitCompositionRepository>();
                repo.Create(compo);
                //$$$uow.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error($"ProduitBS AddProduitComposition Probléme pendant la creation de la composition = {compo.ID} {compo.ParentID}/{compo.EnfantID}", e);
                compo = null;
            }

            return compo;
        }

        /// <summary>
        /// Met a jour la composition d'un produit
        /// </summary>
        /// <param name="compo">la composition a mettre a jour</param>
        public void UpdateProduitComposition(ProduitComposition compo)
        {
            try
            {
                Log.Trace($"ProduitBS UpdateProduitComposition {compo.ID} {compo.ParentID}/{compo.EnfantID}");

                var repo = this.uow.GetRepository<ProduitCompositionRepository>();
                repo.Update(compo);
                //$$$uow.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error($"ProduitBS UpdateProduitComposition Probléme pendant la mise a jour de la composition = {compo.ID} {compo.ParentID}/{compo.EnfantID}", e);
            }
        }

        /// <summary>
        /// Suppression de la composition
        /// </summary>
        /// <param name="compo">La compostion a supprimée</param>
        public void DeleteProduitComposition(ProduitComposition compo)
        {
            try
            {
                Log.Trace($"ProduitBS DeleteProduitComposition {compo.ID} {compo.ParentID}/{compo.EnfantID}");

                var repo = this.uow.GetRepository<ProduitCompositionRepository>();
                repo.Delete(compo);
                //$$$uow.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error($"ProduitBS UpdateProduitComposition Probléme pendant la supression de la composition = {compo.ID} {compo.ParentID}/{compo.EnfantID}", e);
            }
        }
        #endregion
    }
}

