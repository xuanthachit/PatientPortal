﻿using PatientPortal.Domain.Models.CORE;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientPortal.DataAccess.Repo.CORE
{
    public interface IAppointmentLog
    {
        Task<List<Schedule>> GetScheduleExamine(Dictionary<string, object> param);
        Task<bool> Transaction(AppointmentLogEdit data, char action);
        Task<List<AppointmentLog>> Query(Dictionary<string, object> param);
        Task<AppointmentLog> SingleQuery(Dictionary<string, object> param);
        Task<bool> Confirm(AppointmentLogEdit data, char action);
        Task<bool> ApprovedBook(Dictionary<string, object> param);
        Task<List<AppointmentLog>> QuerySearch(Dictionary<string, object> param);
    }
}
