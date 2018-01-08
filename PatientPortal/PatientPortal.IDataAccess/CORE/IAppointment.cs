﻿using PatientPortal.Domain.Models.CORE;
using PatientPortal.Domain.Models.CORE.Report;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientPortal.DataAccess.Repo.Core
{
    public interface IAppointment
    {
        Task<List<AppointmentEdit>> Query(Dictionary<string, object> param);
        Task<List<RPAppointment>> GetRPAppointment(Dictionary<string, object> param);
        Task<List<Schedule>> GetScheduleExamine(Dictionary<string, object> param);
        Task<int> Transaction(AppointmentEdit data, char action);
    }
}
