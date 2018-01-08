﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientPortal.API.CMS.ViewModels
{
    [ProtoContract]
    public class AccessPostViewModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Title { get; set; }
        [ProtoMember(3)]
        public string CategoryName { get; set; }
        [ProtoMember(4)]
        public DateTime PublishDate { get; set; }
    }
}