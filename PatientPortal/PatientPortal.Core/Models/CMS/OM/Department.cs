﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortal.Domain.Models.CMS
{
    public partial class Department
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Handler { get; set; }
        public byte Sort { get; set; }
        public bool IsUsed { get; set; }
    }
}
