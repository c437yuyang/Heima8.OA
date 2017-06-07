 
namespace Heima8.OA.IDAL
{
    public partial interface IDbSession
    {
			
		IActionInfoDal ActionInfoDal {get;} 
	
		IMenuInfoDal MenuInfoDal {get;} 
	
		IOrderInfoDal OrderInfoDal {get;} 
	
		IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal {get;} 
	
		IRoleInfoDal RoleInfoDal {get;} 
	
		IUserInfoDal UserInfoDal {get;} 
	
		IUserInfoExtDal UserInfoExtDal {get;} 
	}
}