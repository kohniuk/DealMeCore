using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DealMeCore.DataAccess.DB.EF
{
    /// <summary>
    /// The generic repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<T> DbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        /// <summary>
        /// Insert new entity to DB.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        /// <summary>
        /// Insert a range entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void AddRange(ICollection<T> entities)
        {
            DbSet.AddRange(entities);
        }

        /// <summary>
        /// Delete entity from DB by entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        /// <summary>
        /// Delete entity from DB by id.
        /// </summary>
        /// <param name="id">Entity id.</param>
        public void Delete(object id)
        {
            var entity = GetById(id);

            if (entity == null)
            {
                return;
            }

            Delete(entity);
        }

        /// <summary>
        /// Update entity in DB.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Query entity from DB by its id.
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>
        /// The entity or null if not found
        /// </returns>
        public T GetById(object id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Query entity from DB by its id.(async version)
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>
        /// The entity or null if not found
        /// </returns>
        public async Task<T> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        /// <summary>
        /// Provides a point to query entities.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>The point to query entities.</returns>
        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        /// <summary>
        /// Determines whether all the entities satisfy a condition.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns></returns>
        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }
    }
}

