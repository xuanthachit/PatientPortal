﻿using PatientPortal.Domain.Models.CORE;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientPortal.DataAccess.Repo.CORE
{
    public interface IOfferAdvise
    {
        Task<IEnumerable<OfferAdvise>> Query(Dictionary<string, dynamic> para);
        Task<OfferAdvise> SingleQuery(Dictionary<string, dynamic> para);
        Task<bool> CheckExist(Dictionary<string, dynamic> para);
        Task<int> Transaction(OfferAdviseEdit data, char action);
    }
}
