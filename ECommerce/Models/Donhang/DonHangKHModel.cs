using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using ECommerce.Models.Domain;
using ECommerce.Models.Domain.EfModels;

namespace ECommerce.Models.Donhang
{
    public class DonHangKHModel
    {
        public Domain.EfModels.DonHangKH donHang;
        public AspNetUser nguoiMua;
        public String nguoiNhan;
        public String tinhTrangDH;
        public List<DonHangKHModel> Xemdonhang(int makh)
        {
            using (var db = new MainContext())
            {
                var listDh = new List<DonHangKHModel>();
                db.DonHangKHs.AsNoTracking();
                var strMaKh = makh.ToString();
                var danhsach = from p in db.DonHangKHs where p.KhachHangId == strMaKh select p;
                foreach (var temp in danhsach.ToList())
                {
                    var users = (from p in db.AspNetUsers where p.Id == strMaKh select p).FirstOrDefault();

                    listDh.Add(new DonHangKHModel()
                    {
                        donHang = temp,
                        nguoiMua = users,
                        tinhTrangDH = gettinhTrangDH(temp.TinhTrangDH)
                    });
                }
                return listDh;
            }
        }

        private string gettinhTrangDH(int? nullable)
        {
            switch (nullable)
            {
                case 0:
                    {
                        return "Chưa giao";
                    }
                case 1:
                    {
                        return "Đang duyệt";
                    }
                case 2:
                    {
                        return "Đang giao hàng";
                    }
                case 3:
                    {
                        return "Đã giao";
                    }
                case 4:
                    {
                        return "Đã hủy";
                    }
            }
            return "Đang duyệt";
        }
        public bool HuyDH(string maDH)
        {
            try
            {
                using (var db = new MainContext())
                {
                    var query = "update DonHangKH set TinhTrangDH = '4' where Id ='" + maDH + "'";
                    db.Database.ExecuteSqlCommand(query);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public AspNetUser Xemttnguoidung(int userId)
        {
            using (var db = new MainContext())
            {
                var strUserId = userId.ToString();
                var users = (from p in db.AspNetUsers where p.Id == strUserId select p).FirstOrDefault();
                return users;
            }
        }
        public void Luudonhang(Donhangtongquan a, int maKH, Giohang giohang)
        {
            try
            {
                using (var db = new MainContext())
                {
                    var dhkh = new Domain.EfModels.DonHangKH();
                    dhkh.KhachHangId = maKH.ToString();

                    dhkh.Diachi = a.address;
                    dhkh.Dienthoai = a.phoneNumber;
                    dhkh.Ghichu = a.Note;
                    dhkh.NgayDatMua = DateTime.Now;
                    dhkh.TinhTrangDH = 1;
                    dhkh.Tongtien = giohang.TinhtongtienCart();
                    dhkh.PhiVanChuyen = 0;

                    dhkh = db.DonHangKHs.Add(dhkh);
                    db.SaveChanges();

                    Luuchitietdonhang(giohang, db, dhkh.Id);
                }
            }
            catch (Exception e) { }
        }

        private void Luuchitietdonhang(Giohang giohang, MainContext db, int maDH)
        {
            foreach (var temp in giohang.getGiohang())
            {
                var chiTiet = new ChiTietDonHang()
                {
                    DonHangId = maDH,
                    SanPhamId = temp.sanPham.Id,
                    SoLuong = temp.Soluong,
                    ThanhTien = (decimal)temp.Thanhtien
                };
                db.ChiTietDonHangs.Add(chiTiet);

            }
            db.SaveChanges();
        }
        //public string RandomMa()
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
        //        var temp = db.DonHangKHs.Find(maID);
        //        if (temp == null)
        //            return true;
        //        return false;
        //    }
        //}

        internal IQueryable<Domain.EfModels.DonHangKH> TimDonHang(int maDonHang, string mobile, DateTime? date, int? status)
        {
            var db = new MainContext();

            IQueryable<Domain.EfModels.DonHangKH> lst = db.DonHangKHs;
            if (maDonHang > 0)
                lst = lst.Where(m => m.Id == maDonHang);
            if (!string.IsNullOrEmpty(mobile))
                lst = lst.Where(m => m.Dienthoai.Contains(mobile));
            if (status != null)
                lst = lst.Where(m => m.TinhTrangDH == status);
            if (date != null)
                lst = lst.Where(m => m.NgayDatMua.Value.Year == date.Value.Year && m.NgayDatMua.Value.Month == date.Value.Month && m.NgayDatMua.Value.Day == date.Value.Day);
            return lst;

        }

        internal bool UpdateTinhTrang(int madh, int? tt)
        {
            if (tt == null) return false;
            try
            {
                var db = new MainContext();
                var dh = db.DonHangKHs.Find(madh);
                if (dh.TinhTrangDH == 4 || dh.TinhTrangDH == 3)
                    return false;
                if (dh.TinhTrangDH == 1)
                    if (tt == 2 || tt == 3)
                    {
                        foreach (var item in dh.ChiTietDonHangs)
                        {
                            var spm = new SanPhamModel();
                            spm.UpdateSL(item.SanPhamId, item.SoLuong, false);
                        }
                    }
                if (dh.TinhTrangDH == 2)
                {
                    if (tt == 4)
                    {
                        foreach (var item in dh.ChiTietDonHangs)
                        {
                            var spm = new SanPhamModel();
                            spm.UpdateSL(item.SanPhamId, item.SoLuong, true);
                        }
                    }
                    if (tt == 1) return false;
                }
                var query = "update DonHangKH set TinhTrangDH = " + tt + " where Id ='" + madh + "'";
                db.Database.ExecuteSqlCommand(query);
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        internal IQueryable<ChiTietDonHang> ChiTietDonHang(int maDh)
        {
            var db = new MainContext();
            return db.ChiTietDonHangs.Where(m => m.DonHangId == maDh);
        }

        internal IQueryable<object> ThongKeDoanhThu(DateTime? froms, DateTime? tos)
        {
            var db = new MainContext();
            var s = from p in db.DonHangKHs
                    where p.TinhTrangDH == 3 && p.NgayDatMua >= froms && p.NgayDatMua <= tos
                    group p by EntityFunctions.TruncateTime(p.NgayDatMua) into gro
                    select new { ngaymua = gro.Key.Value, tongtien = gro.Sum(r => r.Tongtien) };
            return s;
        }


        internal IQueryable<object> ThongKeTiTrong(DateTime? froms, DateTime? tos)
        {
            var db = new MainContext();
            var s = from p in db.ChiTietDonHangs
                    where p.DonHangKH.TinhTrangDH == 3 && p.DonHangKH.NgayDatMua >= froms && p.DonHangKH.NgayDatMua <= tos
                    group p by p.SanPham.TenSP into gro
                    select new { TenSP = gro.Key, SL = gro.Sum(r => r.SoLuong) };
            return s;
        }
    }
}