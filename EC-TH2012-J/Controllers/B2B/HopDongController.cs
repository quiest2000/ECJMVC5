﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using EC_TH2012_J.Models;
using EC_TH2012_J.Models.B2B;
using HopDongNCC = EC_TH2012_J.Models.Domain.EfModels.HopDongNCC;

namespace EC_TH2012_J.Controllers.B2B
{
    public class HopDongController : Controller
    {
        //Hop dong
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TinhTrangGiaoHang()
        {
            return View();
        }

        public ActionResult TimHopDong(string key, string tensp, bool? loaihd, int? page)
        {
            var ncc = new NhaCungCapModel();
            ViewBag.key = key;
            ViewBag.tensp = tensp;
            ViewBag.loaihd = loaihd;
            return PhanTrangHopDong(ncc.TimHopDong(key, tensp, loaihd), page, null);
        }
        public ActionResult TimHopDongB2B(string key, string tensp, bool? loaihd, int? page)
        {
            var ncc = new NhaCungCapModel();
            ViewBag.key = key;
            ViewBag.tensp = tensp;
            ViewBag.loaihd = loaihd;
            return PhanTrangHopDongB2B(ncc.TimHopDong(key, tensp, loaihd), page, null);
        }
        

        public ActionResult DeleteHopDong(string id)
        {
            var ncc = new NhaCungCapModel();
            if (ncc.FindById(id) == null)
            {
                return HttpNotFound();
            }
            ncc.DeleteHopDong(id);
            return TimHopDong(null, null, null, null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemHopDong([Bind(Include = "MaNCC,NgayKy,ThoiHanHD,MaSP,SLToiThieu,SLCungCap,SoNgayGiao,isBuy,DonGia")] HopDongNCC loai)
        {
            var ncc = new NhaCungCapModel();
            if (ModelState.IsValid)
            {
                var maHD = ncc.ThemHopDong(loai);
                if(!(bool)loai.IsBuy)
                {
                    return RedirectToAction("ConfigAPI", "AdminB2B", new { MaNCC = loai.MaNCC });
                }
                return View("Index");
            }
            return View("Index", loai);
        }

        [HttpPost]
        public ActionResult MultibleDel(List<string> lstdel)
        {
            foreach (var item in lstdel)
            {
                var ncc = new NhaCungCapModel();
                ncc.DeleteHopDong(item);
            }
            return TimHopDong(null, null, null, null);
        }

        public ActionResult PhanTrangHopDong(IQueryable<HopDongNCC> lst, int? page, int? pagesize)
        {
            var pageSize = (pagesize ?? 10);
            var pageNumber = (page ?? 1);
            return PartialView("HopDongPartial", lst.OrderByDescending(m => m.NgayKy).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult PhanTrangHopDongB2B(IQueryable<HopDongNCC> lst, int? page, int? pagesize)
        {
            var pageSize = (pagesize ?? 10);
            var pageNumber = (page ?? 1);
            return PartialView("HopDongB2BPartial", lst.OrderByDescending(m => m.NgayKy).ToPagedList(pageNumber, pageSize));
        }


        public ActionResult TTGiaoHang(string key, string tensp, bool? loaihd, bool? tt,int? page)
        {
            var ncc = new NhaCungCapModel();
            ViewBag.key = key;
            ViewBag.tensp = tensp;
            ViewBag.loaihd = loaihd;
            ViewBag.tt = tt;
            var pageNumber = (page ?? 1);
            return PartialView("TTGiaoHangPartial", ncc.TimHopDong(key, tensp, loaihd,tt).OrderByDescending(m=>m.SoNgayGiao).ToPagedList(pageNumber, 10));
        }

        [HttpPost]
        public ActionResult XacNhanDaGiao(List<string> lstdel)
        {
            foreach (var item in lstdel)
            {
                var ncc = new NhaCungCapModel();
                ncc.XacNhanDaGiao(item,true);
            }
            return TTGiaoHang(null, null, null, null,null);
        }

        [HttpPost]
        public ActionResult XacNhanThanhToan(List<string> lstdel)
        {
            foreach (var item in lstdel)
            {
                var ncc = new NhaCungCapModel();
                ncc.XacNhanDaTT(item, true);
            }
            return TTGiaoHang(null, null, null, null, null);
        }
        public JsonResult Xacnhanthanhtoan(string MaHD)
        {
            var modelNCC = new HopdongNCCModel();
            if(modelNCC.SetTTThanhtoan(MaHD,true))
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }

            return Json("faill", JsonRequestBehavior.AllowGet);
        }
	}
}