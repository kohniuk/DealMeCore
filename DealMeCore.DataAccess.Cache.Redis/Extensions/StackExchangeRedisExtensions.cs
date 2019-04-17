using Jil;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DealMeCore.DataAccess.Cache.Redis.Extensions
{
    /// <summary>
    /// StackExchangeRedisExtensions.
    /// </summary>
    public static class StackExchangeRedisExtensions
    {
        public static T Get<T>(this IDatabase cache, string key)
        {
            RedisValue cachedValue = cache.StringGet(key);

            if (cachedValue.IsNull)
            {
                return default(T);
            }

            return JSON.Deserialize<T>(cachedValue.ToString());
        }

        /// <summary>
        /// Gets the specified key (async version).
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="cache">The cache.</param>
        /// <param name="key">The key.</param>
        /// <returns>Cached entry.</returns>
        public static async Task<T> GetAsync<T>(this IDatabase cache, string key)
        {
            RedisValue cachedValue = await cache.StringGetAsync(key);

            if (cachedValue.IsNull)
            {
                return default(T);
            }

            return JSON.Deserialize<T>(cachedValue.ToString());
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <param name="key">The key.</param>
        /// <returns>Cached entry.</returns>
        public static object Get(this IDatabase cache, string key)
        {
            RedisValue cachedValue = cache.StringGet(key);

            if (cachedValue.IsNull)
            {
                return default(object);
            }

            return JSON.Deserialize<object>(cachedValue);
        }

        /// <summary>
        /// Gets the specified key (async version).
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <param name="key">The key.</param>
        /// <returns>Cached entry.</returns>
        public static async Task<object> GetAsync(this IDatabase cache, string key)
        {
            RedisValue cachedValue = await cache.StringGetAsync(key);

            if (cachedValue.IsNull)
            {
                return default(object);
            }

            return JSON.Deserialize<object>(cachedValue);
        }

        /// <summary>
        /// Gets all keys.
        /// </summary>
        /// <param name="connectionMultiplexer">The connection multiplexer.</param>
        /// <returns>Keys.</returns>
        public static IEnumerable<RedisKey> GetAllKeys(this ConnectionMultiplexer connectionMultiplexer)
        {
            var keys = new HashSet<RedisKey>();

            // Could have more than one instance https://github.com/StackExchange/StackExchange.Redis/blob/master/Docs/KeysScan.md
            var endPoints = connectionMultiplexer.GetEndPoints();

            foreach (EndPoint endpoint in endPoints)
            {
                var redisKeys = connectionMultiplexer.GetServer(endpoint).Keys();

                foreach (var key in redisKeys)
                {
                    if (!keys.Contains(key))
                    {
                        keys.Add(key);
                    }
                }
            }

            return keys;
        }

        /// <summary>
        /// Searches the keys.
        /// </summary>
        /// <param name="connectionMultiplexer">The connection multiplexer.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns>Keys.</returns>
        public static IEnumerable<RedisKey> SearchKeys(this ConnectionMultiplexer connectionMultiplexer, string searchPattern)
        {
            var keys = new HashSet<RedisKey>();

            // Could have more than one instance https://github.com/StackExchange/StackExchange.Redis/blob/master/Docs/KeysScan.md
            var endPoints = connectionMultiplexer.GetEndPoints();

            foreach (EndPoint endpoint in endPoints)
            {
                var redisKeys = connectionMultiplexer.GetServer(endpoint).Keys(pattern: searchPattern);

                foreach (var key in redisKeys)
                {
                    if (!keys.Contains(key))
                    {
                        keys.Add(key);
                    }
                }
            }

            return keys;
        }

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns>Set result.</returns>
        public static bool Set(this IDatabase cache, string key, object value, TimeSpan? expiry = null)
        {
            if (value != null)
            {
                string serializedString = JSON.Serialize(value);

                return cache.StringSet(key, serializedString, expiry);
            }

            return cache.StringSet(key, RedisValue.Null, expiry);
        }

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns>Set result.</returns>
        public static async Task<bool> SetAsync(this IDatabase cache, string key, object value, TimeSpan? expiry = null)
        {
            if (value != null)
            {
                string serializedString = JSON.Serialize(value);

                return await cache.StringSetAsync(key, serializedString, expiry);
            }

            return await cache.StringSetAsync(key, RedisValue.Null, expiry);
        }
    }
}
