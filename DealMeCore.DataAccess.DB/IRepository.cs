using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DealMeCore.DataAccess.DB
{
    /// <summary>
    /// Common generic interface for all DB repositories.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> : IBaseRepository where T : class
    {
        /// <summary>
        /// Insert new entity to DB.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Insert(T entity);

        /// <summary>
        /// Insert a range entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void AddRange(ICollection<T> entities);

        /// <summary>
        /// Delete entity from DB by entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Delete entity from DB by id.
        /// </summary>
        /// <param name="id">Entity id.</param>
        void Delete(object id);

        /// <summary>
        /// Update entity in DB.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);

        /// <summary>
        /// Query entity from DB by its id.
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>
        /// The entity or null if not found
        /// </returns>
        T GetById(object id);

        /// <summary>
        /// Query entity from DB by its id.(async version)
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>
        /// The entity or null if not found
        /// </returns>
        Task<T> GetByIdAsync(object id);

        /// <summary>
        /// Provides a point to query entities.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>The point to query entities.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Determines whether all the entities satisfy a condition.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns></returns>
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
    }
}
