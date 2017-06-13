using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Heima8.OA.IBLL;
using Heima8.OA.Model;
using Heima8.OA.Model.Enum;
using Heima8.OA.Model.Param;

namespace Heima8.OA.UI.Portal.Controllers
{
    public class UserInfoController : BaseController
    {
        //
        // GET: /UserInfo/
        //IUserInfoService UserInfoService = new UserInfoService();//Spring.Net
        public IUserInfoService UserInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }
        public IR_UserInfo_ActionInfoService R_UserInfo_ActionInfoService { get; set; }

        private readonly short delFlag = (short)Heima8.OA.Model.Enum.DelFlagEnum.Normal;
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult GetAllUserInfos()
        {
            int pageSize = int.Parse(Request["rows"]);
            int pageIndex = int.Parse(Request["page"]);

            //过滤的用户名   过滤备注schName   schRemark
            string schName = Request["schName"];
            string schRemark = Request["schRemark"];
            int total = 0;

            var queryParam = new UserQueryParam()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Total = 0,
                SchName = schName,
                SchRemark = schRemark
            };

            var pageData = UserInfoService.GetEntitiesByParam(queryParam);

            var temp = pageData.Select(u => new
            {
                u.ID,
                u.UName,
                u.ShowName,
                u.SubTime,
                u.ModfiedOn,
                u.Pwd,
                u.Remark
            }).ToList();



            #region 直接查询分页数据
            //var pageData = UserInfoService.GetPageEntities(pageSize, pageIndex,out total, u => u.DelFlag == (short)DelFlagEnum.Normal,
            //    u => u.ID, true).Select(u => 
            //        new
            //        {
            //            //id=u.ID,
            //            u.ID,
            //            u.UName,
            //            u.Remark,
            //            u.ShowName,
            //            u.SubTime,
            //            u.ModfiedOn,
            //            u.Pwd
            //        }).ToList(); 
            #endregion

            var data = new { total = total, rows = temp };

            return Json(data, JsonRequestBehavior.AllowGet);
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

            if (UserInfoService.Add(userInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("添加失败，请检查用户是否已经存在!");
            }

            
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



        #region 设置用户角色
        public ActionResult SetRole(int id)
        {
            int userid = id;
            UserInfo model = UserInfoService.GetEntities(u => u.ID == id).FirstOrDefault();
            //把所有的角色发送 到前台
            ViewBag.AllRoles = RoleInfoService.GetEntities(u => u.DelFlag == delFlag).ToList();

            //用户已经关联的角色发送到前台。
            ViewBag.ExitsRoles = (from r in model.RoleInfo
                                  select r.ID).ToList();

            return View(model);
        }

        public ActionResult ProcessSetRole(int hidUId) //传入的值是string或者int都可以，会自动进行转换
        {
            //1.拿到用户的ID
            //2.所有勾上的ckb的值
            int userId = int.Parse(Request["hidUId"]);
            List<int> rolesList = new List<int>();
            foreach (var key in Request.Form.AllKeys) //这里的key就是表单里面的name属性的值
            {
                if (key.StartsWith("ckb_"))
                {
                    rolesList.Add(int.Parse(key.Replace("ckb_", "")));
                }
            }


            //3.为相应的用户设置相应的角色
            if (UserInfoService.SetRole(userId, rolesList))
            {
                return Content("ok");
            }
            else
            {
                return Content("fail");
            }
        }

        #endregion


        #region 设置用户特殊权限
        public ActionResult SetAction(int id)
        {
            int userid = id;
            UserInfo model = UserInfoService.GetEntities(u => u.ID == id).FirstOrDefault();
            ViewBag.User = model;

            //传回已经存在的权限信息
            var existedActions =
                R_UserInfo_ActionInfoService.GetEntities(u => u.UserInfoID == userid && u.DelFlag == (short)DelFlagEnum.Normal)
                    .ToList();

            ViewBag.ExistedActions = existedActions;
            ViewData.Model = ActionInfoService.GetEntities(u => u.DelFlag == delFlag).ToList();

            return View();
        }

        //做一个删除  特殊权限。
        public ActionResult DeleteUserAction(int UId, int ActionId)
        {

            var rUserAction = R_UserInfo_ActionInfoService.GetEntities(r => r.ActionInfoID == ActionId && r.UserInfoID == UId)
                                        .FirstOrDefault();
            if (rUserAction != null)
            {
                //rUserAction.DelFlag = (short) Heima8.OA.Model.Enum.DelFlagEnum.Deleted;
                R_UserInfo_ActionInfoService.DeleteListByLogical(new List<int>() { rUserAction.ID });
            }
            return Content("ok");
        }

        //设置当前用户的特殊权限
        public ActionResult SetUserAction(int UId, int ActionId, int Value)
        {
            //{UId:uId,ActionId:actionId,Value:value}
            var rUserAction = R_UserInfo_ActionInfoService.GetEntities(r => r.ActionInfoID == ActionId && 
                                                                        r.UserInfoID == UId && r.DelFlag == delFlag)
                                                                        .FirstOrDefault();
            if (rUserAction != null)
            {
                rUserAction.HasPermission = Value == 1 ? true : false;
                R_UserInfo_ActionInfoService.Update(rUserAction);
            }
            else
            {
                R_UserInfo_ActionInfo rUserInfoActionInfo = new R_UserInfo_ActionInfo();
                rUserInfoActionInfo.ActionInfoID = ActionId;
                rUserInfoActionInfo.UserInfoID = UId;
                rUserInfoActionInfo.HasPermission = Value == 1 ? true : false;
                rUserInfoActionInfo.DelFlag = delFlag;
                R_UserInfo_ActionInfoService.Add(rUserInfoActionInfo);
            }

            return Content("ok");
        }

        #endregion


    }
}
