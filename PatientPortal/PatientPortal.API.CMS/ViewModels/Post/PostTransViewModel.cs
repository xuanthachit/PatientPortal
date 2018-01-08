﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientPortal.API.CMS.ViewModels
{
    [ProtoContract]
    public class PostTransViewModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Image { get; set; }
        [ProtoMember(3)]
        public string Author { get; set; }
        [ProtoMember(4)]
        public byte WorkflowStateId { get; set; }
        [ProtoMember(5)]
        public byte CategoryId { get; set; }
        [ProtoMember(6)]
        public byte Status { get; set; }
        [ProtoMember(7)]
        public string CreatedBy { get; set; }
        [ProtoMember(8)]
        public string ModifiedBy { get; set; }
        [ProtoMember(9)]
        public byte LanguageId { get; set; }
        [ProtoMember(10)]
        public byte Priority { get; set; }
        [ProtoMember(11)]
        public DateTime ExpiredDate { get; set; }
        [ProtoMember(13)]
        public byte Type { get; set; }

        // Post SEO
        [ProtoMember(14)]
        public string TitleSEO { get; set; }
        [ProtoMember(15)]
        public string DescriptionSEO { get; set; }
        [ProtoMember(16)]
        public string Canonical { get; set; }
        [ProtoMember(17)]
        public string BreadcrumbsTitle { get; set; }
        [ProtoMember(18)]
        public byte MetaRobotIndex { get; set; }
        [ProtoMember(19)]
        public byte MetaRobotFollow { get; set; }
        [ProtoMember(20)]
        public byte MetaRobotAdvanced { get; set; }

        // PostTrans
        [ProtoMember(21)]
        public string TitleTrans { get; set; }
        [ProtoMember(22)]
        public string CategoryName { get; set; }
        [ProtoMember(23)]
        public string DescriptionTrans { get; set; }
        [ProtoMember(24)]
        public string Detail { get; set; }
        [ProtoMember(25)]
        public string Tag { get; set; }

        //parameter PostStateHistory
        [ProtoMember(26)]
        public string UserId { get; set; }
        [ProtoMember(27)]
        public string Note { get; set; }
    }
}