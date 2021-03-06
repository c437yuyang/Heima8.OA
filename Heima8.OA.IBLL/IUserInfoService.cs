﻿using Heima8.OA.Model;
using System.Collections.Generic;
using System.Linq;

namespace Heima8.OA.IBLL
{
    public partial interface IUserInfoService:IBaseService<UserInfo>
    {
        IQueryable<UserInfo> GetEntitiesByParam(Model.Param.UserQueryParam userQueryParam);

        bool SetRole(int uid, List<int> rolesList);

    }
}
