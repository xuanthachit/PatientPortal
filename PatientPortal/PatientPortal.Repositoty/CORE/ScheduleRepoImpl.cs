﻿using PatientPortal.DataAccess.Repo.CORE;
using PatientPortal.Domain.Models.CORE;
using PatientPortal.IRepository.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortal.Repositoty.CORE
{
    public class ScheduleRepoImpl: IScheduleRepo
    {
        private readonly ISchedule _schedule;

        public ScheduleRepoImpl(ISchedule schedule)
        {
            this._schedule = schedule;
        }

        public async Task<List<Schedule>> Query(Dictionary<string, object> param)
        {
            return await _schedule.Query(param);
        }

        public async Task<int> Transaction(Schedule data, char action)
        {
            return await _schedule.Transaction(data, action);
        }

        public async Task<Schedule> SingleQuery(Dictionary<string, object> param)
        {
            return await _schedule.SingleQuery(param);
        }
    }
}
