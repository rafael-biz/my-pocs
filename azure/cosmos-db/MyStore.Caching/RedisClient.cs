using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Caching
{
    public sealed class RedisClient
    {
        private readonly ILogger<RedisClient> logger;

        private readonly IConnectionMultiplexer redis;

        public RedisClient(ILogger<RedisClient> logger, IConnectionMultiplexer redis)
        {
            this.logger = logger;
            this.redis = redis;
        }

        /// <summary>
        /// Try to read from cache, if not found, set it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fetch"></param>
        /// <returns></returns>
        public async Task<T> GetJsonAsync<T>(string key, Func<Task<T>> fetch) where T : class
        {
            T value = await GetFromCacheAsync<T>(key);

            logger.LogInformation("Fetch key '{key}' class '{class}' from Redis: {success}", key, typeof(T).Name, (value != null));

            if (value != null)
            {
                return value;
            }

            value = await fetch();

            await SetCacheAsync<T>(key, value);

            return value;
        }

        private async Task<T> GetFromCacheAsync<T>(string key) where T : class
        {
            try
            {
                IDatabase db = redis.GetDatabase();

                RedisValue json = await db.StringGetAsync(key);

                if (json.IsNullOrEmpty)
                    return null;

                T dto = JsonConvert.DeserializeObject<T>(json);

                if (dto == null)
                {
                    logger.LogWarning("Unable to deserialize object: {json}", json);
                    return null;
                }

                return dto;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Failed to fetch key '{key}' class '{class}' from Redis: {message}", key, typeof(T).FullName, e.Message);
                return null;
            }
        }

        private async Task SetCacheAsync<T>(string key, T value) where T : class
        {
            try
            {
                IDatabase db = redis.GetDatabase();

                string json = JsonConvert.SerializeObject(value);

                bool success = await db.StringSetAsync(key, json);

                logger.LogInformation("Set key '{key}' class '{class}' from Redis: {message}", key, typeof(T).FullName, success);
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "StringSetAsync failed: {message}", e.Message);
            }
        }

        public async Task Invalidade(string key)
        {
            try
            {
                IDatabase db = redis.GetDatabase();

                bool success = await db.KeyDeleteAsync(key);

                logger.LogInformation("Delete key '{key}' from Redis: {message}", key, success);
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "KeyDeleteAsync failed: {message}", e.Message);
            }
        }
    }
}
