﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EC_TH2012_J.Models.Domain;
using EC_TH2012_J.Models.Domain.EfModels;

namespace EC_TH2012_J.Models.B2B
{
    public class DangKySanPhamModel
    {
        public string ID { get; set; }
        public int MaSPCM { get; set; }
        public string tenSanPham { get; set; }
        public string MaNCC { get; set; }
        public string tenNhaCC { get; set; }
        public string Ghichu { get; set; }
        public DateTime NgayDK { get; set; }
        public int Trangthai { get; set; }
        [Required]
        [Range(1000,99999999,ErrorMessage ="Xin nhập trong khoảng 1000-> 99999999")]
        public int TienmoiSP { get; set; }
        public bool ThemDanhKi(DangKySanPhamModel a)
        {
            using(var db = new MainContext())
            {
                try
                {
                    a.NgayDK = DateTime.Now;
                    a.Trangthai = 0;
                    var temp = new Domain.EfModels.DanhsachdangkisanphamNCC()
                    {
                        MaSPCanMua = a.MaSPCM,
                        MaNCC = a.MaNCC,
                        Ghichu = a.Ghichu,
                        NgayDK = a.NgayDK,
                        Trangthai = a.Trangthai,
                        TienmoiSP = a.TienmoiSP
                    };
                    db.DanhsachdangkisanphamNCCs.Add(temp);
                    db.SaveChanges();
                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }
            }
        }
        public DangKySanPhamModel createDangKiSanPham(string name, int ID_SPCM)
        {
            using(var db = new MainContext())
            {
                var temp = new DangKySanPhamModel();
                var spcm = (from p in db.Sanphamcanmuas where p.ID == ID_SPCM select p.SanPham).FirstOrDefault();
                var User = (from p in db.AspNetUsers where p.UserName == name select new { p.Id}).FirstOrDefault();
                var NCC = (from p in db.NhaCungCaps where p.Net_user == User.Id select new { p.MaNCC, p.TenNCC }).FirstOrDefault();
                temp.tenSanPham = spcm.TenSP;
                temp.MaSPCM = ID_SPCM;
                temp.MaNCC = NCC.MaNCC;
                temp.tenNhaCC = NCC.TenNCC;
                return temp;
            }
        }
        public List<Domain.EfModels.DanhsachdangkisanphamNCC> getDSDKNCC(int IDSPCM)
        {
            using(var db = new MainContext())
            {
                var ds =  db.DanhsachdangkisanphamNCCs.Where(m => m.MaSPCanMua == IDSPCM).ToList();
                foreach(var temp in ds)
                {
                    temp.TenNCC = (from p in db.NhaCungCaps where p.MaNCC == temp.MaNCC select p.TenNCC).FirstOrDefault();
                }
                return ds;
            }
        }
        
    }
}