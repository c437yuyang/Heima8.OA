using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Heima8.OA.DALFactory;
using Heima8.OA.EFDAL;
using Heima8.OA.IBLL;
using Heima8.OA.IDAL;
using Heima8.OA.Model;
using Heima8.OA.NHDAL;

namespace Heima8.OA.BLL
{
    //模块内高内聚。模块间低耦合。

    //变化点：1、跨数据库。有mysql，slqserver2、数据库访问驱动层驱动变化。

    public partial class UserInfoService:BaseService<UserInfo>,IUserInfoService //crud
    {
        #region old2
        //让一个菜鸟到另一个一般开发人员。
        //依赖接口编程。
        //IUserInfoDal UserInfoDal = new UserInfoDal();

        //稍微高级点开发人员也不能这么写。
        //private IUserInfoDal UserInfoDal = StaticDalFactory.GetUserInfoDal();

        //DbSession dbSession =new DbSession();
        //IDbSession dbSession = new DbSession();
        //private IDbSession dbSession = DbSessionFactory.GetCurrentDbSession(); 
        #endregion

        #region old

        //更高级：IoC、DI 依赖注入的方式。  Spring.Net

        //public UserInfo Add(UserInfo userInfo)//UnitWork单元工作模式。
        //{
        //    //10000:每个用户
        //    dbSession.UserInfoDal.Add(userInfo);
        //    if (dbSession.SaveChanges() > 0)
        //    {

        //    }
        //    dbSession.UserInfoDal.Add(new UserInfo());
        //    dbSession.OrderInfoDal.Delete(new OrderInfo());
        //    dbSession.OrderInfoDal.Update(new OrderInfo());

        //    dbSession.SaveChanges();//数据提交的权利从数据库访问层提到了业务逻辑层。

        //    //return UserInfoDal.Add(userInfo);
        //} 
        #endregion


        //public UserInfoService(IDbSession dbSession)
        //    : base(dbSession)
        //{
        //    //this.DbSession = dbSession;
        //}

        //public override void SetCurrentDal()
        //{
        //    CurrentDal = this.DbSession.UserInfoDal;
        //}
    }
}
