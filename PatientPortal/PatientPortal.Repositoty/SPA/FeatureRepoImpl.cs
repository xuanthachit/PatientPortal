﻿using System.Collections.Generic;
using PatientPortal.Domain.Models.SPA;
using PatientPortal.DataAccess.Repo.SPA;
using PatientPortal.IRepository.SPA;
using System.Threading.Tasks;

namespace PatientPortal.Repositoty.SPA
{
    public class FeatureRepoImpl : IFeatureRepo
    {
        private IFeature _iFeature;

        public FeatureRepoImpl(IFeature iFeature)
        {
            this._iFeature = iFeature;
        }

        public async Task<Feature> SingleQuery(Dictionary<string, object> param)
        {
            return await _iFeature.SingleQuery(param);
        }
        public async Task<List<Feature>> Query(Dictionary<string, object> param)
        {
            return await _iFeature.Query(param);
        }
    }
}
