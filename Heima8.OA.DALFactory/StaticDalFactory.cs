using System.Reflection;
using Heima8.OA.IDAL;

namespace Heima8.OA.DALFactory
{
    /// <summary>
    /// 由简单工厂转变成了抽象工厂。
    /// 抽象工厂  VS  简单工厂
    /// 
    /// </summary>
    public partial class StaticDalFactory
    {
        public  static string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];
#region 改为由T4模板生成
        //public static IUserInfoDal GetUserInfoDal()
        //{
        //    //HttpRuntime.Cache.Get("")
        //    //return new NhUserInfoDal();
        //    //把上面的new去掉：希望改一个配置那么创建实例就发生变化，不需要改代码。
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName+".UserInfoDal") //UserInfoDal就不用读配置了，约定大于配置
        //           as IUserInfoDal;
        //}

        //public static IOrderInfoDal GetOrderInfoDal()
        //{
        //    return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".OrderInfoDal")
        //        as IOrderInfoDal;
        //}
#endregion
    }
}
