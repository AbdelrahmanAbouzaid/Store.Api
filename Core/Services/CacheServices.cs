using Domain.Contracts;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CacheServices(ICacheRepository cacheRepository) : ICacheServices
    {
        public async Task<string?> GetCacheValueAsynk(string key)
        {
            var result = await cacheRepository.GetAsynk(key);
            return result == null ? null : result;
        }

        public async Task SetCacheValueAsynk(string key, object value, TimeSpan duration)
        {
            await cacheRepository.SetAsynk(key, value, duration);
        }
    }
}
