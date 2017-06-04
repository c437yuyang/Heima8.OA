 
namespace Heima8.OA.IDAL
{
    public partial interface IDbSession
    {
			
		IActionInfoDal ActionInfoDal {get;} 
	
		IOrderInfoDal OrderInfoDal {get;} 
	
		IRoleInfoDal RoleInfoDal {get;} 
	
		IUserInfoDal UserInfoDal {get;} 
	}
}