using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC_TH2012_J.Models.Domain;
using EC_TH2012_J.Models.Domain.EfModels;

namespace EC_TH2012_J.Models
{
    public class HopdongNCCModel
    {
        public List<NhaCungCap> getDsNhaCC()
        {
            using(MainContext db = new MainContext())
            {
                var temp = (from p in db.NhaCungCaps select p).ToList();
                return temp;
            }
        }
        public List<DropdownSanpham> getDsSanPham()
        {
            using (MainContext db = new MainContext())
            {
                var temp = (from p in db.SanPhams select new DropdownSanpham() { ID = p.MaSP, TenSP = p.TenSP }).ToList();
                return temp;
            }
        }
        public string ThemmoiHopDongNCC(Domain.EfModels.HopDongNCC a)
        {
            using(MainContext db = new MainContext())
            {
                try
                {
                    a.MaHD = TaoMa(db);
                    a.IsBuy = false;
                    db.HopDongNCCs.Add(a);
                    db.SaveChanges();
                    return a.MaHD;
                }
                catch(Exception e)
                {
                    return "";
                }
            }
        }
        private string TaoMa(MainContext db)
        {
            string maID;
            Random rand = new Random();
            do
            {
                maID = "";
                for (int i = 0; i < 5; i++)
                {
                    maID += rand.Next(9);
                }
            }
            while (!KiemtraID(maID,db));
            return maID;
        }

        private bool KiemtraID(string maID,MainContext db)
        {
            var temp = db.HopDongNCCs.Find(maID);
            if (temp == null)
                return true;
            return false;
        }
        public List<MaHD> getMaHD(string IDdoitac)
        {
            using(MainContext db = new MainContext())
            {
                var ds = (from p in db.HopDongNCCs where p.MaNCC == IDdoitac select new MaHD() { Mahd = p.MaHD }).ToList();
                return ds;
            }
        }
        public string GetMaSP(string MaHD)
        {
            using(MainContext db = new MainContext())
            {
                var temp = (from p in db.HopDongNCCs where p.MaHD == MaHD select new { Masp = p.MaSP }).FirstOrDefault();
                return temp.Masp;
            }
        }
        public bool SetTTThanhtoan(string MaHD,bool value)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var temp = (from p in db.HopDongNCCs where p.MaHD == MaHD select p).FirstOrDefault();
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
        public bool SetTTGiaohang(string MaHD, bool value)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var temp = (from p in db.HopDongNCCs where p.MaHD == MaHD select p).FirstOrDefault();
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
        public void SetXacnhangiaohang(string MaHD)
        {
            SetTTThanhtoan(MaHD, false);
            SetTTGiaohang(MaHD, false);
        }

    }
    public class DropdownSanpham
    {
        public string  ID { get; set; }
        public string TenSP { get; set; }
    }
    public class MaHD
    {
        public string Mahd { get; set; }
    }
}