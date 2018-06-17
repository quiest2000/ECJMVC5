using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Models.Domain.EfModels;
using PagedList;

namespace ECommerce.Controllers
{
    public class AdminController : Controller
    {
        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult SanPham()
        {
            var spm = new SanPhamModel();
            ViewBag.HangSX = new SelectList(spm.GetAllHangSX(), "Id", "TenHang");
            ViewBag.LoaiSP = new SelectList(spm.GetAllLoaiSP(), "Id", "TenLoai");
            return View();
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult EditSP(int spId)
        {
            if (spId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var spm = new SanPhamModel();
            var sp = spm.FindById(spId);
            if (sp == null)
            {
                return HttpNotFound();
            }
            ViewBag.HangSX = new SelectList(spm.GetAllHangSX().ToList(), "Id", "TenHang", sp.HangSxId);
            ViewBag.LoaiSP = new SelectList(spm.GetAllLoaiSP().ToList(), "Id", "TenLoai", sp.LoaiSpId);
            return View(sp);
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSP([Bind(Include = "Id,TenSP,LoaiSpId,Id,XuatXu,GiaGoc,MoTa,SoLuong,isnew,ishot")] SanPham sanpham, HttpPostedFileBase ad, HttpPostedFileBase an, HttpPostedFileBase ak)
        {
            var spm = new SanPhamModel();
            if (ModelState.IsValid)
            {
                spm.EditSP(sanpham);
                UploadAnh(ad,sanpham.Id + "1");
                UploadAnh(an, sanpham.Id + "2");
                UploadAnh(ak, sanpham.Id + "3");
                return RedirectToAction("SanPham");
            }
            ViewBag.HangSX = new SelectList(spm.GetAllHangSX(), "Id", "TenHang", sanpham.HangSxId);
            ViewBag.LoaiSP = new SelectList(spm.GetAllLoaiSP(), "Id", "TenLoai", sanpham.LoaiSpId);            
            return View(sanpham);
        }

        [AuthLog(Roles = "Quản trị viên")]
        public ActionResult DeleteSP(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var spm = new SanPhamModel();
            DeleteAnh(spm.FindById(id).AnhDaiDien);
            DeleteAnh(spm.FindById(id).AnhNen);
            DeleteAnh(spm.FindById(id).AnhKhac);
            spm.DeleteSP(id);
            return TimSP(null,null,null);
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public bool UploadAnh(HttpPostedFileBase file,string tenfile)
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
                var path = Path.Combine(Server.MapPath("~/images/products"), tenfile + ".jpg");
                file.SaveAs(path);
                return true;
            }
            // redirect back to the index action to show the form once again
            return false;
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSP([Bind(Include = "TenSP,LoaiSpId,Id,XuatXu,GiaGoc,MoTa,SoLuong,isnew,ishot")] SanPham sanpham, HttpPostedFileBase ad, HttpPostedFileBase an, HttpPostedFileBase ak)
        {
            var spm = new SanPhamModel();
            if (ModelState.IsValid)
            {
                var masp = spm.ThemSP(sanpham);
                UploadAnh(ad, masp + "1");
                UploadAnh(an, masp + "2");
                UploadAnh(ak, masp + "3");
                var ts = new ThongSoKyThuat();
                ts.SanPhamId = masp;
                var lst = new List<ThongSoKyThuat>();
                lst.Add(ts);
                return View("ThemThongSoKT", lst);
            }
            ViewBag.HangSX = new SelectList(spm.GetAllHangSX(), "Id", "TenHang", sanpham.HangSxId);
            ViewBag.LoaiSP = new SelectList(spm.GetAllLoaiSP(), "Id", "TenLoai", sanpham.LoaiSpId);
            return View("SanPham",sanpham);
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult SPDetail(int id)
        {
            var sp = new SanPhamModel();
            return PartialView("SPDetail", sp.FindById(id));
        }

        [AuthLog(Roles = "Quản trị viên")]
        [HttpPost]
        public ActionResult MultibleDel(List<int> lstdel)
        {
            if (lstdel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foreach (var item in lstdel)
            {
                var spm = new SanPhamModel();
                DeleteAnh(spm.FindById(item).AnhDaiDien);
                DeleteAnh(spm.FindById(item).AnhNen);
                DeleteAnh(spm.FindById(item).AnhKhac);
                spm.DeleteSP(item);
            } 
            return TimSP(null,null,null);
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        [HttpPost]
        public ActionResult ThemThongSoKT(List<ThongSoKyThuat> lstkt) 
        {
            if(lstkt.Count == 0)
            {
                return RedirectToAction("SanPham");
            }
            var spm = new SanPhamModel();
            foreach (var item in lstkt)
            {
                if (!string.IsNullOrEmpty(item.ThuocTinh))
                    spm.ThemTSKT(item);
            }
            return RedirectToAction("SanPham"); 
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        [HttpPost]
        public ActionResult SuaThongSoKT(List<ThongSoKyThuat> lstkt)
        {
            if (lstkt.Count == 0)
            {
                return RedirectToAction("SanPham");
            }
            var spm = new SanPhamModel();
            spm.DelAllTSKT(lstkt[0].SanPhamId);
            foreach (var item in lstkt)
            {
                if (!string.IsNullOrEmpty(item.ThuocTinh))
                    spm.ThemTSKT(item);
            }
            return RedirectToAction("SanPham");
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult SuaThongSoKT(int masp)
        {
            var spm = new SanPhamModel();
            if (spm.GetTSKT(masp).ToList().Count == 0)
            {
                var ts = new ThongSoKyThuat();
                ts.SanPhamId = masp;
                var lst = new List<ThongSoKyThuat>();
                lst.Add(ts);
                return View("ThemThongSoKT", lst);
            }
            return View("SuaThongSoKT",spm.GetTSKT(masp).ToList());
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult TimSP(string key,string maloai,int? page)
        {
            var spm = new SanPhamModel();
            ViewBag.key = key;
            ViewBag.maloai = maloai;
            return PhanTrangSP(spm.AdvancedSearch(key, maloai, null, null, null),page,null);
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult PhanTrangSP(IQueryable<SanPham> lst,int? page, int? pagesize)
        {
            var pageSize = (pagesize ?? 10);
            var pageNumber = (page ?? 1);
            return PartialView("SanPhamPartial", lst.OrderBy(m => m.Id).ToPagedList(pageNumber, pageSize));
        }

        [AuthLog(Roles = "Quản trị viên")]
        public bool DeleteAnh(string filename)
        {
            var fullPath = Request.MapPath("~/images/products/" + filename);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                return true;
            }
            return false;
        }

    }
}