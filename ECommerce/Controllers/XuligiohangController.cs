﻿using System;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Models.Domain;

namespace ECommerce.Controllers
{
    public class XuligiohangController : Controller
    {
        private static MainContext db = new MainContext();
        public ActionResult Addcart(int sp, int quantity)
        {
            try
            {
                var temp = db.SanPhams.Find(sp);
                var index = Kiemtratontai(sp);
                if(index == -1)
                {
                    var tam = new ChiTietGioHang();
                    tam.sanPham = temp;
                    tam.Soluong = quantity;
                    ManagerObiect.getIntance().giohang.addCart(tam);
                }
                else
                {
                    ManagerObiect.getIntance().giohang.getGiohang()[index].Soluong += quantity;
                }
                return PartialView("Addcart1", ManagerObiect.getIntance().giohang);
            }
            catch (Exception e) {return Json("faill"); }
            
        }
        public int Kiemtratontai(int sanPhamId)
        {
            for (var i = 0; i < ManagerObiect.getIntance().giohang.getGiohang().Count; i++)
            {
                if (ManagerObiect.getIntance().giohang.getGiohang()[i].sanPham.Id == sanPhamId)
                    return i;
            }
            return -1;
        }
        // GET: Xuligiohang
        public ActionResult Xoagiohang(int index)
        {
            ManagerObiect.getIntance().giohang.removeCart(index);
            return RedirectToAction("basicXuLiGiohang");
        }
        public ActionResult Thaydoisoluong(int index,string value)
        {
            ManagerObiect.getIntance().giohang.Changequanlity(index, value);
            return RedirectToAction("basicXuLiGiohang");
        }
        
        public ActionResult basicXuLiGiohang()
        {
            return PartialView("basicXuLiGiohang", ManagerObiect.getIntance().giohang);
        }
        public ActionResult UpdategiohangContent()
        {
            return PartialView("Addcart1", ManagerObiect.getIntance().giohang);
        }
        public ActionResult CartTitle()
        {
            return PartialView("Addcart1",ManagerObiect.getIntance().giohang);
        }
        public ActionResult CartOrder()
        {
            return PartialView("Ordercheckout", ManagerObiect.getIntance().giohang);
        }
    }
}