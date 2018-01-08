﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortal.Domain.Models.CMS
{
    public partial class PostStateHistory
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public byte WorkflowStateId { get; set; }
        public string UserId { get; set; }
        public string Note { get; set; }
        //public string CreatedDate { get; set; }

        //public virtual Post Post { get; set; }
    }
}
