using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heima8.OA.BLL;
using Heima8.OA.IBLL;
using Heima8.OA.Model;

namespace Heima8.OA.UI.Portal.Controllers
{
    public class UserInfoController : Controller
    {
        //
        // GET: /UserInfo/
        //IUserInfoService UserInfoService = new UserInfoService();//Spring.Net
        public IUserInfoService UserInfoService { get; set; }

        public ActionResult Index()
        {
            ViewData.Model = UserInfoService.GetEntities(u => true);
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserInfo userInfo)
        {
            if (ModelState.IsValid)//表单校验是否成功
            {
                UserInfoService.Add(userInfo);
            }
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int Id)
        {

            ViewData.Model = UserInfoService.GetEntities(u => u.ID == Id).FirstOrDefault();

           
            return View();
        }

        [HttpPost]
        public ActionResult Delete(UserInfo userInfo)
        {
            UserInfoService.Delete(userInfo);
            return RedirectToAction("Index");
        }

    }
}
