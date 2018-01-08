﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortal.Domain.Models.CMS
{
    public partial class PostList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public DateTime PublishDate { get; set; }
        public int WorkflowStateId { get; set; }
        public string WorkflowStateName { get; set; }
        public byte CategoryId { get; set; }
        public byte Roles { get; set; }
    }
}
