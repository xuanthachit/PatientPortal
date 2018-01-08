﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortal.Domain.Models.CORE
{
    public class DoctorProfile
    {
        public string UserId { get; set; }
        public string Speciality { get; set; }
        public string Degrees { get; set; }
        public string Training { get; set; }
        public string Office { get; set; }
        public string Workdays { get; set; }
        public byte DepartmentId { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
    }

    public class DoctorProfileEdit
    {
        public string UserId { get; set; }
        public string Speciality { get; set; }
        public string Degrees { get; set; }
        public string Training { get; set; }
        public string Office { get; set; }
        public string Workdays { get; set; }
        public byte DepartmentId { get; set; }
    }
}
