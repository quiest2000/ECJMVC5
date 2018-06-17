using System;
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
    public class HangSXController : Controller
    {
        //
        // GET: /HangSX/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditHangSX(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lm = new HangSanXuatModel();
            var sp = lm.FindById(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditHangSX([Bind(Include = "HangSX,TenHang,TruSoChinh,QuocGia")] HangSanXuat loai)
        {
            var spm = new HangSanXuatModel();
            if (ModelState.IsValid)
            {
                spm.EditHangSX(loai);
                return RedirectToAction("Index");
            }
            return View(loai);
        }

        public ActionResult DeleteHangSX(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var spm = new HangSanXuatModel();
            if (spm.FindById(id) == null)
            {
                return HttpNotFound();
            }
            spm.DeleteHangSX(id);
            return TimHangSX(null, null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemHangSX([Bind(Include = "TenHang,TruSoChinh,QuocGia")] HangSanXuat loai)
        {
            var spm = new HangSanXuatModel();
            if (ModelState.IsValid && spm.KiemTraTen(loai.TenHang))
            {
                var maloai = spm.ThemHangSX(loai);
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
                var spm = new HangSanXuatModel();
                if (spm.FindById(item) == null)
                {
                    return HttpNotFound();
                }
                spm.DeleteHangSX(item);
            }
            return TimHangSX(null, null);
        }

        public ActionResult TimHangSX(string key, int? page)
        {
            var spm = new HangSanXuatModel();
            ViewBag.key = key;
            return PhanTrangHangSX(spm.SearchByName(key), page, null);
        }

        public ActionResult PhanTrangHangSX(IQueryable<HangSanXuat> lst, int? page, int? pagesize)
        {
            var pageSize = (pagesize ?? 10);
            var pageNumber = (page ?? 1);
            return PartialView("HangSXPartial", lst.OrderBy(m => m.TenHang).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult kiemtra(string key)
        {
            var spm = new HangSanXuatModel();
            if (spm.KiemTraTen(key))
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }
	}
}