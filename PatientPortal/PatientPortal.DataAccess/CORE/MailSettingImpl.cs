﻿using PatientPortal.DataAccess.Repo.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientPortal.Domain.Models.CORE;
using System.Transactions;
using PatientPortal.DataAccess.Dapper;
using Dapper;
using PatientPortal.Domain.Utilities;
using PatientPortal.DataAccess.Repo.Wrapper;
using PatientPortal.Domain.Common;
using PatientPortal.Domain.LogManager;

namespace PatientPortal.DataAccess.CORE
{
    public class MailSettingImpl : IMailSetting
    {
        private readonly IAdapterPattern _adapterPattern;

        public MailSettingImpl(IAdapterPattern adapterPattern)
        {
            this._adapterPattern = adapterPattern;
        }

        /// <summary>
        /// Get MailSetting
        /// </summary>
        /// <param name="param">Dictionary</param>
        /// <returns>List MailSetting</returns>
        public async Task<List<MailSetting>> Query(Dictionary<string, object> param)
        {
            try
            {
                var data = await _adapterPattern.ExecuteQuery<MailSetting>(param, "usp_MailSetting", DataConfiguration.instanceCore);
                return data.ToList();

            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Transaction of Dictionary
        /// </summary>
        /// <param name="data">Object MailSetting</param>
        /// <param name="action">I: Insert, u: Update, D: Delete</param>
        /// <returns>true/false</returns>
        public async Task<int> Transaction(MailSetting data, char action)
        {
            try
            {
                var val = await _adapterPattern.SingleTransaction<MailSetting, int>(data, "usp_MailSetting_Transaction", action, DataConfiguration.instanceCore);
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
