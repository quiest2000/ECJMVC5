//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace EC_TH2012_J.Models.Domain.EfModels
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Sanphamcanmua")]
    public partial class Sanphamcanmua
    {
        public Sanphamcanmua()
        {
            this.DanhsachdangkisanphamNCCs = new HashSet<DanhsachdangkisanphamNCC>();
        }
        [System.ComponentModel.DataAnnotations.Key]
        public int ID { get; set; }
        public string MaSP { get; set; }
        public Nullable<int> Soluong { get; set; }
        public Nullable<System.DateTime> Ngayketthuc { get; set; }
        public Nullable<System.DateTime> Ngaydang { get; set; }

        [AllowHtml]
        public string Mota { get; set; }
        [ForeignKey(nameof(MaSP))]
        public virtual SanPham SanPham { get; set; }
        public virtual ICollection<DanhsachdangkisanphamNCC> DanhsachdangkisanphamNCCs { get; set; }
    }
}