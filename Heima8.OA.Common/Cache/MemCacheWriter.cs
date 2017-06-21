using System;
using System.Web.Configuration;
using Memcached.ClientLibrary;

namespace Heima8.OA.Common.Cache
{
    public class MemCacheWriter:ICacheWriter
    {

        private readonly MemcachedClient _memcachedClient;

        public MemCacheWriter()
        {
            var serverList = WebConfigurationManager.AppSettings["MemCacheServerList"];

            //
//            if (string.IsNullOrEmpty(serverList))
//            {
//                throw new ConfigurationReadException();
//            }


            string[] servers = serverList.Split(',');

            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(servers);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();
            //客户端实例
            _memcachedClient = new Memcached.ClientLibrary.MemcachedClient();
            _memcachedClient.EnableCompression = false;
        }

        public void AddCache(string key, object value, DateTime expDate)
        {
            _memcachedClient.Add(key, value, expDate);
        }

        public void AddCache(string key, object value)
        {
            _memcachedClient.Add(key, value);
        }

        public object GetCache(string key)
        {
            return _memcachedClient.Get(key);
        }

        public T GetCache<T>(string key)
        {
            return (T)_memcachedClient.Get(key);
        }

        public void SetCache(string key, object value, DateTime expDate)
        {
            _memcachedClient.Set(key, value, expDate);
        }

        public void SetCache(string key, object value)
        {
            _memcachedClient.Set(key, value);
        }

        public void RemoveCache(string key)
        {
            _memcachedClient.Delete(key);
        }
    }
}