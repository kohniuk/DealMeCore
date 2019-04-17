using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DealMeCore.DataAccess.DB.EF
{
    /// <summary>
    /// Maintains a list of objects affected by a business transaction
    /// and coordinates the writing out of changes and the resolution of concurrency problems.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, IBaseRepository> repositories;

        private readonly DbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UnitOfWork(DbContext context)
        {
            repositories = new Dictionary<Type, IBaseRepository>();
            _context = context;
        }

        /// <summary>
        /// Get generic repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRepository<T> GetRepository<T>() where T : class
        {
            if (!repositories.ContainsKey(typeof(T)))
            {
                repositories[typeof(T)] = new Repository<T>(_context);
            }

            return repositories[typeof(T)] as IRepository<T>;
        }

        /// <summary>
        /// Saves all changes in db.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Saves all changes in db asynchronous.
        /// </summary>
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
