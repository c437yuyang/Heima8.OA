 
using System.Reflection;
using Heima8.OA.IDAL;

namespace Heima8.OA.DALFactory
{
	public partial class StaticDalFactory
    {
		
	public static IOrderInfoDal GetOrderInfoDal()
	{
		return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".OrderInfoDal")
			as IOrderInfoDal;
	}
		
	public static IRoleInfoDal GetRoleInfoDal()
	{
		return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".RoleInfoDal")
			as IRoleInfoDal;
	}
		
	public static IUserInfoDal GetUserInfoDal()
	{
		return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserInfoDal")
			as IUserInfoDal;
	}
		}
}