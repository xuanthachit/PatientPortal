﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatientPortal.PatientServices.Models
{
    [ProtoContract]
    public class ScheduleViewModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Title { get; set; }
        [ProtoMember(3)]
        public DateTime Start { get; set; }
        [ProtoMember(4)]
        public DateTime End { get; set; }
        [ProtoMember(5)]
        public string Detail { get; set; }
    }
}