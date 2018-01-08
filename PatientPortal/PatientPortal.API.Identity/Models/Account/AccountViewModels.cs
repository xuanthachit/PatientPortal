﻿using ProtoBuf;
using System;
using System.Collections.Generic;

namespace PatientPortal.API.Identity.Models
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }

    [ProtoContract]
    public class UserSecretInfoViewModel
    {
        [ProtoMember(1)]
        public string Email { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public string NameIdentifier { get; set; }
        [ProtoMember(4)]
        public byte[] Image { get; set; }
        [ProtoMember(5)]
        public string PatientId { get; set; }
    }

    [ProtoContract]
    public class UserEditViewModel
    {
        [ProtoMember(1)]
        public string Id { get; set; }
        [ProtoMember(2)]
        public string Email { get; set; }

        [ProtoMember(3)]
        public string Name { get; set; }

        [ProtoMember(4)]
        public string PatientId { get; set; }

        [ProtoMember(5)]
        public DateTime? DoB { get; set; }

        [ProtoMember(6)]
        public byte Gender { get; set; }

        [ProtoMember(7)]
        public byte[] Image { get; set; }

        [ProtoMember(8)]
        public int OrganizationId { get; set; }

        [ProtoMember(9)]
        public bool IsDoctor { get; set; }

        [ProtoMember(10)]
        public string Tag { get; set; }

        [ProtoMember(11)]
        public string[] RoleName { get; set; }
        [ProtoMember(12)]
        public string PhoneNumber { get; set; }
        [ProtoMember(13)]
        public string Address { get; set; }
        [ProtoMember(14)]
        public string PersonalId { get; set; }
        [ProtoMember(15)]
        public string InsuranceId { get; set; }
    }

    [ProtoContract]
    public class UserListViewModel
    {
        [ProtoMember(1)]
        public string Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public string UserName { get; set; }
        [ProtoMember(4)]
        public string Email { get; set; }
        [ProtoMember(5)]
        public byte[] Image { get; set; }
        [ProtoMember(6)]
        public string GroupMember { get; set; }
    }
}