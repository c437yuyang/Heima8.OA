using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memcached.ClientLibrary;

namespace MemCachedDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //分布Memcachedf服务IP 端口
            //string[] servers = { "192.168.135.1:11211", "192.168.135.100:11211" };
            string[] servers = { "127.0.0.1:11211", "192.168.135.100:11211" };

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
            MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.EnableCompression = false;


//            mc.Add("key1", "abc1");
//            mc.Add("key2", "abc2");
//            mc.Add("key3", "abc3");
//            mc.Add("key4", "abc4");
            mc.Add("key5", "a", DateTime.Now.AddDays(1)); //一天过期

            Console.WriteLine(mc.Get("key2"));
            Console.WriteLine(mc.Get("key3"));


        }
    }
}
