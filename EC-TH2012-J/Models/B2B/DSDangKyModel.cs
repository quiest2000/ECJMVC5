﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC_TH2012_J.Models.Domain;

namespace EC_TH2012_J.Models.B2B
{
    public class DSDangKyModel
    {
        private MainContext db = new MainContext();


        internal IQueryable<Domain.EfModels.DanhsachdangkisanphamNCC> TimDS(string tensp, string tenncc, int? tt)
        {
            IQueryable<Domain.EfModels.DanhsachdangkisanphamNCC> lst = db.DanhsachdangkisanphamNCCs;
            if (!string.IsNullOrEmpty(tensp))
                lst = lst.Where(m => m.Sanphamcanmua.SanPham.TenSP.Contains(tensp));
            if (!string.IsNullOrEmpty(tenncc))
                lst = lst.Where(m => m.NhaCungCap.TenNCC.Contains(tenncc));
            if (tt != null)
                lst = lst.Where(m => m.Trangthai == tt);
            return lst;
        }

        internal object Findbyid(int id)
        {
            return db.DanhsachdangkisanphamNCCs.Find(id);
        }

        internal void DeleteDSDK(int id)
        {
            var loai = db.DanhsachdangkisanphamNCCs.Find(id);
            db.DanhsachdangkisanphamNCCs.Remove(loai);
            db.SaveChanges();
        }
    }
}