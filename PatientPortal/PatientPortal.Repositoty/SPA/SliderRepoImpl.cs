﻿using PatientPortal.DataAccess.Repo.SPA;
using PatientPortal.Domain.Models.SPA;
using PatientPortal.IRepository.SPA;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientPortal.Repositoty.SPA
{
    public class SliderRepoImpl : ISliderRepo
    {
        private ISlider _iSlider;

        public SliderRepoImpl(ISlider iSlider)
        {
            this._iSlider = iSlider;
        }

        public async Task<List<Slider>> GetAll()
        {
            return await _iSlider.GetAll();
        }
    }
}
