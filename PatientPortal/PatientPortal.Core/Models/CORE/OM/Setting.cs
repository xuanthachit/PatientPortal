﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortal.Domain.Models.CORE
{
    public class Setting
    {
        public byte Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public bool Membership { get; set; }
        public string DefaultRole { get; set; }
        public string LoginURL { get; set; }
        public string LockedIPNoteDefault { get; set; }
        public bool IsSaveCanceledAppointment { get; set; }
        public byte AppointmentIntervalTime { get; set; }
        public int AppointmentStartTime { get; set; }
        public int AppointmentEndTime { get; set; }
    }
}
