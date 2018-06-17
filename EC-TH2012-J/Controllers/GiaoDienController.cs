using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EC_TH2012_J.Models;
using EC_TH2012_J.Models.Domain.EfModels;

namespace EC_TH2012_J.Controllers
{

    public class GiaoDienController : Controller
    {
        //
        // GET: /GiaoDien/
        public ActionResult Header()
        {
            var dd = new GiaoDienModel();
            var model = dd.GetDD().ToList();
            return View(model);
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult General()
        {
            var dd = new GiaoDienModel();
            var model = dd.GetDD().ToList();
            return View(model);
        }

        public ActionResult SlideShowView()
        {
            var km = new KhuyenMaiModel();
            return PartialView("SlideShowView", km.TimKhuyenMai(null, null, null).Where(m => m.NgayBatDau <= DateTime.Today && m.NgayKetThuc >= DateTime.Today));
        }

        public ActionResult SlideShowSetting()
        {
            var gd = new GiaoDienModel();
            var linklist = gd.GetSlideShow().ToList();
            return View(linklist);
        }

        public ActionResult SlideShow()
        {
            var link = new Link();
            link.Group = "SlideShow";
            return View(link);
        }

    }
}