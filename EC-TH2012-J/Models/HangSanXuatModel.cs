using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EC_TH2012_J.Models.Domain;
using EC_TH2012_J.Models.Domain.EfModels;

namespace EC_TH2012_J.Models
{
    public class HangSanXuatModel
    {
        private MainContext db = new MainContext();
        public IQueryable<HangSanXuat> GetHangSX()
        {
            IQueryable<HangSanXuat> lst = db.HangSanXuats;
            return lst;
        }

        internal HangSanXuat FindById(string id)
        {
            return db.HangSanXuats.Find(id);
        }

        internal void EditHangSX(HangSanXuat loai)
        {
            var lsp = db.HangSanXuats.Find(loai.HangSX);
            lsp.TenHang = loai.TenHang;
            lsp.TruSoChinh = loai.TruSoChinh;
            lsp.QuocGia = loai.QuocGia;
            db.Entry(lsp).State = EntityState.Modified;
            db.SaveChanges();
        }

        internal void DeleteHangSX(string id)
        {
            var loai = db.HangSanXuats.Find(id);
            db.HangSanXuats.Remove(loai);
            db.SaveChanges();
        }

        internal bool KiemTraTen(string p)
        {
            var temp = db.HangSanXuats.Where(m => m.TenHang.Equals(p)).ToList();
            if (temp.Count == 0)
                return true;
            return false;
        }

        internal string ThemHangSX(HangSanXuat loai)
        {
            loai.HangSX = TaoMa();
            db.HangSanXuats.Add(loai);
            db.SaveChanges();
            return loai.HangSX;
        }

        private string TaoMa()
        {
            string maID;
            var rand = new Random();
            do
            {
                maID = "";
                for (var i = 0; i < 5; i++)
                {
                    maID += rand.Next(9);
                }
            }
            while (!KiemtraID(maID));
            return maID;
        }

        private bool KiemtraID(string maID)
        {
            var temp = db.HangSanXuats.Find(maID);
            if (temp == null)
                return true;
            return false;
        }

        internal IQueryable<HangSanXuat> SearchByName(string key)
        {
            if (string.IsNullOrEmpty(key))
                return db.HangSanXuats;
            return db.HangSanXuats.Where(u => u.TenHang.Contains(key));
        }
    }
}