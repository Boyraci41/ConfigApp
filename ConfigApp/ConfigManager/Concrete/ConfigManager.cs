using ConfigManager.Abstract;
using ConfigManager.Exceptions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigManager.Concrete
{
    public class ConfigManager : IConfigManager
    {
        private readonly IDistributedCache _cache;
        private readonly string _prefix;


        public ConfigManager(IDistributedCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _prefix = configuration["ServicePrefix"];
        }

        public async Task<T> GetConfigValue<T>(string cacheKey)
        {
            try
            {
                cacheKey = String.Format("{0}_{1}", _prefix, cacheKey);
                var cacheObj = await _cache.GetAsync(cacheKey);
                return JsonSerializer.Deserialize<T>(cacheObj);
            }
            catch (Exception ex)
            {

                throw new CacheException("Could not get config data", ex);
            }
        }

        public async Task RemoveConfig(string cacheKey)
        {
            try
            {
                cacheKey = String.Format("{0}_{1}", _prefix, cacheKey);
                var value = await _cache.GetAsync(cacheKey);

                if (value != null)
                {
                    await _cache.RemoveAsync(cacheKey);
                }

            }
            catch (Exception ex)
            {

                throw new CacheException("Could not remove config data", ex);
            }
        }

        public async Task SetConfig(string key, object value)
        {
            try
            {
                SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
                var strValue = JsonSerializer.Serialize(value);

                _semaphore.WaitAsync();

                try
                {
                    key = String.Format("{0}_{1}", _prefix, key);
                    await _cache.SetStringAsync(key, strValue);
                }
                finally { _semaphore.Release(); }


            }
            catch (Exception ex)
            {
                throw new CacheException("Could not remove config data", ex);
            }
        }
    }
}
