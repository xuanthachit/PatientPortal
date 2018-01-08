﻿using PatientPortal.Domain.Models.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortal.DataAccess.Repo.CORE
{
    public interface IModule
    {
        Task<IEnumerable<Module>> Query(Dictionary<string, dynamic> para, string procedureName);
        Task<Module> SingleQuery(Dictionary<string, dynamic> para);
        Task<bool> Transaction(Module data, char action);
        Task<bool> CheckExist(Dictionary<string, dynamic> para);
        Task<bool> CheckIsUsed(Dictionary<string, dynamic> para);
    }
}
