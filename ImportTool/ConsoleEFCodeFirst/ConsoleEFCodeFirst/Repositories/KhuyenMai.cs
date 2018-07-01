﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleEFCodeFirst.Repositories
{
    [System.ComponentModel.DataAnnotations.Schema.Table("KhuyenMai")]
    public partial class KhuyenMai
    {
        public KhuyenMai()
        {
            this.SanPhamKhuyenMais = new List<SanPhamKhuyenMai>();
        }
        [Key]
        [Display(Name = "Mã khuyến mãi ")]
        [StringLength(1024)] public string MaKM { get; set; }
        [Display(Name = "Ngày bắt đầu")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> NgayBatDau { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Ngày kết thúc")]
        public Nullable<System.DateTime> NgayKetThuc { get; set; }
        [Display(Name = "Nội dung")]
        [StringLength(1024)] public string NoiDung { get; set; }
        [Display(Name = "Tên chương trình")]
        [StringLength(1024)] public string TenCT { get; set; }
        [Display(Name = "Ảnh chương trình")]
        [StringLength(1024)] public string AnhCT { get; set; }

        public virtual ICollection<SanPhamKhuyenMai> SanPhamKhuyenMais { get; set; }
    }
}