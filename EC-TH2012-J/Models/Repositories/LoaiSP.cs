//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EC_TH2012_J.Models
{
    using System;
    using System.Collections.Generic;
    [System.ComponentModel.DataAnnotations.Schema.Table("LoaiSP")]
    public partial class LoaiSP
    {
        public LoaiSP()
        {
            this.SanPhams = new List<SanPham>();
        }
        [System.ComponentModel.DataAnnotations.Key]
        [System.ComponentModel.DataAnnotations.StringLength(1024)] public string MaLoai { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(1024)] public string TenLoai { get; set; }
    
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
