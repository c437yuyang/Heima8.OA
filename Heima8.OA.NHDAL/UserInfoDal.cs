﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Heima8.OA.IDAL;
using Heima8.OA.Model;

namespace Heima8.OA.NHDAL
{
    public class UserInfoDal :IUserInfoDal
    {
        public IQueryable<UserInfo> GetEntities(Expression<Func<UserInfo, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserInfo> GetPageEntities<S>(int pageSize, int pageIndex, out int total, Expression<Func<UserInfo, bool>> whereLambda,
            Expression<Func<UserInfo, S>> orderByLambda, bool isAsc)
        {
            throw new NotImplementedException();
        }

        public UserInfo Add(UserInfo entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(UserInfo entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(UserInfo entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByLogical(int id)
        {
            throw new NotImplementedException();
        }
    }
}
