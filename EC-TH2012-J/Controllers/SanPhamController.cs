using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EC_TH2012_J.Models;
using PagedList;
using PagedList.Mvc;
using EC_TH2012_J.App_Start;
using EC_TH2012_J.Models.Domain;
using EC_TH2012_J.Models.Domain.EfModels;

namespace EC_TH2012_J.Controllers
{
    public class SanPhamController : Controller
    {
        private MainContext db = new MainContext();
        private SanPhamModel sp = new SanPhamModel();
        // GET: SanPham
        [Trackingactionfilter]
        public ActionResult Index(string id)
        {
            var sp = db.SanPhams.Find(id);
            return View("ProductDetail",sp);
        }
        public ActionResult SearchByName(string tensp)
        {
            var splist = db.SanPhams.Where(u => u.TenSP.Contains(tensp));
            ViewBag.CurrentFilter = tensp;
            splist = splist.OrderByDescending(u => u.TenSP);
            return View(splist);
        }

        public ActionResult SearchByType(string MaLoai)
        {
            var splist = (from p in db.SanPhams where p.LoaiSP.Equals(MaLoai) select p);
            return View(splist);
        }
        public ActionResult Loadsplienquan(string maloai,int sl)
        {
            var splist = sp.SearchByType(maloai);
            splist = splist.Take(sl);
            return PartialView("_PartialSanPhamLienQuan", splist);
        }
        
        public ActionResult ThongSoKyThuat(string MaSP)
        {
            var spm = new SanPhamModel();
            return PartialView("ThongSoKyThuatPartial", spm.GetTSKT(MaSP));
        }

    }
}