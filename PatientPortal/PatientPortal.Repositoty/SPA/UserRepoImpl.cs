﻿using PatientPortal.IRepository.SPA;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatientPortal.DataAccess.Repo.SPA;
using PatientPortal.Domain.Models.SPA;
using System;

namespace PatientPortal.Repositoty.SPA
{
    public class UserRepoImpl : IUserRepo
    {
        private IUser _iUser;

        public UserRepoImpl(IUser iUser)
        {
            this._iUser = iUser;
        }

        public async Task<User> Get(Dictionary<string, object> param)
        {
            return await _iUser.Get(param);
        }

        public async Task<List<User>> GetUserHaveScheduleExamine(Dictionary<string, object> param)
        {
            return await _iUser.GetUserHaveScheduleExamine(param);
        }

        public async Task<List<User>> Query(Dictionary<string, object> param)
        {
            return await _iUser.Query(param);
        }

        public async Task<Tuple<IEnumerable<UserProfile>, int>> GetDoctorList(Dictionary<string, dynamic> param)
        {
            return await _iUser.GetDoctorList(param);
        }

        public async Task<UserProfile> GetUserProfile(Dictionary<string, object> param)
        {
            return await _iUser.GetUserProfile(param);
        }

    }
}
