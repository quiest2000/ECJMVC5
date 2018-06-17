using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Models.Constants;
using ECommerce.Models.Domain.EfModels;
using PagedList;

namespace ECommerce.Controllers
{
    [AuthLog(Roles =RoleNames.Administrator + "," + RoleNames.Employee)]
    public class LoaiSPController : Controller
    {
        //
        // GET: /LoaiSpId/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditLoaiSP(int id)
        {
            if (id == 0)
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
        public ActionResult EditLoaiSP([Bind(Include = "Id,TenLoai")] LoaiSanPham loai)
        {
            var spm = new CategoryModel();
            if (ModelState.IsValid)
            {
                spm.EditLoaiSP(loai);
                return RedirectToAction("Index");
            }
            return View(loai);
        }

        public ActionResult DeleteLoaiSP(int id)
        {
            if (id == 0)
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
        public ActionResult ThemLoaiSP([Bind(Include = "TenLoai")] LoaiSanPham loai)
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
        public ActionResult MultibleDel(List<int> lstdel)
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

        public ActionResult PhanTrangSP(IQueryable<LoaiSanPham> lst, int? page, int? pagesize)
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