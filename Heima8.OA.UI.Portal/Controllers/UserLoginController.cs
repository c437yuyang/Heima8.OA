using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Heima8.OA.Common;
using Heima8.OA.IBLL;
using Heima8.OA.Model;
using Heima8.OA.Model.Enum;
using Heima8.OA.UI.Portal.Models;

namespace Heima8.OA.UI.Portal.Controllers
{
    [LoginCheckFilter(IsCheck = false)]
    public class UserLoginController : BaseController
    {

        //
        // GET: /UserControl/
        public UserLoginController()
        {
            IsCheckLogin = false;
            IsCheckAction = false;
        }

        public IUserInfoService UserInfoService { get; set; }

        public ActionResult Index()
        {

            //cache不存在检查cookie是否存在(是否选择了自动登录)
            if (Request.Cookies["UserLoginName"] != null && Request.Cookies["UserLoginPwd"] != null)
            {
                UserInfo user = WebCommon.CheckUser(Request.Cookies["UserLoginName"].Value.ToString(),
                    Request.Cookies["UserLoginPwd"].Value.ToString());
                if (user != null)
                {
                    LoginUser = user;
                    //RedirectToAction("Index", "Home");

                    string userLoginGuid = Guid.NewGuid().ToString();

                    Common.Cache.CacheHelper.AddCache(userLoginGuid, user, DateTime.Now.AddMinutes(20));
                    Response.Cookies["userLoginGuid"].Value = userLoginGuid;
                    Response.Redirect("/Home/Index");
                    Response.End();
                    //return null;
                    //ViewData["hasLogin"] = true;
                }
            }

            return View();
        }


        public ActionResult ShowValidCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(5);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }


        public ActionResult ProcessLogin()
        {
            string sessionCode = (string)Session["ValidateCode"];
            string inputCode = Request["txtValidateCode"];

            //校验验证码
            Session["ValidateCode"] = null;
            if (string.IsNullOrEmpty(sessionCode) || inputCode != sessionCode)
            {
                return Content("验证码错误!");
            }

            //校验用户名密码
            string name = Request["LoginCode"];
            string pwd = Request["LoginPwd"];


            var userInfo = UserInfoService.GetEntities(u => u.UName == name && u.Pwd == pwd && u.DelFlag == (short)DelFlagEnum.Normal)
                  .FirstOrDefault();
            if (userInfo == null)
            {
                var cookie1 = Response.Cookies["UserLoginName"];
                if (cookie1 != null)
                    cookie1.Expires = DateTime.Now.AddDays(-1);
                var cookie2 = Response.Cookies["UserLoginPwd"];
                if (cookie2 != null)
                    cookie2.Expires = DateTime.Now.AddDays(-1);
                return Content("用户名密码错误!");
            }


            //判断用户是否选中记住登陆
            if (!string.IsNullOrEmpty(Request["ckAutoLogin"]))
            {
                HttpCookie userNameCookie = new HttpCookie("UserLoginName", name);
                userNameCookie.Expires = DateTime.Now.AddDays(7);
                HttpCookie userPwdCookie = new HttpCookie("UserLoginPwd", pwd);
                userPwdCookie.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Add(userNameCookie);
                Response.Cookies.Add(userPwdCookie);
            }
            else
            {
                var cookie1 = Response.Cookies["UserLoginName"];
                if (cookie1 != null)
                    cookie1.Expires = DateTime.Now.AddDays(-1);
                var cookie2 = Response.Cookies["UserLoginPwd"];
                if (cookie2 != null)
                    cookie2.Expires = DateTime.Now.AddDays(-1);
            }


            //用memcache+cookie替代session
            //            Session["loginUser"] = userInfo; 

            string userLoginGuid = Guid.NewGuid().ToString();

            Common.Cache.CacheHelper.AddCache(userLoginGuid, userInfo, DateTime.Now.AddMinutes(20));
            Response.Cookies["userLoginGuid"].Value = userLoginGuid;
            //Response.Cookies["userLoginGuid"].Expires.AddMinutes(20);

            return Content("ok");
        }

        public ActionResult UserLogout()
        {
            string userLoginGuid = Request.Cookies["userLoginGuid"].Value.ToString();
            Common.Cache.CacheHelper.RemoveCache(userLoginGuid);

            var httpCookie = Response.Cookies["userLoginGuid"]; //清空缓存
            if (httpCookie != null)
                httpCookie.Expires = DateTime.Now.AddDays(-1);

            if (Request.Cookies["UserLoginName"] != null)//清空cookie
            {
                Response.Cookies["UserLoginName"].Expires = DateTime.Now.AddDays(-1);
            }

            if (Request.Cookies["UserLoginPwd"] != null)
            {
                Response.Cookies["UserLoginPwd"].Expires = DateTime.Now.AddDays(-1);
            }

            return Content("ok");
        }

    }
}
