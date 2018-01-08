﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatientPortal.API.CMS.ViewModels
{
    [ProtoContract]
    public class WorkflowStateViewModel
    {
        [Required]
        [ProtoMember(1)]
        public short Id { get; set; }
        [Required]
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public byte WorkflowId { get; set; }
        [ProtoMember(4)]
        public bool IsActive { get; set; }
        [ProtoMember(5)]
        public bool IsFirst { get; set; }
    }
}