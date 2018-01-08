﻿using PatientPortal.Domain.Models.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortal.IRepository.CORE
{
    public interface IUserNotificationRepo
    {
        Task<UserNotification> SingleQuery(Dictionary<string, dynamic> para);
        Task<bool> Transaction(UserNotification data, char action);
        Task<Tuple<IEnumerable<UserNotification>, int>> QueryPaging(Dictionary<string, dynamic> para);
        Task<bool> UpdateStatus(UserNotification data, char action);
    }
}