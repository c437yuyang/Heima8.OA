using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Context;
using Spring.Context.Support;

namespace Heima8.OA.Common.Cache
{
    public class CacheHelper
    {

        //Sprint.Net注入一个CacheWriter过来
        private static ICacheWriter _CacheWriter { get; set; }


        static CacheHelper()
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            ctx.GetObject("CacheHelper");
            CacheHelper._CacheWriter = ctx.GetObject("CacheWriter") as ICacheWriter;
        }

        public static void AddCache(string key, object value, DateTime expDate)
        {
            _CacheWriter.AddCache(key, value, expDate);
        }


        public static object GetCache(string key)
        {
            return _CacheWriter.GetCache(key);
        }

        public static void SetCache(string key, object value, DateTime expDate)
        {
            _CacheWriter.SetCache(key,value,expDate);
        }


    }
}
