using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ECommerce.Models.Domain;
using ECommerce.Models.Domain.EfModels;
using ECommerce.Utils;

namespace ECommerce.Models
{
    public class KhuyenMaiModel
    {
        private MainContext db = new MainContext();

        internal KhuyenMai FindById(int id)
        {
            return db.KhuyenMais.Find(id);
        }

        internal void EditKhuyenMai(KhuyenMai loai)
        {
            var lsp = db.KhuyenMais.Find(loai.Id);
            lsp.TenCT = loai.TenCT;
            lsp.NgayBatDau = loai.NgayBatDau;
            lsp.NgayKetThuc = loai.NgayKetThuc;
            lsp.NoiDung = loai.NoiDung;
            db.Entry(lsp).State = EntityState.Modified;
            db.SaveChanges();
        }

        internal void DeleteKhuyenMai(int khuyenMaiId)
        {
            var lst = db.SanPhamKhuyenMais.Where(m => m.KhuyenMaiId==(khuyenMaiId)).ToList();
            foreach (var item in lst)
            {
                DeleteSPKhuyenMai(item.KhuyenMaiId, item.SanPhamId);
            }
            var loai = db.KhuyenMais.Find(khuyenMaiId);
            db.KhuyenMais.Remove(loai);
            db.SaveChanges();
        }

        internal int ThemKhuyenMai(KhuyenMai loai)
        {
            loai.AnhCT = loai.Id + "1.jpg";
            db.KhuyenMais.Add(loai);
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
        //    var temp = db.KhuyenMais.Find(maID);
        //    if (temp == null)
        //        return true;
        //    return false;
        //}

        internal IList<KhuyenMai> TimKhuyenMai(string key, DateTime? start, DateTime? end)
        {
            IQueryable<KhuyenMai> lst = db.KhuyenMais;
            if (!string.IsNullOrEmpty(key))
                lst = db.KhuyenMais.Where(u => u.TenCT.Contains(key));
            if (start != null)
                lst = db.KhuyenMais.Where(u => u.NgayBatDau >= start);
            if (end != null)
                lst = db.KhuyenMais.Where(u => u.NgayKetThuc <= end);
            return lst.ToList();
        }

        internal bool KiemTraTen(string key)
        {
            var temp = db.KhuyenMais.Where(m => m.TenCT==(key)).ToList();
            if (temp.Count == 0)
                return true;
            return false;
        }

        internal IQueryable<SanPhamKhuyenMai> CTKhuyenMai(string key)
        {
            var id = key.ToInt();
            return db.SanPhamKhuyenMais.Where(m => m.KhuyenMaiId==(id));
        }



        internal void ThemSPKhuyenMai(SanPhamKhuyenMai item)
        {
            db.SanPhamKhuyenMais.Add(item);
            db.SaveChanges();
            var s = new SanPhamModel();
            s.UpdateGiaBan(item.SanPhamId);
        }


        internal void DelAllSPKM(int maKm)
        {
            db.SanPhamKhuyenMais.RemoveRange(db.SanPhamKhuyenMais.Where(m => m.KhuyenMaiId == maKm));
            db.SaveChanges();
        }

        internal IQueryable<SanPhamKhuyenMai> GetSPKM(int maKm)
        {
            return db.SanPhamKhuyenMais.Where(m => m.KhuyenMaiId == maKm);
        }

        internal IQueryable<SanPham> DSSP(string key, int maloai, int makm)
        {
            var lst = db.SanPhamKhuyenMais.Where(m => m.KhuyenMaiId == makm).Select(m => m.SanPhamId);
            var lst1 = db.SanPhams.Where(m => !lst.Contains(m.Id));
            if (!string.IsNullOrEmpty(key))
                lst1 = lst1.Where(m => m.TenSP.Contains(key));
            if (maloai > 0)
                lst1 = lst1.Where(m => m.LoaiSpId==(maloai));
            return lst1;
        }

        internal IQueryable<SanPham> DSSanPhamKhuyenMai(string key, int maloai, int makm)
        {
            var lst = db.SanPhamKhuyenMais.Where(m => m.KhuyenMaiId == makm).Select(m => m.SanPham);
            if (!string.IsNullOrEmpty(key))
                lst = lst.Where(m => m.TenSP.Contains(key));
            if (maloai > 0)
                lst = lst.Where(m => m.LoaiSpId==(maloai));
            return lst;
        }

        internal void DeleteSPKhuyenMai(int makm, int masp)
        {
            var sp = db.SanPhamKhuyenMais.FirstOrDefault(m => m.SanPhamId == masp && m.KhuyenMaiId == makm);
            db.SanPhamKhuyenMais.Remove(sp);
            db.SaveChanges();
            var s = new SanPhamModel();
            s.UpdateGiaBan(masp);

        }
    }
}