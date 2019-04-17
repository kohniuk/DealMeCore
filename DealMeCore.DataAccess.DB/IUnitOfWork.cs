using System.Threading.Tasks;

namespace DealMeCore.DataAccess.DB
{
    /// <summary>
    /// Maintains a list of objects affected by a business transaction
    /// and coordinates the writing out of changes and the resolution of concurrency problems.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Get generic repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepository<T> GetRepository<T>() where T : class;

        /// <summary>
        /// Saves all changes in db.
        /// </summary>
        void Save();

        /// <summary>
        /// Saves all changes in db asynchronous.
        /// </summary>
        Task SaveAsync();
    }
}
