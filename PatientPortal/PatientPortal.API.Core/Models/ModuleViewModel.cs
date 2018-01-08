﻿using System;
using ProtoBuf;

namespace PatientPortal.API.Core.Models
{
    [ProtoContract]
    public class ModuleViewModel
    {
        [ProtoMember(1)]
        public short Id { get; set; }
        [ProtoMember(2)]
        public string Title { get; set; }
        [ProtoMember(3)]
        public string Handler { get; set; }
        [ProtoMember(4)]
        public byte Sort { get; set; }
        [ProtoMember(5)]
        public short ParentId { get; set; }
        [ProtoMember(6)]
        public string Group { get; set; }
        [ProtoMember(7)]
        public string ClassName { get; set; }
    }
}