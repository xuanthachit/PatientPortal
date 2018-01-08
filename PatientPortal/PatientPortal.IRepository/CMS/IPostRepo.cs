﻿using PatientPortal.Domain.Models.CMS;
using PatientPortal.Domain.Models.CMS.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortal.IRepository.CMS
{
    public interface IPostRepo
    {
        Task<Post> SingleQuery(Dictionary<string, dynamic> para);
        Task<IEnumerable<AccessPost>> QueryAccess(Dictionary<string, dynamic> para);
        Task<IEnumerable<PostList>> Query(Dictionary<string, dynamic> para);
        Task<IEnumerable<DashboardCounter>> Counter(Dictionary<string, dynamic> para);
        Task<int> Transaction(PostEdit data, char action);
        Task<bool> Transaction(PostStateHistory data, char action);

        //post trans
        Task<PostTran> SingleQueryPostTran(Dictionary<string, dynamic> para);
        Task<bool> Transaction(PostTran data, char action);
    }
}
