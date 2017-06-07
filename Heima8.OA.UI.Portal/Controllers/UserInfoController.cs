using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Heima8.OA.IBLL;
using Heima8.OA.Model;
using Heima8.OA.Model.Enum;

namespace Heima8.OA.UI.Portal.Controllers
{
    public class UserInfoController : BaseController
    {
        //
        // GET: /UserInfo/
        //IUserInfoService UserInfoService = new UserInfoService();//Spring.Net
        public IUserInfoService UserInfoService { get; set; }

        public ActionResult Index()
        {
            
            return View();
        }


        public ActionResult GetAllUserInfos()
        {
            int pageSize = int.Parse(Request["rows"]);
            int pageIndex = int.Parse(Request["page"]);
            int total = 0;
            var pageData = UserInfoService.GetPageEntities(pageSize, pageIndex,out total, u => u.DelFlag == (short)DelFlagEnum.Normal,
                u => u.ID, true).Select(u => 
                    new
                    {
                        //id=u.ID,
                        u.ID,
                        u.UName,
                        u.Remark,
                        u.ShowName,
                        u.SubTime,
                        u.ModfiedOn,
                        u.Pwd
                    }).ToList();

            var data = new {total = total, rows = pageData};
       
            return Json(data,JsonRequestBehavior.AllowGet);
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




        #region 添加用户
        public ActionResult Add(UserInfo userInfo)
        {
            userInfo.ModfiedOn = DateTime.Now;
            userInfo.SubTime = DateTime.Now;
            userInfo.DelFlag = (short)Heima8.OA.Model.Enum.DelFlagEnum.Normal;

            UserInfoService.Add(userInfo);

            return Content("ok");
        }
        #endregion


        #region 修改
        public ActionResult Edit(int id)
        {
            ViewData.Model = UserInfoService.GetEntities(u => u.ID == id).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            UserInfoService.Update(userInfo);
            return Content("ok");
        }
        #endregion

        #region 删除

        //public ActionResult Delete(int Id)
        //{

        //    //ViewData.Model = UserInfoService.GetEntities(u => u.ID == Id).FirstOrDefault();
        //    throw (new Exception());

        //    //return View();
        //}

        //[HttpPost]
        //public ActionResult Delete(UserInfo userInfo)
        //{
        //    UserInfoService.Delete(userInfo);
        //    return RedirectToAction("Index");
        //}

        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Content("请选中要删除数据！");
            }

            //正常处理
            string[] strIds = ids.Split(',');
            List<int> idList = new List<int>();
            foreach (var strId in strIds)
            {
                idList.Add(int.Parse(strId));
            }
            //UserInfoService.DeleteList(idList);
            UserInfoService.DeleteListByLogical(idList);
            return Content("ok");
        }
        #endregion



    }
}
