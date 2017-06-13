using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heima8.OA.IBLL;
using Heima8.OA.Model;

namespace Heima8.OA.UI.Portal.Controllers
{
    public class RoleInfoController : BaseController
    {
        //
        // GET: /RoleInfo/

        public IRoleInfoService RoleInfoService { get; set; }
        short delFlag = (short)Model.Enum.DelFlagEnum.Normal;

        public ActionResult Index()
        {
            return View();
        }


        #region Add
        public ActionResult Add(RoleInfo roleInfo)
        {
            roleInfo.ModfiedOn = DateTime.Now;
            roleInfo.SubTime = DateTime.Now;
            roleInfo.DelFlag = delFlag;
            RoleInfoService.Add(roleInfo);
            return Content("ok");
        }
        #endregion


        #region 查询
        public ActionResult GetAllRoleInfos()
        {
            int pageSize = int.Parse(Request["rows"]);
            int pageIndex = int.Parse(Request["page"]);
            int total = 0;
            #region 直接查询分页数据
            var pageData = RoleInfoService.GetPageEntities(pageSize, pageIndex, out total, u => u.DelFlag == delFlag,
                u => u.ID, true).Select(u => //select防止出现导航属性的递归调用
                    new
                    {
                        //id=u.ID,
                        u.ID,
                        u.RoleName,
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
            RoleInfoService.DeleteListByLogical(idList);
            return Content("ok");
        }
        #endregion


        #region 修改
        public ActionResult Edit(int id)
        {
            ViewData.Model = RoleInfoService.GetEntities(u => u.ID == id).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(RoleInfo model)
        {
            RoleInfoService.Update(model);
            return Content("ok");
        }
        #endregion

    }
}
