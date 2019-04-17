using System;
using System.Threading.Tasks;

namespace DealMeCore.DataAccess.Cache
{
    /// <summary>
    /// ICacheProvider interface.
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// Adds value to cache under specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        void Add(string key, object value, TimeSpan? expiry = null);

        /// <summary>
        /// Adds value to cache under specified key (async version).
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns>Task.</returns>
        Task AddAsync(string key, object value, TimeSpan? expiry = null);

        /// <summary>
        /// Removes value from the cache using specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        void Remove(string key);

        /// <summary>
        /// Removes value from the cache using specified key (async version).
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Task.</returns>
        Task RemoveAsync(string key);

        /// <summary>
        /// Gets value from cache using specified key.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="action">The action.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns>Cached entry.</returns>
        T Get<T>(string key, Func<T> action = null, TimeSpan? expiry = null) where T : class;

        /// <summary>
        /// Gets value from cache using specified key.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="action">The action.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns>Cached entry.</returns>
        Task<T> GetAsync<T>(string key, Func<Task<T>> action = null, TimeSpan? expiry = null) where T : class;
    }
}
