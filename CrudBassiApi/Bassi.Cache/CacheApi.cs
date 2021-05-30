using Bassi.Cache.Interface;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.Cache
{
    public class CacheApi : ICacheApi
    {
        private IMemoryCache _cache;

        public CacheApi(IMemoryCache cache)
        {
            _cache = cache;
        }


        public T GetFromCache<T>(string key) where T : class
        {
            T response;

            bool exist = _cache.TryGetValue<T>(key, out response);

            if (exist)
                return response;
            else
                return null;
        }

        public void RemoveCache(string key)
        {
            _cache.Remove(key);
        }

        public void SetCache<T>(string key, int minutes, T entity)
        {
            _cache.Set(key, entity,
                 new MemoryCacheEntryOptions()
                 .SetAbsoluteExpiration(TimeSpan.FromMinutes(minutes)));
        }
    }

    public static class CacheKeys
    {
        public static string Alarms { get { return "Alarms"; } }

    }
}
