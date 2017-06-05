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
        //public UserInfo LoginUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //拿到请求的action和controller
            //filterContext.RouteData.Values;

            if (isCheck)
            {
                if (filterContext.HttpContext.Session["loginUser"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                }
            }
        }
    }
}
