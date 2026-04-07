using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Services.Abstraction;

namespace Services
{
    public class CacheService(ICacheRepository _cache) : ICacheService
    {
        public async Task SetCacheValueAsync(string key, object value, TimeSpan duration)
        {
           await _cache.SetAsync(key,value,duration);
        }
        public async Task<string?> GetCacheValueAsync(string key)
        {
            
            var value =await _cache.GetAsync(key);
            return value==null?null:value;
        }

        
    }
}
