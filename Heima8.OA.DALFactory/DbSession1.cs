 
using Heima8.OA.EFDAL;
using Heima8.OA.IDAL;

namespace Heima8.OA.DALFactory
{
	public partial class DbSession :IDbSession{
		
		public IActionInfoDal ActionInfoDal
		{
			get { return StaticDalFactory.GetActionInfoDal(); }
		}   
		
		public IMenuInfoDal MenuInfoDal
		{
			get { return StaticDalFactory.GetMenuInfoDal(); }
		}   
		
		public IOrderInfoDal OrderInfoDal
		{
			get { return StaticDalFactory.GetOrderInfoDal(); }
		}   
		
		public IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal
		{
			get { return StaticDalFactory.GetR_UserInfo_ActionInfoDal(); }
		}   
		
		public IRoleInfoDal RoleInfoDal
		{
			get { return StaticDalFactory.GetRoleInfoDal(); }
		}   
		
		public IUserInfoDal UserInfoDal
		{
			get { return StaticDalFactory.GetUserInfoDal(); }
		}   
		
		public IUserInfoExtDal UserInfoExtDal
		{
			get { return StaticDalFactory.GetUserInfoExtDal(); }
		}   
		}
}