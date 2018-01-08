﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatientPortal.API.Core.Models
{
    [ProtoContract]
    public class AppointmentEditModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        [Display(Name = "Ngày đặt hẹn")]
        public DateTime Date { get; set; }

        [ProtoMember(3)]
        [Display(Name = "Thời gian")]
        public int Time { get; set; }

        [Display(Name = "Bác sĩ")]
        [ProtoMember(4)]
        public string PhysicianId { get; set; }

        [Display(Name = "Bệnh nhân")]
        [Required(ErrorMessage = "Thông tin bệnh nhân không hợp lệ.")]

        [ProtoMember(5)]
        public string PatientId { get; set; }

        [Display(Name = "Triệu chứng")]
        [Required(ErrorMessage = "Xin vui lòng điền thông tin triệu chứng khi khám chữa bệnh.")]
        [ProtoMember(6)]
        public string Symptom { get; set; }

        [ProtoMember(7)]
        [Display(Name = "Trạng thái")]
        public byte Status { get; set; }
    }
}