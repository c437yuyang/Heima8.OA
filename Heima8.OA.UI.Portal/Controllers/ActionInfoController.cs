using Heima8.OA.BLL;
using Heima8.OA.IBLL;
using Heima8.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heima8.OA.UI.Portal.Controllers
{
    public class ActionInfoController : Controller
    {
        //
        // GET: /ActionInfo/
        short delFlag = (short)Model.Enum.DelFlagEnum.Normal;

        public IActionInfoService ActionInfoService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        #region Add
        public ActionResult Add(ActionInfo actionInfo)
        {
            actionInfo.ModfiedOn = DateTime.Now;
            actionInfo.SubTime = DateTime.Now;
            actionInfo.DelFlag = delFlag;
            ActionInfoService.Add(actionInfo);
            return Content("ok");
        }
        #endregion


        #region 查询
        public ActionResult GetAllActionInfos()
        {
            int pageSize = int.Parse(Request["rows"]);
            int pageIndex = int.Parse(Request["page"]);
            int total = 0;
            #region 直接查询分页数据
            var pageData = ActionInfoService.GetPageEntities(pageSize, pageIndex, out total, u => u.DelFlag == delFlag,
                u => u.ID, true).Select(u => //select防止出现导航属性的递归调用
                    new
                    {
                        //id=u.ID,
                        u.ID,
                        u.ActionName,
                        u.Url,
                        u.HttpMethd,
                        u.IsMenu,
                        u.MenuIcon,
                        u.Sort,
                        u.Remark,
                        u.SubTime,
                        u.ModfiedOn
                    }).ToList();
            #endregion

            var data = new { total = total, rows = pageData };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region 删除
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
            ActionInfoService.DeleteListByLogical(idList);
            return Content("ok");
        }
        #endregion


        #region 修改
        public ActionResult Edit(int id)
        {
            ViewData.Model = ActionInfoService.GetEntities(u => u.ID == id).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ActionInfo model)
        {
            ActionInfoService.Update(model);
            return Content("ok");
        }
        #endregion


        #region 上传图片处理
        public ActionResult UploadImage()
        {
            var file = Request.Files["fileMenuIcon"];
            string path = "/UploadFiles/UploadImages/" + Guid.NewGuid().ToString() + "-" + file.FileName;
            file.SaveAs(Request.MapPath(path));
            return Content(path);
        }

        #endregion


    }
}
