using System;
using System.Linq;
using System.Web.Mvc;
using ECommerce.Models.Donhang;

namespace ECommerce.Controllers
{
    public class ThongKeController : Controller
    {
        //
        // GET: /ThongKe/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThongKeDoanhThu()
        {
            return View();
        }

        public ActionResult ThongKeDTTheoTG(DateTime? froms, DateTime? tos)
        {
            if (froms == null)
                froms = DateTime.Today.AddMonths(-1);
            if (tos == null)
                tos = DateTime.Today;
            ViewBag.froms = froms.Value.ToShortDateString();
            ViewBag.tos = tos.Value.ToShortDateString();
            var donhang = new DonHangKHModel();
            return PartialView("TheoTime", donhang.ThongKeDoanhThu(froms, tos).ToList());
        }

        public ActionResult ThongKeTiTrong(DateTime? froms, DateTime? tos)
        {
            if (froms == null)
                froms = DateTime.Today.AddMonths(-1);
            if (tos == null)
                tos = DateTime.Today;
            ViewBag.froms = froms.Value.ToShortDateString();
            ViewBag.tos = tos.Value.ToShortDateString();
            var donhang = new DonHangKHModel();
            return PartialView("TheoTiTrong", donhang.ThongKeTiTrong(froms, tos).ToList());
        }
	}
}