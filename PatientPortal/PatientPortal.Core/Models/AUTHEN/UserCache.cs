﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using PatientPortal.Domain.Common;

namespace PatientPortal.Domain.Models.AUTHEN
{
    public class UserCache
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public string PatientId { get; set; }
    }
}
