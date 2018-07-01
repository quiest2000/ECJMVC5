﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleEFCodeFirst.Repositories
{
    [System.ComponentModel.DataAnnotations.Schema.Table("SanPham")]
    public partial class SanPham
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.BinhLuans = new List<BinhLuan>();
            this.ChiTietDonHangs = new List<ChiTietDonHang>();
            this.HopDongNCCs = new List<HopDongNCC>();
            this.SanPhamKhuyenMais = new List<SanPhamKhuyenMai>();
            this.ThongSoKyThuats = new List<ThongSoKyThuat>();
            this.Sanphamcanmuas = new List<Sanphamcanmua>();
        }
        [System.ComponentModel.DataAnnotations.Key]
        [StringLength(1024)] public string MaSP { get; set; }
        [StringLength(1024)] public string TenSP { get; set; }
        [StringLength(1024)] public string LoaiSP { get; set; }
        [ForeignKey(nameof(LoaiSP))]
        public virtual LoaiSP LoaiSanPham { get; set; }
        public Nullable<int> SoLuotXemSP { get; set; }
        [StringLength(1024)] public string HangSX { get; set; }
        [StringLength(1024)] public string XuatXu { get; set; }
        public Nullable<decimal> GiaTien { get; set; }
        [StringLength(1024)] public string MoTa { get; set; }
        [StringLength(1024)] public string AnhDaiDien { get; set; }
        [StringLength(1024)] public string AnhNen { get; set; }
        [StringLength(1024)] public string AnhKhac { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<bool> isnew { get; set; }
        public Nullable<bool> ishot { get; set; }
        public Nullable<decimal> GiaGoc { get; set; }

        public virtual ICollection<BinhLuan> BinhLuans { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        [ForeignKey(nameof(HangSX))]
        public virtual HangSanXuat HangSanXuat { get; set; }
        public virtual ICollection<HopDongNCC> HopDongNCCs { get; set; }
        public virtual ICollection<SanPhamKhuyenMai> SanPhamKhuyenMais { get; set; }
        public virtual ICollection<ThongSoKyThuat> ThongSoKyThuats { get; set; }
        public virtual ICollection<Sanphamcanmua> Sanphamcanmuas { get; set; }
    }
}
