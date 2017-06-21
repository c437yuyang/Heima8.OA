using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heima8.OA.Common;
using Heima8.OA.Common.Cache;
using Heima8.OA.IBLL;
using Heima8.OA.Model;
using Heima8.OA.Model.Enum;
using Spring.Context;
using Spring.Context.Support;

namespace Heima8.OA.UI.Portal.Controllers
{
    //需要进行登陆校验的类可以继承这个类，不需要的继承了后令isChcek为false即可
    public class BaseController : Controller
    {
        public BaseController()
        {
            //            this.isCheckLogin = isCheckLogin;
            this.IsCheckLogin = true;
            this.IsCheckAction = true;

        }

        public bool IsCheckLogin { get; set; }
        public bool IsCheckAction { get; set; }

        //可以有这么一个属性，用来在继承的子类里面很方便的点出已经登陆的用户的属性来，不用再去拿Session再强转
        //Session缺点:1.吃服务器资源2.性能差，所以一般的优化里面都要首先去掉Session
        public UserInfo LoginUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //拿到请求的action和controller
            //filterContext.RouteData.Values;


            //TODO:方便测试，测试完成后删除return
            //return;

            #region 登陆校验

            if (IsCheckLogin)
            {
                //先检查cache是否存在
                if (Request.Cookies["userLoginGuid"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                    return;
                }

                string userGuid = Request.Cookies["userLoginGuid"].Value;
                UserInfo userInfo = CacheHelper.GetCache(userGuid) as UserInfo;
                if (userInfo == null)
                {
                    //用户长时间不操作，超时了
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");

                    //TODO:这里在调试转向后还是会继续下面的操作，导致LoginUser那边判断为null，但是下面两句并不会终止路由，不知道怎么解决

                    filterContext.HttpContext.Response.Clear();

                    filterContext.HttpContext.Response.End();

                    return;
                }
                LoginUser = userInfo;
                //刷新session过期时间,滑动窗口机制
                CacheHelper.SetCache(userGuid, LoginUser, DateTime.Now.AddMinutes(20));
            }

            #endregion


            #region 权限过滤
            if (!IsCheckAction) return;
            //把当前的请求的对应权限拿到
            if (LoginUser.UName == "admin")
            {
                //留个后门
                return;
            }


            string url = Request.Url.AbsolutePath;
            string httpMethod = Request.HttpMethod;

            //通过容器创建对象(注入必须通过子类注入)
            IApplicationContext ctx = ContextRegistry.GetContext();

            IUserInfoService userInfoService = ctx.GetObject("UserInfoService") as IUserInfoService;
            IActionInfoService actionInfoService = ctx.GetObject("ActionInfoService") as IActionInfoService;
            IR_UserInfo_ActionInfoService rUserInfoActionInfoService = ctx.GetObject("R_UserInfo_ActionInfoService") as IR_UserInfo_ActionInfoService;
            IRoleInfoService roleInfoService = ctx.GetObject("RoleInfoService") as IRoleInfoService;

            //先拿到请求需要的那一条action
            ActionInfo requestActionInfo = actionInfoService.GetEntities(
                 a => a.Url.ToLower() == url.ToLower() && a.HttpMethd.ToLower() == httpMethod.ToLower()
                 && a.DelFlag == (short)DelFlagEnum.Normal
                 ).FirstOrDefault();

            if (requestActionInfo == null)
            {

                Response.Redirect("/Error.aspx");
                //TODO:要怎么告诉错误页面显示相应的错误?
                //Response.Write("找不到请求的页面");
                Session["ErrorInfo"] = "找不到请求的页面！！！";
                return;
            }

            //先检查用户有哪些角色权限

            //先拿到所有的角色(这里不能直接LoginUser.RoleInfo,因为存在memcache里面貌似没有存引用的属性)
            //var allRole = LoginUser.RoleInfo; 
            var allRole = userInfoService.GetEntities(u => u.ID == LoginUser.ID).FirstOrDefault().RoleInfo;
            var allRoleActionIDs = (from r in allRole
                                    from a in r.ActionInfo
                                    select a.ID).ToList(); //拿到所有角色的actionID

            var allDenyActionIDs = (from r in LoginUser.R_UserInfo_ActionInfo
                                    where r.HasPermission == false
                                    select r.ActionInfoID).ToList(); //拿到所有拒绝的actionID

            var allActionIDs = (from a in allRoleActionIDs
                                where !allDenyActionIDs.Contains(a)
                                select a).ToList(); //拿到所有没被拒绝的ID

            var allUserActionIDs = from a in LoginUser.R_UserInfo_ActionInfo //在拿到允许的特殊权限
                                   where a.HasPermission == true
                                   select a.ID;

            allActionIDs.AddRange(allUserActionIDs.ToList());//合并特殊权限和role权限
            allActionIDs = allActionIDs.Distinct().ToList();//去除重复

            var actionListIds = actionInfoService.GetEntities(u => allActionIDs.Contains(u.ID)
                                                                && u.DelFlag == (short)DelFlagEnum.Normal)
                                                                .Select(u => u.ID)
                                                                .ToList();

            if (actionListIds.Contains(requestActionInfo.ID)) //该用户有对应的权限
            {
                return;
            }

            Session["ErrorInfo"] = "没有权限请求对应的页面！！！";
            Response.Redirect("/Error.aspx");

            //var allowAction =
            //    rUserInfoActionInfoService.GetEntities(u => u.UserInfoID == LoginUser.ID
            //                                                && u.HasPermission == true
            //                                                && u.ActionInfoID == requestActionInfo.ID)
            //                                                .FirstOrDefault();



            //if (allowAction != null)
            //{
            //    return;
            //}
            //else
            //{
            //    Response.Redirect("/Error.html");
            //}


            #endregion


        }
    }
}
