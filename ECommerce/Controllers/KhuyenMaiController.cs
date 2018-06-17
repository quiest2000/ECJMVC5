using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Models.Constants;
using ECommerce.Models.Domain.EfModels;
using PagedList;

namespace ECommerce.Controllers
{
    [AuthLog(Roles = RoleNames.Administrator + "," + RoleNames.Employee)]
    public class KhuyenMaiController : Controller
    {
        //
        // GET: /KhuyenMai/
        public ActionResult Index()
        {
            return View();
        }

        public bool UploadAnh(HttpPostedFileBase file, string tenfile)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                var name = Path.GetExtension(file.FileName);
                // extract only the filename
                if (!Path.GetExtension(file.FileName).Equals(".jpg"))
                {
                    return false;
                }
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/images/khuyenmai"), tenfile + ".jpg");
                file.SaveAs(path);
                return true;
            }
            // redirect back to the index action to show the form once again
            return false;
        }

        public bool DeleteAnh(string filename)
        {
            var fullPath = Request.MapPath("~/images/khuyenmai/" + filename);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                return true;
            }
            return false;
        }

        public ActionResult EditKhuyenMai(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lm = new KhuyenMaiModel();
            var sp = lm.FindById(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditKhuyenMai([Bind(Include = "Id,TenCT,NgayBatDau,NgayKetThuc,NoiDung")] KhuyenMai loai, HttpPostedFileBase ad)
        {
            var spm = new KhuyenMaiModel();
            if (ModelState.IsValid)
            {
                spm.EditKhuyenMai(loai);
                UploadAnh(ad, loai.Id + "1");
                return RedirectToAction("Index");
            }
            return View(loai);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemKhuyenMai([Bind(Include = "TenCT,NgayBatDau,NgayKetThuc,NoiDung")] KhuyenMai loai, HttpPostedFileBase ad)
        {
            var spm = new KhuyenMaiModel();
            if (ModelState.IsValid && spm.KiemTraTen(loai.TenCT))
            {
                var makm = spm.ThemKhuyenMai(loai);
                UploadAnh(ad, makm + "1");
                return RedirectToAction("SuaCTKhuyenMai", new { MaKM = makm });
            }
            return View("Index", loai);
        }

        public ActionResult DeleteKhuyenMai(int id)
        {
            var spm = new KhuyenMaiModel();
            DeleteAnh(spm.FindById(id).AnhCT);
            spm.DeleteKhuyenMai(id);
            return TimKhuyenMai(null, null, null, null);
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
                var spm = new KhuyenMaiModel();
                DeleteAnh(spm.FindById(item).AnhCT);
                spm.DeleteKhuyenMai(item);
            }
            return TimKhuyenMai(null, null, null, null);
        }

        public ActionResult DeleteSPKhuyenMai(int makm, int masp)
        {
            var spm = new KhuyenMaiModel();
            spm.DeleteSPKhuyenMai(makm, masp);

            return RedirectToAction("DSSanPham", new { makm = makm });
        }

        public ActionResult TimKhuyenMai(string key, DateTime? start, DateTime? end, int? page)
        {
            var spm = new KhuyenMaiModel();
            ViewBag.key = key;
            ViewBag.start = start;
            ViewBag.end = end;
            return PhanTrangKhuyenMai(spm.TimKhuyenMai(key, start, end), page, null);
        }

        public ActionResult PhanTrangKhuyenMai(IQueryable<KhuyenMai> lst, int? page, int? pagesize)
        {
            var pageSize = (pagesize ?? 10);
            var pageNumber = (page ?? 1);
            return PartialView("KhuyenMaiPartial", lst.OrderBy(m => m.NgayBatDau).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult kiemtra(string key)
        {
            var spm = new KhuyenMaiModel();
            if (spm.KiemTraTen(key))
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CTKhuyenMai(string id)
        {
            var km = new KhuyenMaiModel();
            var lst = km.CTKhuyenMai(id);
            if (lst.Any())
                return PartialView("KhuyenMaiDetail", lst);
            return null;
        }

        [HttpPost]
        public ActionResult ThemSPKhuyenMai(List<SanPhamKhuyenMai> lstkt)
        {
            if (lstkt.Count == 0)
            {
                return RedirectToAction("Index");
            }
            var spm = new KhuyenMaiModel();
            foreach (var item in lstkt)
            {
                if (item.SanPhamId > 0 && item.KhuyenMaiId > 0)
                    spm.ThemSPKhuyenMai(item);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ThemSP1KhuyenMai([Bind(Include = "Id,Id,MoTa,GiamGia")] SanPhamKhuyenMai spkm)
        {
            var spm = new KhuyenMaiModel();
            spm.ThemSPKhuyenMai(spkm);
            return RedirectToAction("DSSanPham", new { makm = spkm.KhuyenMaiId });
        }

        [HttpPost]
        public ActionResult SuaCTKhuyenMai(List<SanPhamKhuyenMai> lstkt)
        {
            if (lstkt.Count == 0)
            {
                return RedirectToAction("Index");
            }
            var spm = new KhuyenMaiModel();
            spm.DelAllSPKM(lstkt[0].KhuyenMaiId);
            foreach (var item in lstkt)
            {
                if (item.SanPhamId > 0 && item.KhuyenMaiId > 0)
                    spm.ThemSPKhuyenMai(item);
            }
            return RedirectToAction("Index");
        }

        public ActionResult SuaCTKhuyenMai(string MaKM)
        {
            var sp = new SanPhamModel();
            ViewBag.LoaiSP = new SelectList(sp.GetAllLoaiSP(), "Id", "TenLoai");
            ViewBag.makm = MaKM;
            return View("SuaSPKhuyenMai");
        }


        public ActionResult DSSanPham(string keyTenSp, int maloai, int makm, int? page)
        {
            ViewBag.key = keyTenSp;
            ViewBag.maloai = maloai;
            ViewBag.makm = makm;
            var km = new KhuyenMaiModel();
            var lst = km.DSSP(keyTenSp, maloai, makm);
            if (lst.Any())
                return PhanTrangSP(lst, "DSSanPham", page, null);
            return null;
        }

        public ActionResult DSSanPhamKhuyenMai(string keyTenSp, int maloai, int makm, int? page)
        {
            ViewBag.key = keyTenSp;
            ViewBag.maloai = maloai;
            ViewBag.makm = makm;
            var km = new KhuyenMaiModel();
            var lst = km.DSSanPhamKhuyenMai(keyTenSp, maloai, makm);
            if (lst.Any())
                return PhanTrangSP(lst, "DSSanPhamKhuyenMai", page, null);
            return null;
        }


        public ActionResult PhanTrangSP(IQueryable<SanPham> lst, string stringview, int? page, int? pagesize)
        {
            var pageSize = (pagesize ?? 10);
            var pageNumber = (page ?? 1);
            return PartialView(stringview, lst.OrderBy(m => m.Id).ToPagedList(pageNumber, pageSize));
        }

        [AllowAnonymous]
        public ActionResult KhuyenMaiPost(int id)
        {
            var km = new KhuyenMaiModel();
            return View("KhuyenMaiPostView", km.FindById(id));
        }

    }
}