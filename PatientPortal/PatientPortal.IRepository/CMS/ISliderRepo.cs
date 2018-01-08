﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientPortal.Domain.Models.CMS;

namespace PatientPortal.IRepository.CMS
{
    public interface ISliderRepo
    {
        Task<Slider> SingleQuery(Dictionary<string, dynamic> para);
        Task<IEnumerable<Slider>> Query(Dictionary<string, dynamic> para);
        Task<bool> Transaction(Slider data, char action);
        Task<bool> CheckExist(Dictionary<string, dynamic> para);
    }
}
