 
using Heima8.OA.EFDAL;
using Heima8.OA.IDAL;

namespace Heima8.OA.DALFactory
{
	public partial class DbSession :IDbSession{
		
		public IOrderInfoDal OrderInfoDal
		{
			get { return StaticDalFactory.GetOrderInfoDal(); }
		}   
		
		public IRoleInfoDal RoleInfoDal
		{
			get { return StaticDalFactory.GetRoleInfoDal(); }
		}   
		
		public IUserInfoDal UserInfoDal
		{
			get { return StaticDalFactory.GetUserInfoDal(); }
		}   
		}
}