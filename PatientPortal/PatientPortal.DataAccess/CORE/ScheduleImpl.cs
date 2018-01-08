﻿using PatientPortal.DataAccess.Repo.CORE;
using PatientPortal.DataAccess.Repo.Wrapper;
using PatientPortal.Domain.Common;
using PatientPortal.Domain.LogManager;
using PatientPortal.Domain.Models.CORE;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;

namespace PatientPortal.DataAccess.CORE
{
    public class ScheduleImpl : ISchedule
    {
        private readonly IAdapterPattern _adapterPattern;
        private OleDbConnection Econ;

        public ScheduleImpl(IAdapterPattern adapterPattern)
        {
            this._adapterPattern = adapterPattern;
        }

        /// <summary>
        /// Get Schedule
        /// </summary>
        /// <param name="param">Dictionary</param>
        /// <returns>List Schedule</returns>
        public async Task<List<Schedule>> Query(Dictionary<string, object> param)
        {
            try
            {
                var data = await _adapterPattern.ExecuteQuery<Schedule>(param, "usp_Schedule", DataConfiguration.instanceCore);
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// get Schedule detail
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Schedule> SingleQuery(Dictionary<string, object> param)
        {
            try
            {
                return await _adapterPattern.SingleExecuteQuery<Schedule>(param, "usp_Schedule", DataConfiguration.instanceCore);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Trasaction of Schedule
        /// </summary>
        /// <param name="data">Object Schedule</param>
        /// <param name="action">I: Insert, U: Update, D: Delete</param>
        /// <returns>true/false</returns>
        public async Task<int> Transaction(Schedule data, char action)
        {
            try
            {
                var val = await _adapterPattern.SingleTransaction<Schedule, int>(data, "usp_Schedule_Transaction", action, DataConfiguration.instanceCore);
                return val;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return 0;
            }
        }
    }
}