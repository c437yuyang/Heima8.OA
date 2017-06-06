using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heima8.OA.Model;

namespace Heima8.OA.UI.Portal.Controllers
{
    //需要进行登陆校验的类可以继承这个类，不需要的继承了后令isChcek为false即可
    public class BaseController : Controller
    {
        public BaseController()
        {
            //            this.isCheck = isCheck;
            this.isCheck = true;
        }

        public bool isCheck { get; set; }

        //可以有这么一个属性，用来在继承的子类里面很方便的点出已经登陆的用户的属性来，不用再去拿Session再强转
        //Session缺点:1.吃服务器资源2.性能差，所以一般的优化里面都要首先去掉Session
        public UserInfo LoginUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //拿到请求的action和controller
            //filterContext.RouteData.Values;


            //TODO:方便测试，测试完成后删除return
            return;

            if (isCheck)
            {
                if (Request.Cookies["userLoginGuid"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                    return;
                }

                string userGuid = Request.Cookies["userLoginGuid"].Value;
                UserInfo userInfo = Common.Cache.CacheHelper.GetCache(userGuid) as UserInfo;
                if (userInfo == null)
                {
                    //用户长时间不操作，超时了
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                }
                LoginUser = userInfo;
                //刷新session过期时间,滑动窗口机制
                Common.Cache.CacheHelper.SetCache(userGuid,LoginUser,DateTime.Now.AddMinutes(20));


            }
        }
    }
}
