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
    [System.ComponentModel.DataAnnotations.Schema.Table("ThongSoKyThuat")]
    public partial class ThongSoKyThuat
    {
        [System.ComponentModel.DataAnnotations.Key, Column(Order = 0)]
        [System.ComponentModel.DataAnnotations.StringLength(1024)] public string MaSP { get; set; }
        [System.ComponentModel.DataAnnotations.Key, Column(Order = 1)]
        [System.ComponentModel.DataAnnotations.StringLength(1024)] public string ThuocTinh { get; set; }
        [System.ComponentModel.DataAnnotations.Key, Column(Order = 2)]
        [System.ComponentModel.DataAnnotations.StringLength(1024)] public string GiaTri { get; set; }
        [ForeignKey(nameof(MaSP))]
        public virtual SanPham SanPham { get; set; }
    }
}
