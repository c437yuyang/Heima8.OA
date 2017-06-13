using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Heima8.OA.DALFactory;
using Heima8.OA.EFDAL;
using Heima8.OA.IBLL;
using Heima8.OA.IDAL;
using Heima8.OA.Model;
using Heima8.OA.NHDAL;

namespace Heima8.OA.BLL
{
    //模块内高内聚。模块间低耦合。

    //变化点：1、跨数据库。有mysql，slqserver2、数据库访问驱动层驱动变化。

    public partial class ActionInfoService : BaseService<ActionInfo>, IActionInfoService //crud
    {
        public bool SetRole(int uid, List<int> rolesList)
        {
            ActionInfo action = DbSession.ActionInfoDal.GetEntities(u => u.ID == uid).FirstOrDefault();

            List<RoleInfo> allRoles = DbSession.RoleInfoDal.GetEntities(r => rolesList.Contains(r.ID)).ToList();

            action.RoleInfo.Clear(); //清除以前的

            foreach (var role in allRoles)
            {
                action.RoleInfo.Add(role);
            }
            return DbSession.SaveChanges() >= 0;

            //这里如果之前选中的一些，现在选中和之前一样的，那么就会出现savechanges为0的情况
            //所以直接返回true或者>=0了

        }
    }
}
