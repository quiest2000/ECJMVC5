﻿using EC_TH2012_J.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EC_TH2012_J.Models.Domain.EfModels;

namespace EC_TH2012_J.App_Start
{
    public class Trackingactionfilter : ActionFilterAttribute
    {
        public string maSP { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters.ContainsKey("id"))
            {
                var maSP = filterContext.ActionParameters["id"].ToString();
                var temp = generateTracking(filterContext,maSP);
                // them san pham da xem
                var sp = new SanPhamModel();
                ManagerObiect.getIntance().Themsanphammoixem(sp.getSanPham(maSP));
                ManagerObiect.getIntance().SaveTrackingLog(temp);
            }
        }

        private Trackingaction generateTracking(ActionExecutingContext filterContext, string maSP)
        {
            var temp = new Trackingaction()
            {
                Username = (ManagerObiect.getIntance().userName ?? " "),
                MaSP = maSP,
                Ngaythuchien = DateTime.Now,
                Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                Action = filterContext.ActionDescriptor.ActionName
            };
            return temp;
        }

    }
}