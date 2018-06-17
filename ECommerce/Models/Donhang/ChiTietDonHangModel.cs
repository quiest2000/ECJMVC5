using System.Collections.Generic;
using System.Linq;
using ECommerce.Models.Domain;
using ECommerce.Models.Domain.EfModels;

namespace ECommerce.Models.Donhang
{
    public class ChiTietDonHangModel
    {
        public int MaDH { get; set; }
        public int? SoLuong { get; set; }
        public decimal? ThanhTien { get; set; }
        public SanPham SanPham { get; set; }

        public List<ChiTietDonHangModel>getChiTietDonHang(int maDH)
        {
            using(var db = new MainContext())
            {
                var danhSachChiTiet = new List<ChiTietDonHangModel>();
                var ds = (from p in db.ChiTietDonHangs where p.DonHangId == maDH select p).ToList();
                foreach(var temp in ds)
                {
                    var tam = new ChiTietDonHangModel()
                    {
                        MaDH = temp.DonHangId,
                        SoLuong = temp.SoLuong,
                        ThanhTien = temp.ThanhTien,
                        SanPham = temp.SanPham
                    };
                    var tt = temp.SanPham.GiaTien.ToString();
                    danhSachChiTiet.Add(tam);
                }
                return danhSachChiTiet;
            }
        }
    }
}