using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Models.Domain;
using ECommerce.Models.Domain.EfModels;

namespace ECommerce.Models.B2B
{
    public class HopdongNCCModel
    {
        public List<NhaCungCap> getDsNhaCC()
        {
            using (var db = new MainContext())
            {
                var temp = (from p in db.NhaCungCaps select p).ToList();
                return temp;
            }
        }
        public List<DropdownSanpham> getDsSanPham()
        {
            using (var db = new MainContext())
            {
                var temp = (from p in db.SanPhams select new DropdownSanpham() { ID = p.Id.ToString(), TenSP = p.TenSP }).ToList();
                return temp;
            }
        }
        public int ThemmoiHopDongNCC(Domain.EfModels.HopDongNcc a)
        {
            using (var db = new MainContext())
            {
                try
                {
                    //a.Id = TaoMa(db);
                    a.IsBuy = false;
                    db.HopDongNccs.Add(a);
                    db.SaveChanges();
                    return a.Id;
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
        }
        //private string TaoMa(MainContext db)
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
        //    while (!KiemtraID(maID,db));
        //    return maID;
        //}

        //private bool KiemtraID(string maID, MainContext db)
        //{
        //    var temp = db.HopDongNccs.Find(maID);
        //    if (temp == null)
        //        return true;
        //    return false;
        //}
        public List<MaHD> getMaHD(int IDdoitac)
        {
            using (var db = new MainContext())
            {
                var ds = (from p in db.HopDongNccs where p.NccId == IDdoitac select new MaHD() { Mahd = p.Id }).ToList();
                return ds;
            }
        }
        public int GetMaSP(int MaHD)
        {
            using (var db = new MainContext())
            {
                var temp = (from p in db.HopDongNccs where p.Id == MaHD select new { SanPhamId = p.SanPhamId }).FirstOrDefault();
                return temp?.SanPhamId ?? 0;
            }
        }
        public bool SetTTThanhtoan(int MaHD, bool value)
        {
            using (var db = new MainContext())
            {
                try
                {
                    var temp = (from p in db.HopDongNccs where p.Id == MaHD select p).FirstOrDefault();
                    temp.TTThanhToan = value;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
        public bool SetTTGiaohang(int MaHD, bool value)
        {
            using (var db = new MainContext())
            {
                try
                {
                    var temp = (from p in db.HopDongNccs where p.Id == MaHD select p).FirstOrDefault();
                    temp.TinhTrang = value;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
        public void SetXacnhangiaohang(int MaHD)
        {
            SetTTThanhtoan(MaHD, false);
            SetTTGiaohang(MaHD, false);
        }

    }
    public class DropdownSanpham
    {
        public string ID { get; set; }
        public string TenSP { get; set; }
    }
    public class MaHD
    {
        public int Mahd { get; set; }
    }
}