using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ECommerce.Models.Domain;
using ECommerce.Models.Domain.EfModels;

namespace ECommerce.Models.B2B
{
    public class SanPhamCanMuaModel
    {
        MainContext db = new MainContext();
        public List<SanPhamCanMua> getDS(int index,int count)
        {
            using(var db = new MainContext())
            {
                var ds =  db.Sanphamcanmuas.OrderBy(m=>m.Ngaydang).Skip(index).Take(count).ToList();
                return ds;
            }
        }
        public SanPham getSP(int maSP)
        {
            using (var db = new MainContext())
            {
                return (from p in db.SanPhams where p.Id == maSP select p).FirstOrDefault();
            }
        }
        public SanPhamCanMua getSanphamcanmua(int ID)
        {
            using(var db = new MainContext())
            {
                var var = (from p in db.Sanphamcanmuas where p.Id == ID select p).FirstOrDefault();
                return var;
            }
        }
        internal IQueryable<SanPhamCanMua> TimSPCM(string key)
        {
            IQueryable<SanPhamCanMua> lst = db.Sanphamcanmuas;
            if (!string.IsNullOrEmpty(key))
                lst = lst.Where(m => m.SanPham.TenSP.Contains(key));
            return lst;
        }

        internal void DeleteSPCM(int id)
        {
            var loai = db.Sanphamcanmuas.Find(id);
            db.Sanphamcanmuas.Remove(loai);
            db.SaveChanges();
        }

        internal string ThemSPCM(SanPhamCanMua loai)
        {
            loai.Ngaydang = DateTime.Today;
            db.Sanphamcanmuas.Add(loai);
            db.SaveChanges();
            return null;
        }

        internal void EditSPCM(SanPhamCanMuaEdit loai)
        {
            var lsp = db.Sanphamcanmuas.Find(loai.ID);
            lsp.Soluong = loai.Soluong;
            lsp.Ngayketthuc = loai.Ngayketthuc;
            lsp.Mota = loai.Mota;
            db.Entry(lsp).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}