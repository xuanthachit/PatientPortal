﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PatientPortal.CMS.Models
{
    public class GalleryModel
    {
        public GalleryViewModel Gallery { get; set; }
        public List<GalleryViewModel> lstGallery { get; set; }
    }
    [ProtoContract]
    public partial class GalleryViewModel
    {
        public GalleryViewModel()
        {
            Id = Title = Description = Img = Highlight = YoutubeURL = Date = string.Empty;
            IsMultiple = false;
        }
        [ProtoMember(1)]
        public string Id { get; set; }
        [ProtoMember(2)]
        [Display(Name = "Tiêu đề")]
        [Required(ErrorMessage = "Bạn cần nhập tiêu đề")]
        public string Title { get; set; }
        [ProtoMember(3)]
        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Bạn cần nhập nội dung hiển thị")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Description { get; set; }
        [ProtoMember(4)]
        [Display(Name = "Thông tin nổi bật")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Highlight { get; set; }
        [ProtoMember(5)]
        [Display(Name = "Hình ảnh")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Img { get; set; }
        [ProtoMember(6)]
        [Display(Name = "URL Youtube")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string YoutubeURL { get; set; }
        [ProtoMember(7)]
        [Display(Name = "Ngày")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Date { get; set; }
        [ProtoMember(8)]
        [Display(Name = "Chuyên khoa")]
        [Required(ErrorMessage = "Bạn cần chọn chuyên khoa")]
        public short DepartmentId { get; set; }
        [ProtoMember(9)]
        [Display(Name = "Bộ sưu tập?")]
        public bool IsMultiple { get; set; }
    }
}