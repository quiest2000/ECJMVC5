using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ECommerce.Models.Domain;
using ECommerce.Models.Domain.EfModels;

namespace ECommerce.Models
{
    public class SanPhamModel
    {
        MainContext db = new MainContext();
        public IQueryable<SanPham> SearchByName(string term)
        {
            IQueryable<SanPham> lst;
            lst = db.SanPhams.Where(u => u.TenSP.Contains(term));
            return lst;
        }

        public IQueryable<SanPham> AdvancedSearch(string term, string loai, string hangsx, int? minprice, int? maxprice)
        {
            IQueryable<SanPham> lst = db.SanPhams;
            if (!string.IsNullOrEmpty(term))
                lst = SearchByName(term);
            if (!string.IsNullOrEmpty(loai))
                lst = from p in lst where p.LoaiSpId.Equals(loai) select p;
            if (!string.IsNullOrEmpty(hangsx))
                lst = from p in lst where p.HangSxId.Equals(hangsx) select p;
            if (minprice != null)
                lst = from p in lst where p.GiaTien >= minprice select p;
            if (maxprice != null)
                lst = from p in lst where p.GiaTien <= maxprice select p;
            return lst;
        }
        public IQueryable<SanPham> SearchByType(string term)
        {
            var splist = (from p in db.SanPhams where p.LoaiSpId.Equals(term) select p);
            return splist;
        }



        internal IQueryable<SanPham> SPMoiNhap()
        {
            var splist = db.SanPhams.Where(s => s.isnew == true);
            return splist;
        }

        internal IQueryable<SanPham> SPKhuyenMai()
        {
            var splist = from p in db.SanPhamKhuyenMais
                         orderby p.GiamGia descending
                         where DateTime.Today >= p.KhuyenMai.NgayBatDau && DateTime.Today <= p.KhuyenMai.NgayKetThuc
                         select p.SanPham;
            return splist;
        }

        internal IQueryable<SanPham> SPBanChay(int takenum)
        {
            var s = from chiTietDonHang in db.ChiTietDonHangs
                    where chiTietDonHang.DonHangKH.TinhTrangDH == 3
                    group chiTietDonHang by chiTietDonHang.SanPhamId into groupMaSP
                    select new { MaSP = groupMaSP.Key, sl = groupMaSP.Sum(r => r.SoLuong) };
            var splist = from p in db.SanPhams join ca in s on p.Id equals ca.MaSP orderby ca.sl descending select p;
            return splist.Take(takenum);
        }

        internal IQueryable<SanPham> GetAll()
        {
            return db.SanPhams;
        }

        internal SanPham FindById(int id)
        {
            return db.SanPhams.Find(id);
        }

        internal IQueryable<HangSanXuat> GetAllHangSX()
        {
            return db.HangSanXuats;
        }

        internal IQueryable<LoaiSanPham> GetAllLoaiSP()
        {
            return db.LoaiSPs;
        }

        internal void EditSP(SanPham sanpham)
        {
            //Id,TenSP,LoaiSpId,Id,XuatXu,GiaTien,MoTa,SoLuong,isnew,ishot
            var sp = db.SanPhams.Find(sanpham.Id);
            sp.TenSP = sanpham.TenSP;
            sp.LoaiSpId = sanpham.LoaiSpId;
            sp.HangSxId = sanpham.HangSxId;
            sp.XuatXu = sanpham.XuatXu;
            sp.GiaGoc = sanpham.GiaGoc;
            sp.GiaTien = tinhgiatien(sp.Id, sp.GiaGoc);
            sp.MoTa = sanpham.MoTa;
            sp.SoLuong = sanpham.SoLuong;
            sp.isnew = sanpham.isnew;
            sp.ishot = sanpham.ishot;
            db.Entry(sp).State = EntityState.Modified;
            db.SaveChanges();
        }

        private decimal? tinhgiatien(int masp, decimal? giagoc)
        {
            IQueryable<SanPhamKhuyenMai> s = db.SanPhamKhuyenMais.Where(m => m.SanPhamId.Equals(masp)).OrderByDescending(m => m.GiamGia);
            if (s.Any())
            {
                return (giagoc * (100 - s.First().GiamGia) / 100);
            }
            return giagoc;
        }

        internal void DeleteSP(int id)
        {
            var sanpham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanpham);
            db.SaveChanges();
        }
        /// <summary>
        /// return SP id
        /// </summary>
        /// <param name="sanpham"></param>
        /// <returns></returns>
        internal int ThemSP(SanPham sanpham)
        {
            sanpham.GiaTien = sanpham.GiaGoc;
            sanpham.AnhDaiDien = sanpham.Id + "1.jpg";
            sanpham.AnhNen = sanpham.Id + "2.jpg";
            sanpham.AnhKhac = sanpham.Id + "3.jpg";
            db.SanPhams.Add(sanpham);
            db.SaveChanges();
            return sanpham.Id;
        }

        //private string TaoMa()
        //{
        //    string maID;
        //    var rand = new Random();
        //    do
        //    {
        //        maID = "";
        //        for (var i = 0; i < 5; i++)
        //        {
        //            maID += rand.Next(9);
        //        }
        //    }
        //    while (!KiemtraID(maID));
        //    return maID;
        //}

        //private bool KiemtraID(string maID)
        //{
        //    using (var db = new MainContext())
        //    {
        //        var temp = db.SanPhams.Find(maID);
        //        if (temp == null)
        //            return true;
        //        return false;
        //    }
        //}
        public SanPham getSanPham(int id)
        {
            var sp = (from p in db.SanPhams where (p.Id == id) select p).FirstOrDefault();
            return sp;
        }

        internal void ThemTSKT(ThongSoKyThuat item)
        {
            db.ThongSoKyThuats.Add(item);
            db.SaveChanges();
        }

        internal IQueryable<ThongSoKyThuat> GetTSKT(int masp)
        {
            return db.ThongSoKyThuats.Where(m => m.SanPhamId == masp);
        }

        internal void DelAllTSKT(int sanPhamId)
        {
            db.ThongSoKyThuats.RemoveRange(db.ThongSoKyThuats.Where(m => m.SanPhamId == sanPhamId));
            db.SaveChanges();
        }

        internal IQueryable<SanPham> SPHot()
        {
            return db.SanPhams.Where(s => s.ishot == true);
        }

        internal void UpdateGiaBan(int sanPhamId)
        {
            var s = db.SanPhams.Find(sanPhamId);
            s.GiaTien = tinhgiatien(sanPhamId, s.GiaGoc);
            db.Entry(s).State = EntityState.Modified;
            db.SaveChanges();
        }

        internal void UpdateGiaBans(List<SanPham> lst)
        {
            using (var db = new MainContext())
            {
                lst.ForEach(m => m.GiaTien = tinhgiatien(m.Id, m.GiaGoc));
                db.SaveChanges();
            }
        }

        internal void UpdateSL(int masp, int? sl, bool? loaihd)
        {
            if (sl != null)
            {
                var s = db.SanPhams.Find(masp);
                if (loaihd == true)
                    s.SoLuong += sl;
                else if (loaihd == false)
                    s.SoLuong -= sl;
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}