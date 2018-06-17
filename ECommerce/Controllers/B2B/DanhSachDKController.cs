using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ECommerce.Models.B2B;
using PagedList;
using DangKySanPhamNcc = ECommerce.Models.Domain.EfModels.DangKySanPhamNcc;


namespace ECommerce.Controllers.B2B
{
    public class DanhSachDKController : Controller
    {
        // GET: DanhSachDK
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TimDS(string tensp, string tenncc, int? tt, int? page)
        {
            var DSDK = new DSDangKyModel();
            ViewBag.tensp = tensp;
            ViewBag.tenncc = tenncc;
            ViewBag.tt = tt;
            return PhanTrangDSDK(DSDK.TimDS(tensp,tenncc,tt), page, null);
        }

        [HttpPost]
        public ActionResult KyHopDong(string id)
        {
            var sp = new SanPhamCanMuaModel();
            sp.DeleteSPCM(Convert.ToInt32(id));
            return TimDS(null, null, null, null);

        }

        public ActionResult DeleteDSDK(int id)
        {
            var ncc = new DSDangKyModel();
            if (ncc.Findbyid(id) == null)
            {
                return HttpNotFound();
            }
            ncc.DeleteDSDK(id);
            return TimDS(null, null,null,null);
        }

        [HttpPost]
        public ActionResult MultibleDel(List<string> lstdel)
        {
            foreach (var item in lstdel)
            {
                var ncc = new DSDangKyModel();
                ncc.DeleteDSDK(Convert.ToInt32(item));
            }
            return TimDS(null, null, null, null);
        }

        public ActionResult PhanTrangDSDK(IQueryable<DangKySanPhamNcc> lst, int? page, int? pagesize)
        {
            var pageSize = (pagesize ?? 10);
            var pageNumber = (page ?? 1);
            return PartialView("DSDKPartial", lst.OrderByDescending(m => m.NgayDK).ToPagedList(pageNumber, pageSize));
        }
    }
}