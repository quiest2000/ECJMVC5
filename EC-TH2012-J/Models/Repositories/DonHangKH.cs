﻿//------------------------------------------------------------------------------
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
    using System.ComponentModel.DataAnnotations;
    [System.ComponentModel.DataAnnotations.Schema.Table("DonHangKH")]
    public partial class DonHangKH
    {
        public DonHangKH()
        {
            this.ChiTietDonHangs = new List<ChiTietDonHang>();
        }
        [Key]
        [Display(Name = "Mã đơn hàng")]
        [System.ComponentModel.DataAnnotations.StringLength(1024)] public string MaDH { get; set; }
        [Display(Name = "Mã khách hàng")]
        [System.ComponentModel.DataAnnotations.StringLength(1024)] public string MaKH { get; set; }
        [Display(Name = "Phí vận chuyển ")]
        public Nullable<decimal> PhiVanChuyen { get; set; }
        [Display(Name = "Phương thức giao dịch")]
        [System.ComponentModel.DataAnnotations.StringLength(1024)] public string PTGiaoDich { get; set; }
        [Display(Name = "Ngày đặt mua")]
        public Nullable<System.DateTime> NgayDatMua { get; set; }
        [Display(Name = "Tình trạng đơn hàng")]
        public Nullable<int> TinhTrangDH { get; set; }
        [Display(Name = "Tổng tiền")]
        public Nullable<double> Tongtien { get; set; }
        [Display(Name = "Ghi Chú")]
        [System.ComponentModel.DataAnnotations.StringLength(1024)] public string Ghichu { get; set; }
        [Display(Name = "Địa Chỉ")]
        [System.ComponentModel.DataAnnotations.StringLength(1024)] public string Diachi { get; set; }
        [Display(Name = "Điện thoại")]
        [System.ComponentModel.DataAnnotations.StringLength(1024)] public string Dienthoai { get; set; }
        [ForeignKey(nameof(MaKH))]
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
