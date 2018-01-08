﻿using PatientPortal.DataAccess.Repo.CORE;
using PatientPortal.Domain.Models.CORE;
using PatientPortal.IRepository.CORE;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace PatientPortal.Repositoty.CORE
{
    public class UserRepoImpl : IUserRepo
    {
        private readonly IUser _user;

        public UserRepoImpl(IUser user)
        {
            this._user = user;
        }

        public async Task<List<User>> GetUserHaveScheduleExamine(Dictionary<string, object> param)
        {
            return await _user.GetUserHaveScheduleExamine(param);
        }

        public async Task<List<User>> GetUsersByGroupType(Dictionary<string, object> param)
        {
            return await _user.GetUsersByGroupType(param);
        }

        public async Task<List<User>> Query(Dictionary<string, object> param)
        {
            return await _user.Query(param);
        }

        public async Task<User> SingleQuery(Dictionary<string, object> param)
        {
            return await _user.SingleQuery(param);
        }

        public async Task<int> Transaction(User data, char action)
        {
            return await _user.Transaction(data, action);
        }

        public async Task<string> GetUserId(Dictionary<string, object> param)
        {
            return await _user.GetUserId(param);
        }
    }
}
