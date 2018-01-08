﻿using PatientPortal.IRepository.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientPortal.Domain.Models.CORE;
using PatientPortal.DataAccess.Repo.CORE;

namespace PatientPortal.Repositoty.CORE
{
    public class UserNotificationRepoImpl : IUserNotificationRepo
    {
        private IUserNotification _userNotification;

        public UserNotificationRepoImpl(IUserNotification userNotification)
        {
            this._userNotification = userNotification;
        }

        public async Task<Tuple<IEnumerable<UserNotification>, int>> QueryPaging(Dictionary<string, dynamic> para)
        {
            return await _userNotification.QueryPaging(para);
        }

        public async Task<UserNotification> SingleQuery(Dictionary<string, dynamic> para)
        {
            return await _userNotification.SingleQuery(para);
        }

        public async Task<bool> Transaction(UserNotification data, char action)
        {
            return await _userNotification.Transaction(data, action);
        }
        public async Task<bool> UpdateStatus(UserNotification data, char action)
        {
            return await _userNotification.UpdateStatus(data, action);
        }
    }
}
