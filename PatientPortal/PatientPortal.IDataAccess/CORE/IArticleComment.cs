﻿using PatientPortal.Domain.Models.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortal.DataAccess.Repo.CORE
{
    public interface IArticleComment
    {
        Task<ArticleComment> SingleQuery(Dictionary<string, object> param);

        Task<List<ArticleComment>> Query(Dictionary<string, object> param);

        Task<int> Transaction(ArticleCommentEdit data, char action);
    }
}
