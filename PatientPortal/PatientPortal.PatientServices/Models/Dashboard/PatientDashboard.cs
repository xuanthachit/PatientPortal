﻿using PatientPortal.Domain.Models.CORE.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientPortal.PatientServices.Models.Dashboard
{
    public class PatientDashboard
    {
        public List<RPAppointment> lstAppointment { get; set; }
        public List<ArticleViewModel> lstArticle { get; set; }
    }
}