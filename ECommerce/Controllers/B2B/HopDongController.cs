using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ECommerce.Models.B2B;
using PagedList;
using HopDongNcc = ECommerce.Models.Domain.EfModels.HopDongNcc;

namespace ECommerce.Controllers.B2B
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

        public ActionResult TimHopDong(int maHd, string tensp, bool? loaihd, int? page)
        {
            var ncc = new NhaCungCapModel();
            ViewBag.key = maHd;
            ViewBag.tensp = tensp;
            ViewBag.loaihd = loaihd;
            return PhanTrangHopDong(ncc.TimHopDong(maHd, tensp, loaihd), page, null);
        }
        public ActionResult TimHopDongB2B(int maHd, string tensp, bool? loaihd, int? page)
        {
            var ncc = new NhaCungCapModel();
            ViewBag.key = maHd;
            ViewBag.tensp = tensp;
            ViewBag.loaihd = loaihd;
            return PhanTrangHopDongB2B(ncc.TimHopDong(maHd, tensp, loaihd), page, null);
        }
        

        public ActionResult DeleteHopDong(int id)
        {
            var ncc = new NhaCungCapModel();
            if (ncc.FindById(id) == null)
            {
                return HttpNotFound();
            }
            ncc.DeleteHopDong(id);
            return TimHopDong(0, null, null, null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemHopDong([Bind(Include = "NhaCungCapId,NgayKy,ThoiHanHD,Id,SLToiThieu,SLCungCap,SoNgayGiao,isBuy,DonGia")] HopDongNcc loai)
        {
            var ncc = new NhaCungCapModel();
            if (ModelState.IsValid)
            {
                var maHD = ncc.ThemHopDong(loai);
                if(!(bool)loai.IsBuy)
                {
                    return RedirectToAction("ConfigAPI", "AdminB2B", new { MaNCC = loai.NccId });
                }
                return View("Index");
            }
            return View("Index", loai);
        }

        [HttpPost]
        public ActionResult MultibleDel(List<int> lstdel)
        {
            foreach (var item in lstdel)
            {
                var ncc = new NhaCungCapModel();
                ncc.DeleteHopDong(item);
            }
            return TimHopDong(0, null, null, null);
        }

        public ActionResult PhanTrangHopDong(IQueryable<HopDongNcc> lst, int? page, int? pagesize)
        {
            var pageSize = (pagesize ?? 10);
            var pageNumber = (page ?? 1);
            return PartialView("HopDongPartial", lst.OrderByDescending(m => m.NgayKy).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult PhanTrangHopDongB2B(IQueryable<HopDongNcc> lst, int? page, int? pagesize)
        {
            var pageSize = (pagesize ?? 10);
            var pageNumber = (page ?? 1);
            return PartialView("HopDongB2BPartial", lst.OrderByDescending(m => m.NgayKy).ToPagedList(pageNumber, pageSize));
        }


        public ActionResult TTGiaoHang(int maHd, string tensp, bool? loaihd, bool? tt,int? page)
        {
            var ncc = new NhaCungCapModel();
            ViewBag.key = maHd;
            ViewBag.tensp = tensp;
            ViewBag.loaihd = loaihd;
            ViewBag.tt = tt;
            var pageNumber = (page ?? 1);
            return PartialView("TTGiaoHangPartial", ncc.TimHopDong(maHd, tensp, loaihd,tt).OrderByDescending(m=>m.SoNgayGiao).ToPagedList(pageNumber, 10));
        }

        [HttpPost]
        public ActionResult XacNhanDaGiao(List<int> lstdel)
        {
            foreach (var item in lstdel)
            {
                var ncc = new NhaCungCapModel();
                ncc.XacNhanDaGiao(item,true);
            }
            return TTGiaoHang(0, null, null, null,null);
        }

        [HttpPost]
        public ActionResult XacNhanThanhToan(List<int> lstdel)
        {
            foreach (var item in lstdel)
            {
                var ncc = new NhaCungCapModel();
                ncc.XacNhanDaTT(item, true);
            }
            return TTGiaoHang(0, null, null, null, null);
        }
        public JsonResult Xacnhanthanhtoan(int maHd)
        {
            var modelNCC = new HopdongNCCModel();
            if(modelNCC.SetTTThanhtoan(maHd,true))
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }

            return Json("faill", JsonRequestBehavior.AllowGet);
        }
	}
}