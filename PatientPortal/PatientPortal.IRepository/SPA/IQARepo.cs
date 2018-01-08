﻿using PatientPortal.Domain.Models.SPA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortal.IRepository.SPA
{
    public interface IQARepo
    {
        Task<Tuple<IEnumerable<QA>, int>> Query(Dictionary<string, dynamic> para);
    }
}