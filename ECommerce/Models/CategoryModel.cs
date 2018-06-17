using System;
using System.Data.Entity;
using System.Linq;
using ECommerce.Models.Domain;
using ECommerce.Models.Domain.EfModels;

namespace ECommerce.Models
{
    public class CategoryModel
    {
        private MainContext db;
        public CategoryModel()
        {
            db = new MainContext();
        }
        public IQueryable<LoaiSanPham> GetCategory()
        {
            IQueryable<LoaiSanPham> lst = db.LoaiSPs;
            return lst;
        }

        internal LoaiSanPham FindById(int id)
        {
            return db.LoaiSPs.Find(id);
        }

        internal void EditLoaiSP(LoaiSanPham loai)
        {
            var lsp = db.LoaiSPs.Find(loai.Id);
            lsp.TenLoai = loai.TenLoai;
            db.Entry(lsp).State = EntityState.Modified;
            db.SaveChanges();
        }

        internal void DeleteLoaiSP(int id)
        {
            var loai = db.LoaiSPs.Find(id);
            db.LoaiSPs.Remove(loai);
            db.SaveChanges();
        }

        internal int ThemLoaiSP(LoaiSanPham loai)
        {
            //loai.Id = TaoMa();
            db.LoaiSPs.Add(loai);
            db.SaveChanges();
            return loai.Id;
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
        //    var temp = db.LoaiSPs.Find(maID);
        //    if (temp == null)
        //        return true;
        //    return false;
        //}

        internal IQueryable<LoaiSanPham> SearchByName(string key)
        {
            if (string.IsNullOrEmpty(key))
                return db.LoaiSPs;
            return db.LoaiSPs.Where(u => u.TenLoai.Contains(key));
        }


        internal bool KiemTraTen(string p)
        {
            var temp = db.LoaiSPs.Where(m=>m.TenLoai.Equals(p)).ToList();
            if (temp.Count == 0)
                return true;
            return false;
        }
    }
}