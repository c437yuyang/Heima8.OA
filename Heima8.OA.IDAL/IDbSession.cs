namespace Heima8.OA.IDAL
{
    public partial interface IDbSession
    {

        //IUserInfoDal UserInfoDal { get;}
        //IOrderInfoDal OrderInfoDal { get; }

        int SaveChanges();
    }
}