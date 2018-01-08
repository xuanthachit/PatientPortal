﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortal.Domain.Models.CORE
{
    public class ArticleComment
    {
        public short Id { get; set; }
        public int ArticleId { get; set; }
        public DateTime Date { get; set; }
        public string Detail { get; set; }
        public string CreatedUser { get; set; }
        public byte Status { get; set; }
        public string Author { get; set; }
        public byte[] ImageProfile { get; set; }
    }
}
