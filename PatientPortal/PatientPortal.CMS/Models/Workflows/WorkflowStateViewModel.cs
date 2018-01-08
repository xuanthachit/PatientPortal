﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientPortal.CMS.Models
{
    [ProtoContract]
    public partial class WorkflowStateViewModel
    {
        [ProtoMember(1)]
        public short Id { get; set; }

        [ProtoMember(2)]
        [Display(Name = "Nhập tên")]
        [Required(ErrorMessage = "Nhập tên.")]
        [Remote("CheckExist", "WorkflowState", HttpMethod = "POST", AdditionalFields = "Id,WorkflowId", ErrorMessage = "Tên đã tồn tại. Vui lòng chọn tên khác!!!")]
        public string Name { get; set; }

        [ProtoMember(3)]
        [Display(Name = "Workflow")]
        public byte WorkflowId { get; set; }

        [ProtoMember(4)]
        [Display(Name = "Tình trạng")]
        public bool IsActive { get; set; }

        [ProtoMember(5)]
        [Display(Name = "Là bước đầu tiên")]
        public bool IsFirst { get; set; }
    }
}