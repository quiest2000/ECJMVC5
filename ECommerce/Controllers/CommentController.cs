using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Models.Domain.EfModels;
using ECommerce.Utils;
using Microsoft.AspNet.Identity;
using PagedList;

namespace ECommerce.Controllers
{
    public class CommentController : Controller
    {
        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult Index()
        {
            return View();
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult TimBinhLuan(string key, DateTime? date, string status, int? page)
        {
            var spm = new CommentModel();
            ViewBag.key = key;
            ViewBag.date = date;
            ViewBag.status = status;
            return PhanTrangBL(spm.TimBinhLuan(key, date, status), page, null);
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult PhanTrangBL(IQueryable<BinhLuan> lst, int? page, int? pagesize)
        {
            var pageSize = (pagesize ?? 10);
            var pageNumber = (page ?? 1);
            return PartialView("BinhLuanPartial", lst.OrderByDescending(m => m.NgayDang).ToPagedList(pageNumber, pageSize));
        }

        [AllowAnonymous]
        // GET: Comment
        //Tai danh sach binh luan
        public ActionResult LoadComment(int masp, int? page)
        {
            var cm = new CommentModel();
            //cai dat phan trang
            //So san pham tren 1 trang
            var pageSize = 10;
            //So trang
            var pageNumber = (page ?? 1);
            ViewBag.masp = masp;
            return PartialView("_CommentListPartial", cm.FindByMaSP(masp).OrderByDescending(m => m.NgayDang).ToPagedList(pageNumber, pageSize));
        }

        [AllowAnonymous]
        public ActionResult ChilComment(int mabl)
        {
            var cm = new CommentModel();
            return PartialView("_ChilComment",cm.FindChild(mabl));
        }

        [AllowAnonymous]
        //Them binh luan moi sau khi nhan nut gui binh luan se chay ham nay
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(BinhLuan Comment)
        {
            Comment.NgayDang = DateTime.Now;
            Comment.KhachHangId = User.Identity.GetUserId().ToInt();
            Comment.DaTraLoi = "C";
            var cm = new CommentModel();
            cm.AddComment(Comment);
            return RedirectToAction("LoadComment", new { masp = Comment.SanPhamId });
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRepl(BinhLuan Comment)
        {
            Comment.NgayDang = DateTime.Now;
            Comment.KhachHangId = User.Identity.GetUserId().ToInt();
            var cm = new CommentModel();
            cm.AddComment(Comment);
            cm.UpdateComment(Comment);
            return RedirectToAction("TimBinhLuan");
        }

        [AllowAnonymous]
        //Hien thi form de binh luan co tham so la masp va makh
        public ActionResult AddComment(int masp)
        {
            ViewBag.masp = masp;

            var userid = User.Identity.GetUserId();
            if (userid != null)
            {
                var us = new UserModel();
                var user = us.FindById(userid.ToInt());
                ViewBag.Name = user.UserName;
            }
            return PartialView("_CommentFormPartial");
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult AddRepl(int masp, int parent)
        {
            var bl = new BinhLuan();
            bl.SanPhamId = masp;
            bl.Parent = parent;
            return View("RepComment", bl);
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult DeleteBinhLuan(int id)
        {
            var cm = new CommentModel();
            cm.DeleteBinhLuan(id);
            return RedirectToAction("TimBinhLuan");
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        [HttpPost]
        public ActionResult MultibleDel(List<int> lstdel)
        {
            foreach (var item in lstdel)
            {
                var spm = new CommentModel();
                spm.DeleteBinhLuan(item);
            }
            return RedirectToAction("TimBinhLuan");
        }

    }
}