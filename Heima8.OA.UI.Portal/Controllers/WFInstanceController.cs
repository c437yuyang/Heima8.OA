using System;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Heima8.OA.IBLL;

namespace Heima8.OA.UI.Portal.Controllers
{
    public class WFInstanceController : Controller
    {
        //
        // GET: /WFInstance/

        public IWF_TempService Wf_TempService { get; set; }
        public IWF_InstanceService WF_InstanceService { get; set; }
        public IUserInfoService UserInfoService { get; set; }
        short delFlag = (short)Model.Enum.DelFlagEnum.Normal;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(int id)
        {



            ViewBag.Template = Wf_TempService.GetEntities(u => u.ID == id).FirstOrDefault();


            var allUsers = UserInfoService.GetEntities(u => u.DelFlag == delFlag).ToList();
            ViewData["UserList"] = (from u in allUsers
                select new SelectListItem()
                {
                    Selected = false,
                    Text = u.UName,
                    //Value = SqlFunctions.StringConvert((double)u.ID)
                    Value = u.ID + "" //linq里面不能直接.tostring来进行类型转换
                }).ToList();

            return View();
        }

    }
}
