﻿using System;
using ProtoBuf;

namespace PatientPortal.API.Core.Models
{
    [ProtoContract]
    public class OfferAdviseViewModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public DateTime Date { get; set; }
        [ProtoMember(3)]
        public string Detail { get; set; }
        [ProtoMember(4)]
        public string PatientId { get; set; }
        [ProtoMember(5)]
        public string Tag { get; set; }
        [ProtoMember(6)]
        public byte Status { get; set; }
        [ProtoMember(7)]
        public string Message { get; set; }
    }
}