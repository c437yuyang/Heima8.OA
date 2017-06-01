using Heima8.OA.DALFactory;
using Heima8.OA.EFDAL;
using Heima8.OA.IBLL;
using Heima8.OA.IDAL;
using Heima8.OA.Model;
using Heima8.OA.NHDAL;

namespace Heima8.OA.BLL
{
    public class OrderInfoService : BaseService<OrderInfo>, IOrderInfoService
    {
        //public OrderInfo Add(OrderInfo orderInfo)
        //{
        //    IUserInfoDal UserInfoDal = StaticDalFactory.GetUserInfoDal();
        //    return orderInfo;
        //}

        //public OrderInfoService(IDbSession dbSession):base(dbSession)
        //{

        //    //this.DbSession = dbSession;

        //}


        //public override void SetCurrentDal()
        //{
        //    CurrentDal = DbSession.OrderInfoDal;
        //}
    }
}