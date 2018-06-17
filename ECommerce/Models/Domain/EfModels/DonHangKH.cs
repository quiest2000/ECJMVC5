using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Domain.EfModels
{
    public class DonHangKH
    {
        [Display(Name = "Mã đơn hàng")]
        public int Id { get; set; }
        [Display(Name = "Mã khách hàng")]
        public int KhachHangId { get; set; }
        [Display(Name = "Phí vận chuyển ")]
        public decimal? PhiVanChuyen { get; set; }
        [Display(Name = "Phương thức giao dịch")]
        public string PTGiaoDich { get; set; }
        [Display(Name = "Ngày đặt mua")]
        public DateTime? NgayDatMua { get; set; }
        [Display(Name = "Tình trạng đơn hàng")]
        public int? TinhTrangDH { get; set; }
        [Display(Name = "Tổng tiền")]
        public double? Tongtien { get; set; }
        [Display(Name = "Ghi Chú")]
        public string Ghichu { get; set; }
        [Display(Name = "Địa Chỉ")]
        public string Diachi { get; set; }
        [Display(Name = "Điện thoại")]
        public string Dienthoai { get; set; }
        [ForeignKey(nameof(KhachHangId))]
        public virtual AspNetUser AspNetUser { get; set; }

        private ICollection<ChiTietDonHang> _chiTietDonHang;
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs
        {
            get => _chiTietDonHang ?? (_chiTietDonHang = new List<ChiTietDonHang>());
            set => _chiTietDonHang = value;
        }
    }
}
