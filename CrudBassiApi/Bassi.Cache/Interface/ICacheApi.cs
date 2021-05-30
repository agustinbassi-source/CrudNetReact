using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.Cache.Interface
{
    public interface ICacheApi
    {
        public T GetFromCache<T>(string key) where T : class;

        public void SetCache<T>(string key, int minutes, T entity);

        public void RemoveCache(string key);

    }
}
