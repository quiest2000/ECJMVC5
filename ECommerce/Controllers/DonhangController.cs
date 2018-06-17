using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ECommerce.Models.Donhang;
using PagedList;
using DonHangKH = ECommerce.Models.Domain.EfModels.DonHangKH;

namespace ECommerce.Controllers
{
    [AuthLog(Roles = "Quản trị viên,Nhân viên")]
    public class DonhangController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TimDonHang(string key, string mobile, DateTime? date, int? status, int? page)
        {
            var spm = new DonHangKHModel();
            ViewBag.key = key;
            ViewBag.date = date;
            ViewBag.status = status;
            ViewBag.mobile = mobile;
            return PhanTrangDH(spm.TimDonHang(0, mobile, date, status), page, null);
        }

        [HttpPost]
        public ActionResult UpdateTinhTrangDH(int madh, int? tt)
        {
            var dh = new DonHangKHModel();
            dh.UpdateTinhTrang(madh, tt);
            return RedirectToAction("TimDonHang");
        }

        [HttpPost]
        public ActionResult MultibleUpdate(List<int> lst, int? tt)
        {
            foreach (var item in lst)
            {
                UpdateTinhTrangDH(item, tt);
            }
            return RedirectToAction("TimDonHang");
        }

        public ActionResult PhanTrangDH(IQueryable<DonHangKH> lst, int? page, int? pagesize)
        {
            var pageSize = (pagesize ?? 10);
            var pageNumber = (page ?? 1);
            return PartialView("DonHangPartial", lst.OrderBy(m => m.TinhTrangDH).ToPagedList(pageNumber, pageSize));
        }

        // GET: Donhang
        public ActionResult Chitietdonhang(int maDh)
        {
            var ctdh = new DonHangKHModel();
            return PartialView("DonHangDetail",ctdh.ChiTietDonHang(maDh));
        }
    }
}