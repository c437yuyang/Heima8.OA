using Heima8.OA.Model;
using System.Collections.Generic;
using System.Linq;

namespace Heima8.OA.IBLL
{
    public partial interface IActionInfoService:IBaseService<ActionInfo>
    {
        bool SetRole(int uid, List<int> rolesList);
    }
}
