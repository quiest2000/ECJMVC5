//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;

namespace EC_TH2012_J.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;
    [System.ComponentModel.DataAnnotations.Schema.Table("SanPhamKhuyenMai")]
    public partial class SanPhamKhuyenMai
    {
        [Key, Column(Order = 0)]
        public string MaKM { get; set; }
        [Key, Column(Order = 1)]
        public string MaSP { get; set; }

        [AllowHtml]
        public string MoTa { get; set; }
        public Nullable<int> GiamGia { get; set; }
        [ForeignKey(nameof(MaKM))]
        public virtual KhuyenMai KhuyenMai { get; set; }
        [ForeignKey(nameof(MaSP))]
        public virtual SanPham SanPham { get; set; }
    }
}