using System;
using System.Web;

namespace Heima8.OA.Common.Cache
{
    public class HttpRuntimeCacheWriter:ICacheWriter
    {
        public void AddCache(string key, object value, DateTime expDate)
        {
            HttpRuntime.Cache.Insert(key, value, null, expDate, TimeSpan.Zero);
        }

        public void AddCache(string key, object value)
        {
            HttpRuntime.Cache.Insert(key, value);
        }

        public object GetCache(string key)
        {
            return HttpRuntime.Cache[key];
        }

        public T GetCache<T>(string key)
        {
            return (T)HttpRuntime.Cache[key];
        }

        public void SetCache(string key, object value, DateTime expDate)
        {
            HttpRuntime.Cache.Remove(key);
            AddCache(key,value,expDate);
        }

        public void SetCache(string key, object value)
        {
            HttpRuntime.Cache.Remove(key);
            AddCache(key, value);
        }
    }
}