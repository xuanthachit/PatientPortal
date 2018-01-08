﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortal.Domain.Models.SPA
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string HomePhone { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string Image { get; set; }
        public short OrganizationId { get; set; }
        public bool Status { get; set; }
        public string PatientId { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDoctor { get; set; }
        public string Tags { get; set; }
    }
}
