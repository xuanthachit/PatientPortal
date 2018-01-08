﻿using PatientPortal.DataAccess.Repo.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientPortal.Domain.Models.CORE;
using System.Transactions;
using PatientPortal.Domain.Utilities;
using PatientPortal.DataAccess.Dapper;
using Dapper;
using PatientPortal.DataAccess.Repo.Wrapper;
using PatientPortal.Domain.Common;
using PatientPortal.Domain.LogManager;

namespace PatientPortal.DataAccess.CORE
{
    public class ArticleCommentImpl : IArticleComment
    {
        private readonly IAdapterPattern _adapterPattern;

        public ArticleCommentImpl(IAdapterPattern adapterPattern)
        {
            this._adapterPattern = adapterPattern;
        }

        /// <summary>
        /// Transaction of ArticleComment
        /// </summary>
        /// <param name="data"></param>
        /// <param name="action"></param>
        /// <returns>true/false</returns>
        public async Task<int> Transaction(ArticleCommentEdit data, char action)
        {
            try
            {
                var val = await _adapterPattern.SingleTransaction<ArticleCommentEdit, int>(data, "usp_ArticleComment_Transaction", action, DataConfiguration.instanceCore);
                return val;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return 0;
            }
        }

        /// <summary>
        /// Get List ArticleComment
        /// </summary>
        /// <param name="param"></param>
        /// <returns>List ArticleComment</returns>
        public async Task<List<ArticleComment>> Query(Dictionary<string, object> param)
        {
            try
            {
                var data = await _adapterPattern.ExecuteQuery<ArticleComment>(param, "usp_ArticleComment", DataConfiguration.instanceCore);
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public async Task<ArticleComment> SingleQuery(Dictionary<string, object> param)
        {
            try
            {
                return await _adapterPattern.SingleExecuteQuery<ArticleComment>(param, "usp_ArticleComment", DataConfiguration.instanceCore);
                
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }
    }
}
