using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ECommerce.Models.B2B;
using ECommerce.Models.Domain.EfModels;
using PagedList;

namespace ECommerce.Controllers.B2B
{
    public class SanPhamCanMuaController : Controller
    {
        //
        // GET: /SanPhamCanMua/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TimSPCM(string key, int? page)
        {
            var spcm = new SanPhamCanMuaModel();
            ViewBag.key = key;
            return PhanTrangSPCM(spcm.TimSPCM(key), page, null);
        }

        public ActionResult DeleteSPCM(int id)
        {
            var ncc = new SanPhamCanMuaModel();
            if (ncc.getSanphamcanmua(id) == null)
            {
                return HttpNotFound();
            }
            ncc.DeleteSPCM(id);
            return TimSPCM(null, null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSPCM(SanPhamCanMuaAdd loai)
        {

            if (ModelState.IsValid)
            {
                var ncc = new SanPhamCanMuaModel();               
                ncc.ThemSPCM(GetData(loai));
                return View("Index");
            }
            return View("Index", loai);
        }

        public ActionResult EditSPCM(int id)
        {
            var sp = new SanPhamCanMuaModel();
            var s = sp.getSanphamcanmua(id);
            var model = new SanPhamCanMuaEdit();
            model.ID = s.Id;
            model.Mota = s.Mota;
            model.Ngayketthuc = s.Ngayketthuc;
            model.Soluong = s.Soluong;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSPCM(SanPhamCanMuaEdit loai)
        {

            if (ModelState.IsValid)
            {
                var ncc = new SanPhamCanMuaModel();
                ncc.EditSPCM(loai);
                return RedirectToAction("Index");
            }
            return RedirectToAction("EditSPCM", loai.ID);
        }


        private SanPhamCanMua GetData(SanPhamCanMuaAdd loai)
        {
            var sp = new SanPhamCanMua();
            sp.SanPhamId = loai.MaSP;
            sp.Ngayketthuc = loai.Ngayketthuc;
            sp.Mota = loai.Mota;
            sp.Soluong = loai.Soluong;
            return sp;
        }

        [HttpPost]
        public ActionResult MultibleDel(List<string> lstdel)
        {
            foreach (var item in lstdel)
            {
                var ncc = new SanPhamCanMuaModel();
                ncc.DeleteSPCM(Convert.ToInt32(item));
            }
            return TimSPCM(null, null);
        }

        public ActionResult PhanTrangSPCM(IQueryable<SanPhamCanMua> lst, int? page, int? pagesize)
        {
            var pageSize = (pagesize ?? 10);
            var pageNumber = (page ?? 1);
            return PartialView("SPCMPartial", lst.OrderByDescending(m => m.Ngaydang).ToPagedList(pageNumber, pageSize));
        }
	}
}