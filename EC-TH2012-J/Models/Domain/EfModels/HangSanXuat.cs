﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EC_TH2012_J.Models.Domain.EfModels
{
    [System.ComponentModel.DataAnnotations.Schema.Table("HangSanXuat")]
    public partial class HangSanXuat
    {
        public HangSanXuat()
        {
            this.SanPhams = new HashSet<SanPham>();
        }
        [System.ComponentModel.DataAnnotations.Key]
        [Display(Name = "Hãng sản xuất")]
        public string HangSX { get; set; }
        [Display(Name = "Tên Hãng")]
        public string TenHang { get; set; }
        [Display(Name = "Trụ sở chính")]
        public string TruSoChinh { get; set; }
        [Display(Name = "Quốc gia")]
        public string QuocGia { get; set; }
    
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}