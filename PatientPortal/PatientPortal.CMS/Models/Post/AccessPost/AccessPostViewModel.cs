﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientPortal.CMS.Models
{
    [ProtoContract]
    public partial class AccessPostViewModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Title { get; set; }
        [ProtoMember(3)]
        public string CategoryName { get; set; }
        [ProtoMember(4)]
        public DateTime PublishDate { get; set; }
        [ProtoMember(5)]
        public string GroupAccess { get; set; }
        [ProtoMember(6)]
        public int[] RoleId { get; set; }
    }
}