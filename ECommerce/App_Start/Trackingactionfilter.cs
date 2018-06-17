using System;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Models.Domain.EfModels;

namespace ECommerce
{
    public class Trackingactionfilter : ActionFilterAttribute
    {
        public string maSP { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters.ContainsKey("id"))
            {
                int.TryParse(filterContext.ActionParameters["id"].ToString(), out var maSP);
                var temp = generateTracking(filterContext, maSP);
                // them san pham da xem
                var sp = new SanPhamModel();
                ManagerObiect.getIntance().Themsanphammoixem(sp.getSanPham(maSP));
                ManagerObiect.getIntance().SaveTrackingLog(temp);
            }
        }

        private TrackingAction generateTracking(ActionExecutingContext filterContext, int maSP)
        {
            var temp = new TrackingAction()
            {
                Username = (ManagerObiect.getIntance().userName ?? " "),
                SanPhamId = maSP,
                Ngaythuchien = DateTime.Now,
                Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                Action = filterContext.ActionDescriptor.ActionName
            };
            return temp;
        }

    }
}