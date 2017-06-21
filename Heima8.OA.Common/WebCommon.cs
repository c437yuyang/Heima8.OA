using System.Linq;
using Heima8.OA.Common.Cache;
using Heima8.OA.IBLL;
using Heima8.OA.Model;
using Heima8.OA.Model.Enum;
using Spring.Context;
using Spring.Context.Support;

namespace Heima8.OA.Common
{
    public class WebCommon
    {

        private static IUserInfoService UserInfoService { get; set; }

        static WebCommon()
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            ctx.GetObject("WebCommon");
            WebCommon.UserInfoService = ctx.GetObject("UserInfoService") as IUserInfoService;
        }
        public static UserInfo CheckUser(string userName, string userPwd)
        {
            UserInfo user = UserInfoService.GetEntities(u => u.UName == userName && u.Pwd == userPwd && u.DelFlag==(short)DelFlagEnum.Normal).FirstOrDefault();
            return user;
        }
    }
}