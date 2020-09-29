using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulkey.DAL
{
    /// <summary>
    /// Implemebtation du design patterns REPOSITORY
    /// </summary>
    /// <typeparam name="T_ENTITY">Le type de l'entite gerer par le repository</typeparam>
    /// <typeparam name="T_DATALIST">Le type de données pour la liste de resultat simple</typeparam>
    public abstract class Repository<T_ENTITY, T_DATALIST> : IRepository<T_ENTITY, T_DATALIST>
        where T_ENTITY : class, IPersistentObject
        where T_DATALIST : IListItemDTO
    {
        /// <summary>
        /// Un repository a besoin d'un unitofwork
        /// </summary>
        /// <param name="uow">Le unit on work</param>
        public Repository(IUnitOfWork uow)
        {
            this.UnitOfWork = uow;
        }

        /// <summary>
        /// Creation de l'entité dans le unit of work
        /// </summary>
        /// <param name="entity">l'entite a creer</param>
        /// <returns>l'entity apres creation</returns>
        public T_ENTITY Create(T_ENTITY entity)
        {
            entity.Version = 1;
            entity.CreatedBy = this.UnitOfWork.UserName;
            entity.CreatedOn = DateTime.Now;

            return Set.Add(entity).Entity;
        }

        /// <summary>
        /// Creation asynchrone
        /// </summary>
        public async Task<T_ENTITY> CreateAsync(T_ENTITY entity)
        {
            entity.Version = 1;
            entity.CreatedBy = this.UnitOfWork.UserName;
            entity.CreatedOn = DateTime.Now;

            var TaskAdd =  await Set.AddAsync(entity);
            return TaskAdd.Entity;
        }

        /// <summary>
        /// Lecture d'une entité depuis son id. 
        /// L'entite est d'abord cherche dans le unit of work, si elle n'est pas présente
        /// alors elle est chargé dans le uow
        /// </summary>
        /// <param name="Id">L'id de l'entite a lire</param>
        /// <returns>l'entite lu, ou null si elle n'existe pas</returns>
        public T_ENTITY Read(int Id)
        {
            return Set.Find(Id);
        }

        /// <summary>
        /// Lecture asynchrone
        /// </summary>
        public async Task<T_ENTITY> ReadAsync(int Id)
        {
            var TaskRead = await Set.FindAsync(Id);
            return TaskRead;
        }

        /// <summary>
        /// Mise a jour de l'entite dans le unit of work
        /// </summary>
        /// <param name="entity">L'entite a mettre a jour. L'entité doit venir du unit of work</param>
        public void Update(T_ENTITY entity)
        {
            EntityEntry<T_ENTITY> entry = Context.Entry(entity);

            // Si l'entité est marquée unchangé, on la met modifiée
            // elle peut egalement etre marquée Added ou Deleted dans ce cas
            // on fait rien
            if (entry.State == EntityState.Unchanged)
            {
                entity.Version++;
                entity.ModifiedBy = this.UnitOfWork.UserName;
                entity.ModifiedOn = DateTime.Now;

                entry.State = EntityState.Modified;
            }
        }

        /// <summary>
        /// Suppression de l'entité. 
        /// Si elle n'est pas dans le unit of work elle est placé dedans
        /// </summary>
        /// <param name="entity">L'entite a supprimée</param>
        public void Delete(T_ENTITY entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Set.Attach(entity);
            }

            Set.Remove(entity);
        }

        /// <summary>
        /// Suppression d'une entité a partir de son id
        /// </summary>
        /// <param name="Id">l'ID de l'entité a supprimée</param>
        public void DeleteById(int Id)
        {
            T_ENTITY entityToDelete = Set.Find(Id);
            Set.Remove(entityToDelete);
        }

        /// <summary>
        /// Suppression asycnhrone par id
        /// </summary>
        public async Task DeleteByIdAsync(int Id)
        {
            T_ENTITY entityToDelete = await Set.FindAsync(Id);
            Set.Remove(entityToDelete);
        }

        public IQueryable<T_ENTITY> FindAll()
        {
            IQueryable<T_ENTITY> query = Set.AsQueryable();
            return query;
        }

        public IQueryable<T_ENTITY> FindBy(System.Linq.Expressions.Expression<Func<T_ENTITY, bool>> predicate)
        {
            IQueryable<T_ENTITY> query = Set.Where(predicate);
            return query;
        }

        public abstract List<T_DATALIST> GetList(string SearchText = null);

        public IUnitOfWork UnitOfWork { get; set; }
        protected DbContext Context
        {
            get
            {
                return UnitOfWork.Context;
            }
        }
        protected DbSet<T_ENTITY> Set 
        {
            get
            {
                return UnitOfWork.Context.Set<T_ENTITY>();
            }
        }
    }
}
