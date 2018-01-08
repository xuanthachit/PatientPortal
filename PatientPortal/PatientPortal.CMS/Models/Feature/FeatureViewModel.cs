﻿using PatientPortal.CMS.Common;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PatientPortal.CMS.Models
{
    [ProtoContract]
    public partial class FeatureViewModel
    {
        public FeatureViewModel()
        {
            Title = Description = Handler = string.Empty;
            Image = ValueConstant.PATH_IMAGE_DEFAULT;
            IsUsed = true;
        }

        [ProtoMember(1)]
        public byte Id { get; set; }

        [ProtoMember(2)]
        [Display(Name = "Tiêu đề")]
        [Remote("CheckExist", "Feature", HttpMethod = "POST", AdditionalFields = "Id", ErrorMessage = "Tiêu đề đã tồn tại. Vui lòng chọn một tên khác.")]
        [Required(ErrorMessage = "Bạn cần nhập tiêu đề")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Title { get; set; }

        [ProtoMember(3)]
        [Display(Name = "Hình ảnh")]
        //[Required(ErrorMessage = "Bạn cần chọn hình ảnh đại diện")]
        public string Image { get; set; }

        [ProtoMember(4)]
        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Bạn cần nhập nội dung")]
        public string Description { get; set; }

        [ProtoMember(5)]
        [Display(Name = "Url")]
        public string Handler { get; set; }

        [ProtoMember(6)]
        [Display(Name = "Trạng thái")]
        public bool IsUsed { get; set; }
    }
}