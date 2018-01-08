﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientPortal.DataAccess.Repo.CORE;
using PatientPortal.Domain.Models.CORE;
using PatientPortal.IRepository.CORE;

namespace PatientPortal.Repositoty.CORE
{
    public class OfferAdviseRepoImpl : IOfferAdviseRepo
    {
        private IOfferAdvise _offeradvise;

        public OfferAdviseRepoImpl(IOfferAdvise offeradvise)
        {
            this._offeradvise = offeradvise;
        }
        public async Task<bool> CheckExist(Dictionary<string, dynamic> para)
        {
            return await _offeradvise.CheckExist(para);
        }

        public async Task<OfferAdvise> SingleQuery(Dictionary<string, dynamic> para)
        {
            return await _offeradvise.SingleQuery(para);
        }

        public async Task<IEnumerable<OfferAdvise>> Query(Dictionary<string, dynamic> para)
        {
            return await _offeradvise.Query(para);
        }

        public async Task<int> Transaction(OfferAdviseEdit data, char action)
        {
            return await _offeradvise.Transaction(data, action);
        }
    }
}