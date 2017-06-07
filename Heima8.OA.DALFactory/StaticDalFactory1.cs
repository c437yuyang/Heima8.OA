 
using System.Reflection;
using Heima8.OA.IDAL;

namespace Heima8.OA.DALFactory
{
	public partial class StaticDalFactory
    {
		
	public static IActionInfoDal GetActionInfoDal()
	{
		return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".ActionInfoDal")
			as IActionInfoDal;
	}
		
	public static IMenuInfoDal GetMenuInfoDal()
	{
		return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".MenuInfoDal")
			as IMenuInfoDal;
	}
		
	public static IOrderInfoDal GetOrderInfoDal()
	{
		return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".OrderInfoDal")
			as IOrderInfoDal;
	}
		
	public static IR_UserInfo_ActionInfoDal GetR_UserInfo_ActionInfoDal()
	{
		return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".R_UserInfo_ActionInfoDal")
			as IR_UserInfo_ActionInfoDal;
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
		
	public static IUserInfoExtDal GetUserInfoExtDal()
	{
		return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserInfoExtDal")
			as IUserInfoExtDal;
	}
		}
}