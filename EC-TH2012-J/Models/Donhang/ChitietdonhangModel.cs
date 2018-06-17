using System;
using System.Collections.Generic;
using System.Linq;
using EC_TH2012_J.Models.Domain;
using EC_TH2012_J.Models.Domain.EfModels;

namespace EC_TH2012_J.Models.Donhang
{
    public class ChiTietDonHangModel
    {
        public string MaDH { get; set; }
        public int? SoLuong { get; set; }
        public decimal? ThanhTien { get; set; }
        public SanPham SanPham { get; set; }

        public List<ChiTietDonHangModel>getChiTietDonHang(string maDH)
        {
            using(var db = new MainContext())
            {
                var danhSachChiTiet = new List<ChiTietDonHangModel>();
                var ds = (from p in db.ChiTietDonHangs where p.MaDH == maDH select p).ToList();
                foreach(var temp in ds)
                {
                    
                    var tam = new ChiTietDonHangModel()
                    {
                        MaDH = temp.MaDH,
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