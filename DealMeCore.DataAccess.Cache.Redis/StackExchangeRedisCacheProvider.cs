using DealMeCore.DataAccess.Cache.Redis.Extensions;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace DealMeCore.DataAccess.Cache.Redis
{
    /// <summary>
    /// StackExchangeRedisCacheProvider.
    /// </summary>
    public class StackExchangeRedisCacheProvider : ICacheProvider
    {
        private const string EmptyResultValue = "EmptyResult";
        private readonly IDatabase database;

        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

        /// <summary>
        /// Initializes a new instance of the <see cref="StackExchangeRedisCacheProvider" /> class.
        /// </summary>
        public StackExchangeRedisCacheProvider()
        {
            this.database = redis.GetDatabase();
        }

        #region Implementation of ICacheProvider

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        public void Add(string key, object value, TimeSpan? expiry = null)
        {
            database.Set(key, value, expiry);
        }

        /// <summary>
        /// Adds value to cache under specified key (async version).
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns>Task.</returns>
        public async Task AddAsync(string key, object value, TimeSpan? expiry = null)
        {
            await database.SetAsync(key, value, expiry);
        }

        /// <summary>
        /// Removes value from the cache using specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            database.KeyDelete(key);
        }

        /// <summary>
        /// Removes value from the cache using specified key (async version).
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Task.</returns>
        public async Task RemoveAsync(string key)
        {
            await database.KeyDeleteAsync(key);
        }

        /// <summary>
        /// Gets value from cache using specified key.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="action">The action.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns>Cached entry.</returns>
        public T Get<T>(string key, Func<T> action = null, TimeSpan? expiry = null) where T : class
        {
            T result = null;

            var preRes = database.Get<T>(key);

            if (preRes == null)
            {
                if (action != null)
                {
                    T value = action();

                    if (value == null)
                    {
                        Add(key, EmptyResultValue, expiry);
                    }
                    else
                    {
                        Add(key, value, expiry);
                    }

                    result = value;
                }
            }
            else
            {
                string stringValue = preRes as string;

                if (!(stringValue != null && stringValue == EmptyResultValue))
                {
                    result = preRes;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets value from cache using specified key.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="action">The action.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns>Cached entry.</returns>
        public async Task<T> GetAsync<T>(string key, Func<Task<T>> action = null, TimeSpan? expiry = null) where T : class
        {
            T result = null;

            var preRes = await database.GetAsync<T>(key);

            if (preRes == null)
            {
                if (action != null)
                {
                    T value = await action();

                    if (value == null)
                    {
                        await AddAsync(key, EmptyResultValue, expiry);
                    }
                    else
                    {
                        await AddAsync(key, value, expiry);
                    }

                    result = value;
                }
            }
            else
            {
                string stringValue = preRes as string;

                if (!(stringValue != null && stringValue == EmptyResultValue))
                {
                    result = preRes;
                }
            }

            return result;
        }
        #endregion
    }
}
