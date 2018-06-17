﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using EC_TH2012_J.Models;
using System.Net;
using EC_TH2012_J.Models.Domain.EfModels;

namespace EC_TH2012_J.Controllers
{
    [AuthLog(Roles = "Quản trị viên,Nhân viên")]
    public class LoaiSPController : Controller
    {
        //
        // GET: /LoaiSP/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditLoaiSP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lm = new CategoryModel();
            var sp = lm.FindById(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLoaiSP([Bind(Include = "MaLoai,TenLoai")] LoaiSP loai)
        {
            var spm = new CategoryModel();
            if (ModelState.IsValid)
            {
                spm.EditLoaiSP(loai);
                return RedirectToAction("Index");
            }
            return View(loai);
        }

        public ActionResult DeleteLoaiSP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var spm = new CategoryModel();
            if (spm.FindById(id) == null)
            {
                return HttpNotFound();
            }
            spm.DeleteLoaiSP(id);
            return TimLoaiSP(null, null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemLoaiSP([Bind(Include = "TenLoai")] LoaiSP loai)
        {
            var spm = new CategoryModel();
            if (ModelState.IsValid && spm.KiemTraTen(loai.TenLoai))
            {
                var maloai = spm.ThemLoaiSP(loai);
                return View("Index");
            }
            return View("Index", loai);
        }

        [HttpPost]
        public ActionResult MultibleDel(List<string> lstdel)
        {
            if (lstdel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foreach (var item in lstdel)
            {
                var spm = new CategoryModel();
                if (spm.FindById(item) == null)
                {
                    return HttpNotFound();
                }
                spm.DeleteLoaiSP(item);
            }
            return TimLoaiSP(null, null);
        }

        public ActionResult TimLoaiSP(string key, int? page)
        {
            var spm = new CategoryModel();
            ViewBag.key = key;
            return PhanTrangSP(spm.SearchByName(key), page, null);
        }

        public ActionResult PhanTrangSP(IQueryable<LoaiSP> lst, int? page, int? pagesize)
        {
            var pageSize = (pagesize ?? 10);
            var pageNumber = (page ?? 1);
            return PartialView("LoaiSPPartial", lst.OrderBy(m => m.TenLoai).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult kiemtra(string key)
        {
            var spm = new CategoryModel();
            if (spm.KiemTraTen(key))
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

    }
}