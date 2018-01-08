﻿
using System.Collections.Generic;
using System.Threading.Tasks;
using PatientPortal.Domain.Models.CORE;

namespace PatientPortal.IRepository.CORE
{
    public interface IModuleRepo
    {
        Task<IEnumerable<Module>> Initial(Dictionary<string, dynamic> para);
        Task<IEnumerable<Module>> Query(Dictionary<string, dynamic> para);

        Task<Module> SingleQuery(Dictionary<string, dynamic> para);

        Task<bool> Transaction(Module data, char action);

        Task<bool> CheckExist(Dictionary<string, dynamic> para);
        Task<bool> CheckIsUsed(Dictionary<string, dynamic> para);
    }
}
