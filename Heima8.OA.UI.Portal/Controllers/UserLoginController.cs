using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heima8.OA.Common;
using Heima8.OA.IBLL;
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
            isCheck = false;
        }

        public IUserInfoService UserInfoService { get; set; }

        public ActionResult Index()
        {
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
            string sessionCode = (string) Session["ValidateCode"];
            string inputCode = Request["txtValidateCode"];

            //校验验证码
            Session["ValidateCode"] = null;
            if (string.IsNullOrEmpty(sessionCode)||inputCode!=sessionCode)
            {
                return Content("验证码错误!");
            }

            //校验用户名密码
            string name = Request["LoginCode"];
            string pwd = Request["LoginPwd"];
         

          var userInfo =   UserInfoService.GetEntities(u => u.UName == name && u.Pwd == pwd && u.DelFlag == (short)DelFlagEnum.Normal)
                .FirstOrDefault();
            if (userInfo == null)
            {
                return Content("用户名密码错误!");
            }

//            Session["loginUser"] = userInfo; //用memcache+cookie替代session
            return Content("ok");
        }

    }
}
