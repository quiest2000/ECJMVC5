﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EC_TH2012_J.Models;
using EC_TH2012_J.Models.Domain.EfModels;

namespace EC_TH2012_J.Controllers
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