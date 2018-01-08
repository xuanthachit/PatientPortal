﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientPortal.CMS.Models
{
    [MetadataTypeAttribute(typeof(PostMetaData))]
    public partial class PostListViewModel
    {
        internal sealed class PostMetaData
        {
            public int Id { get; set; }
            [Display(Name ="Tiêu đề")]
            public string Title { get; set; }
            [Display(Name = "Nhóm bài viết")]
            public string CategoryName { get; set; }
            [Display(Name = "Ngày duyệt bài")]
            public DateTime PublishDate { get; set; }
            public int WorkflowStateId { get; set; }
            [Display(Name = "Trạng thái")]
            public string WorkflowStateName { get; set; }
        }
    }
}