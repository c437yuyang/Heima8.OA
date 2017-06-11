using Heima8.OA.Model;
using System.Linq;

namespace Heima8.OA.IBLL
{
    public partial interface IUserInfoService:IBaseService<UserInfo>
    {
        IQueryable<UserInfo> GetEntitiesByParam(Model.Param.UserQueryParam userQueryParam);
    }
}
