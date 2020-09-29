using Hulkey.Common;
using Hulkey.Common.Cache;
using Hulkey.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulkey.SLL.ServiceCommon
{
    /// <summary>
    /// Classe de base pour les services metiers
    /// Un service métier contient les methodes qui offrent le service metiers d'un domain metiers
    /// Le nom de la classe est representatif du domaine métier
    /// Le nom des methodes est representatif de service metier offert
    /// La classe de base service metier, offre principalement le Unit Of Work qui 
    /// doit être utilisé pour la gestion des données par le service.
    /// Si un service metier A utilise un autre service métiers B, alors
    /// le service métier A doit transmettre son Unit Of Work au service B
    /// Une classe métier ne doit pas contenir de méthode static (sauf exception)   
    /// </summary>
    public abstract class BusinessService<T_DATA, T_DATALIST,T_REPOSITORY> 
        : IBusinessService<T_DATA, T_DATALIST>
        where T_DATA : class, IPersistentObject
        where T_DATALIST : IListItemDTO
        where T_REPOSITORY : Repository<T_DATA, T_DATALIST>, new()
    {
        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="_UserContext">Le context utilisateur, peut être null</param>
        /// <param name="_uow">Le unit of work a utilisé dans le service, ou celui du service métier parent</param>
        public BusinessService(IUserContext _UserContext,IUnitOfWork _uow)
        {
            this.UserContext = _UserContext;
            this.uow = _uow;
            this.uow.UserName = this.UserContext.UserName;
            this.ActivateGetListCache = false;
        }

        /// <summary>
        /// Lecture d'une entitée
        /// </summary>
        /// <param name="iID">ID de l'entitée a lire doit être > 0</param>
        /// <returns>Les données de l'entitée</returns>
        public virtual T_DATA Read(int iID)
        {
            T_DATA data = null;
            try
            {
                Log.Trace($"BusinessService {typeof(T_REPOSITORY).Name} Read ID={iID}");

                var repo = this.uow.GetRepository<T_REPOSITORY>();
                data = repo.Read(iID);
            }
            catch (Exception e)
            {
                Log.Error($"Probléme pendant la lecture de la données : {iID} ", e);
            }

            return data;
        }

        /// <summary>
        /// Creation d'une entitée
        /// </summary>
        /// <param name="Data">La donnée à creer</param>
        public virtual T_DATA Create(T_DATA Data, bool bDoSaveChange = true)
        {
            try
            {
                Log.Trace($"BusinessService {typeof(T_REPOSITORY).Name} Create Data={Data}");

                var repo = this.uow.GetRepository<T_REPOSITORY>();
                repo.Create(Data);

                if (bDoSaveChange == true)
                    uow.SaveChanges();

                ClearCache();
            }
            catch (Exception e)
            {
                Log.Error($"Probléme pendant la création de la données : {Data.ID}", e);
            }

            return Data;
        }

        /// <summary>
        /// Mise a jour d'une entitée
        /// </summary>
        /// <param name="Data">La données à mettre a jour</param>
        public virtual void Update(T_DATA Data,bool bDoSaveChange=true)
        {
            try
            {
                Log.Trace($"BusinessService {typeof(T_REPOSITORY).Name} Update Data={Data}");

                var repo = this.uow.GetRepository<T_REPOSITORY>();
                repo.Update(Data);
                
                if (bDoSaveChange == true)
                    uow.SaveChanges();

                ClearCache();
            }
            catch (Exception e)
            {
                Log.Error($"Probléme pendant la mise a jour de la données : {Data.ID}", e);
            }
        }

        /// <summary>
        /// Supprime la donnéespar son ID
        /// </summary>
        /// <param name="iID">L'ID de la données a supprimer</param>
        public virtual void DeleteById(int iID, bool bDoSaveChange = true)
        {
            try
            {
                Log.Trace($"BusinessService {typeof(T_REPOSITORY).Name} DeleteById ID={iID}");

                var repo = this.uow.GetRepository<T_REPOSITORY>();
                repo.DeleteById(iID);

                if (bDoSaveChange == true)
                    uow.SaveChanges();

                ClearCache();
            }
            catch (Exception e)
            {
                Log.Error($"Probléme pendant la suppression de la données : {iID}", e);
            }
        }

        /// <summary>
        /// Retourne la liste des données 
        /// pour le critere specifié qui est cherché par contains dans le nom
        /// Si null, toutes les données sont renvoyés
        /// Les données peuvent être recuperé dans le cache, uniquement si le critere est null
        /// L'activation du cache est fait par la property ActivateGetListCache 
        /// </summary>
        public virtual List<T_DATALIST> GetList(string SearchText = null)
        {
            Log.Trace($"BusinessService {typeof(T_REPOSITORY).Name} GetList {typeof(T_DATALIST)} SearchText={SearchText}");

            if (SearchText == null && ActivateGetListCache == true)
                return CacheLocator.Get.GetOrAddValue<List<T_DATALIST>>(typeof(T_DATALIST).Name, () => GetListInDatabase());
            else
                return GetListInDatabase(SearchText);
        }

        /// <summary>
        /// Implementation de GetList qui va vraiement reherché les données en base
        /// </summary>
        /// <param name="SearchText">Critere de recherche</param>
        /// <returns>Les données trouvées dans une liste, qui peut être vide</returns>
        protected virtual List<T_DATALIST> GetListInDatabase(string SearchText = null)
        {
            List<T_DATALIST> lst;

            try
            {
                Log.Trace($"BusinessService {typeof(T_REPOSITORY).Name} GetListInDatabase {typeof(T_DATALIST)} SearchText={SearchText}");

                var repo = this.uow.GetRepository<T_REPOSITORY>();
                lst = repo.GetList(SearchText);
            }
            catch (Exception e)
            {
                Log.Error($"Probléme pendant la recherche des données: {SearchText}", e);
                lst = new List<T_DATALIST>();
            }

            return lst;
        }

        /// <summary>
        /// Permet d'activé le cache pour les données de la methode de GetList
        /// True pour activé le cache
        /// </summary>
        protected bool ActivateGetListCache { get; set; }

        /// <summary>
        /// Demande a nettoyer le cache s'il existe, ce qui force le rechargement de celui-ci
        /// </summary>
        public virtual void ClearCache()
        {
            if (ActivateGetListCache == true)
            {
                Log.Trace($"BusinessService {typeof(T_REPOSITORY).Name} ClearCache {typeof(T_DATALIST)}");

                CacheLocator.Get.ClearValue(typeof(T_DATALIST).Name);
            }
        }

        /// <summary>
        /// Sauvegarde les changements concervés dans le unit of work
        /// </summary>
        public void SaveChanges()
        {
            uow.SaveChanges();
        }

        /// <summary>
        /// Annule les changements concervés dans le unit of work
        /// </summary>
        public void RollbackChanges()
        {
            uow.RollbackChanges();
        }

        /// <summary>
        /// Le unit of work du business service
        /// </summary>
        public IUnitOfWork uow { get; set; }

        /// <summary>
        /// Le user context du service, peut être null
        /// </summary>
        public IUserContext UserContext { get; set; }

    }
}
