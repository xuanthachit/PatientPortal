﻿using PatientPortal.Domain.Models.SPA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortal.DataAccess.Repo.SPA
{
    public interface IAppointmentLog
    {
        Task<bool> Transaction(AppointmentLogEdit data, char action);
        Task<List<Schedule>> GetScheduleExamine(Dictionary<string, object> param);
    }
}