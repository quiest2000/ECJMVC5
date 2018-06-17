using System;
using System.Linq;
using System.Web.Mvc;
using ECommerce.Models;

namespace ECommerce.Controllers
{
    public class SideBarController : Controller
    {
        // GET: SlideBar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductFilter()
        {
            return PartialView("ProductFilter");
        }

        public ActionResult GiamGiaNhieu()
        {
            var sp = new SanPhamModel();
            var splist = sp.SPKhuyenMai();
            splist = splist.Take(5);
            return PartialView("_GiamGiaNhieuPartial", splist);
        }

        public ActionResult KhuyenMaiPost()
        {
            var km = new KhuyenMaiModel();
            return PartialView("_KhuyenMaiPost",km.TimKhuyenMai(null, null, null).Where(m=> m.NgayBatDau <= DateTime.Today && m.NgayKetThuc >= DateTime.Today));
        }
    }
}