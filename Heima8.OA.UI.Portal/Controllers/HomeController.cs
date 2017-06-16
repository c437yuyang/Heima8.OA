using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Heima8.OA.BLL;
using Heima8.OA.IBLL;
using Heima8.OA.Model;
using Heima8.OA.Model.Enum;

namespace Heima8.OA.UI.Portal.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        public IUserInfoService UserInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }
        public ActionResult Index()
        {
            ViewBag.AllMenu = LoadUserMenu();
            IsCheckAction = false; //主页不校验权限
            return View();
        }

        private List<ActionInfo> LoadUserMenu()
        {
            //if (this.LoginUser == null)
            //{
            //    return null;
            //}

            int userId = this.LoginUser.ID;
            var user = UserInfoService.GetEntities(u => u.ID == userId).FirstOrDefault();
            var allRole = user.RoleInfo;

            ViewBag.LoginUser = user;

            var allRoleActionIDs = (from r in allRole
                                    from a in r.ActionInfo
                                    select a.ID).ToList();

            var allDenyActionIDs = (from r in user.R_UserInfo_ActionInfo
                                    where r.HasPermission == false
                                    select r.ActionInfoID).ToList();

            var allActionIDs = (from a in allRoleActionIDs
                                where !allDenyActionIDs.Contains(a)
                                select a).ToList();

            var allUserActionIDs = from a in user.R_UserInfo_ActionInfo
                                   where a.HasPermission == true
                                   select a.ID;
            allActionIDs.AddRange(allUserActionIDs.ToList());
            allActionIDs = allActionIDs.Distinct().ToList();

            var actionList = ActionInfoService.GetEntities(u => allActionIDs.Contains(u.ID)
                                                                && u.IsMenu
                                                                && u.DelFlag == (short)DelFlagEnum.Normal)
                                                                .ToList();

            foreach (ActionInfo actionInfo in actionList)
            {
                if (string.IsNullOrEmpty(actionInfo.MenuIcon))
                {
                    actionInfo.MenuIcon = "";
                }
            }

            return actionList;

        }
    }
}
