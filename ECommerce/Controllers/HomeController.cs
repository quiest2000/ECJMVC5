﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Models.Constants;
using ECommerce.Models.Domain;
using ECommerce.Models.Donhang;
using ECommerce.Utils;
using Microsoft.AspNet.Identity;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        private static MainContext db = new MainContext();

        public static List<Member> Ds_Group;
        public ActionResult Index()
        {
            ManagerObiect.getIntance();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Thongtinnhom()
        {
            if (Ds_Group == null)
            {
                Ds_Group = new List<Member>();
                Ds_Group.Add(new Member
                {
                    MSSV = "1212293",
                    Hoten = "Nguyễn Ngọc Phúc",
                    LinkFacebook = "https://www.facebook.com/phuc.nguyen.eccentric?fref=pb_friends"
                });
                Ds_Group.Add(new Member
                {
                    MSSV = "1212080",
                    Hoten = "Huỳnh Phạm Hải Đăng",
                    LinkFacebook = "https://www.facebook.com/wayne.pham.507?fref=pb_friends"
                });
                Ds_Group.Add(new Member
                {
                    MSSV = "1212276",
                    Hoten = "Nguyễn Thành Nhân",
                    LinkFacebook = "https://www.facebook.com/GanderNguyen?fref=pb_friends"
                });
                Ds_Group.Add(new Member
                {
                    MSSV = "1212437",
                    Hoten = "Phan Ngọc Triều",
                    LinkFacebook = "https://www.facebook.com/taolibra?fref=pb_friends"
                });
                Ds_Group.Add(new Member
                {
                    MSSV = "1212502",
                    Hoten = "Nguyễn Văn Ty",
                    LinkFacebook = "https://www.facebook.com/vanty8"
                });
                Ds_Group.Add(new Member
                {
                    MSSV = "1212526",
                    Hoten = "Nguyễn Trương Vương",
                    LinkFacebook = "https://www.facebook.com/vuongtruong.nguyen?fref=pb_friends"
                });
                Ds_Group.Add(new Member
                {
                    MSSV = "1212535",
                    Hoten = "Vũ Thị Thanh Xuân",
                    LinkFacebook = "https://www.facebook.com/harusame.927?fref=pb_friends"
                });
            }
            return View(Ds_Group);
        }

        public ActionResult Cart()
        {
            return View(ManagerObiect.getIntance().giohang);
        }

        [AuthLog(Roles = RoleNames.Administrator + "," + RoleNames.Employee + "," + RoleNames.Customer)]
        //Đơn hàng
        public ActionResult Xemdonhang(int maKH)
        {
            var temp = new List<DonHangKHModel>();
            if (maKH != 0)
            {
                var dh = new DonHangKHModel();
                temp = dh.Xemdonhang(maKH);
            }
            return View(temp);
        }

        [AuthLog(Roles = RoleNames.Administrator + "," + RoleNames.Employee + "," + RoleNames.Customer)]
        public ActionResult Huydonhang(string maDH)
        {
            var dh = new DonHangKHModel();
            dh.HuyDH(maDH);
            var donhang = dh.Xemdonhang(User.Identity.GetUserId().ToInt());
            return View(donhang);
        }
        public ActionResult Checkout()
        {
            if (Request.IsAuthenticated)
            {
                var dh = new DonHangKHModel();
                dh.nguoiMua = dh.Xemttnguoidung(User.Identity.GetUserId().ToInt());
                var dhtq = new Donhangtongquan()
                {
                    buyer = dh.nguoiMua.HoTen,
                    seller = dh.nguoiMua.HoTen,
                    phoneNumber = dh.nguoiMua.PhoneNumber,
                    address = dh.nguoiMua.DiaChi
                };
                return View(dhtq);
            }
            else
            {
                return RedirectToAction("Authentication", "Account", new { returnUrl = "/Home/Checkout" });
            }
        }

        [AuthLog(Roles = RoleNames.Administrator + "," + RoleNames.Employee + "," + RoleNames.Customer)]
        [HttpPost]
        public ActionResult Checkout(Donhangtongquan dh)
        {
            if (User.Identity.IsAuthenticated)
            {
                var dhmodel = new DonHangKHModel();
                dhmodel.Luudonhang(dh, User.Identity.GetUserId().ToInt(), ManagerObiect.getIntance().giohang);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Checkout", "Home");
            }
        }

        public ActionResult MainMenu()
        {
            var mnmodel = new MainMenuModel();
            var menulist = mnmodel.GetMenuList();
            return PartialView("_MainMenuPartial", menulist);
        }

        public ActionResult SPNoiBat(int? skip)
        {
            var sp = new SanPhamModel();
            var skipnum = (skip ?? 0);
            var splist = sp.SPHot();
            splist = splist.OrderBy(r => r.Id).Skip(skipnum).Take(4);
            if (splist.Any())
                return PartialView("_ProductTabLoadMorePartial", splist);
            else
                return null;
        }

        public ActionResult SPMoiNhap(int? skip)
        {
            var sp = new SanPhamModel();
            var skipnum = (skip ?? 0);
            var splist = sp.SPMoiNhap();
            splist = splist.OrderBy(r => r.Id).Skip(skipnum).Take(4);
            if (splist.Any())
                return PartialView("_ProductTabLoadMorePartial", splist);
            else
                return null;
        }

        public ActionResult SPKhuyenMai(int? skip)
        {
            var sp = new SanPhamModel();
            var skipnum = (skip ?? 0);
            var splist = sp.SPKhuyenMai();
            splist = splist.OrderBy(r => r.Id).Skip(skipnum).Take(4);
            if (splist.Any())
                return PartialView("_ProductTabLoadMorePartial", splist);
            else
                return null;
        }

        public ActionResult SPBanChay()
        {
            var sp = new SanPhamModel();
            var splist = sp.SPBanChay(7);
            if (splist.Any())
                return PartialView("_BestSellerPartial", splist.ToList());
            else
                return null;
        }
        public ActionResult SPMoiXem()
        {
            return PartialView("_RecentlyViewPartial", ManagerObiect.getIntance().Laydanhsachsanphammoixem());
        }

    }
}