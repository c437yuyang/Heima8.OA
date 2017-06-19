using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heima8.OA.IBLL;
using Heima8.OA.Model;

namespace Heima8.OA.UI.Portal.Controllers
{
    public class WFTempController : Controller
    {
        //
        // GET: /WFTemp/
        public IWF_TempService WF_TempService { get; set; }
        short delFlag = (short)Model.Enum.DelFlagEnum.Normal;


        public ActionResult Index()
        {
            return View();
        }

        #region 查询
        public ActionResult GetAllWFTemps()
        {
            int pageSize = int.Parse(Request["rows"]);
            int pageIndex = int.Parse(Request["page"]);
            int total = 0;
            #region 直接查询分页数据
            var pageData = WF_TempService.GetPageEntities(pageSize, pageIndex, out total, u => u.DelFlag == delFlag,
                u => u.ID, true).Select(u => //select防止出现导航属性的递归调用
                    new
                    {
                        //id=u.ID,
                        u.ID,
                        u.TepName,
                        u.Remark,
                        u.SubTime,
                        u.ActityType,
                        u.DelFlag
                    }).ToList();
            #endregion

            var data = new { total = total, rows = pageData };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Add

        public ActionResult Add()
        {
            return View();
        }


        [HttpPost]
        [ValidateInput(false)] //避免出现危险值的提示
        public ActionResult Add(WF_Temp WF_Temp)
        {
            WF_Temp.SubTime = DateTime.Now;
            WF_Temp.DelFlag = delFlag;
            WF_TempService.Add(WF_Temp);
            return Content("ok");
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
            WF_TempService.DeleteListByLogical(idList);
            return Content("ok");
        }
        #endregion

        //#region 修改
        public ActionResult Edit(int id)
        {
            ViewData.Model = WF_TempService.GetEntities(u => u.ID == id).FirstOrDefault();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(WF_Temp model)
        {
            model.SubTime = DateTime.Now;
            
            WF_TempService.Update(model);
            return Content("ok");
        }
        //#endregion

        public ActionResult StartWF()
        {
            var data = WF_TempService.GetEntities(u => u.DelFlag == delFlag).ToList();
            ViewData.Model = data;
            return View();
        }

    }
}
