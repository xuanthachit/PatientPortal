﻿using System;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;

namespace PatientPortal.API.Core.Models
{
    [ProtoContract]
    public class ArticleViewModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [ProtoMember(3)]
        public string Title { get; set; }

        [ProtoMember(4)]
        public string Detail { get; set; }

        [ProtoMember(5)]
        public string PatientId { get; set; }

        [ProtoMember(6)]
        public byte Status { get; set; }

        [ProtoMember(7)]
        public bool IsClosed { get; set; }

        [ProtoMember(8)]
        public int CountComments { get; set; }

        [ProtoMember(9)]
        public string AuthorName { get; set; }

        [ProtoMember(10)]
        public string ImageProfile { get; set; }
    }
}