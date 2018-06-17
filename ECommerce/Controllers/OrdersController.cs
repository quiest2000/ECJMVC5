using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ECommerce.Models;
using ECommerce.Models.Domain;

namespace ECommerce.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrdersController : ApiController
    {
        private MainContext db = new MainContext();

        // GET api/Orders
        //[HttpGet]
        //[Route("api/orders")]
        //[Authorize]
        //public HttpResponseMessage GetHopDongNCCs(string supplier_key)
        //{
        //    try
        //    {
        //            var maNcc = from p in db.Oauths where p.Consumer_key == supplier_key select new { NhaCungCapId = p.NhaCungCapId };
        //            string ma = maNcc.ToList()[0].NhaCungCapId;
        //            var listofHD = (from p in db.HopDongNccs where p.NhaCungCapId == ma select new { order_id = p.Id, product_id = p.Id, product_name = p.SanPham.TenSP, product_quantity = p.SanPham.SoLuong });
        //            return Request.CreateResponse(HttpStatusCode.OK, listofHD.ToList());
                
        //    }
        //    catch (Exception e) { return Request.CreateResponse(HttpStatusCode.NotFound); }
        //}

        [HttpGet]
        [Route("api/orders/{supplier_key}")]
        [IdentityAuthentication(true)] // enable basic for this action
        public HttpResponseMessage GetHopDongNCC(string supplier_key)
        {
            try
            {
                    var maNcc = from p in db.Oauths where p.Consumer_key == supplier_key select new { MaNCC = p.NccId };
                    var ma = maNcc.ToList()[0].MaNCC;
                    var listofHD = (from p in db.HopDongNccs where p.NccId == ma select new { order_id = p.Id, product_id = p.SanPhamId, product_name = p.SanPham.TenSP, product_quantity = p.SanPham.SoLuong });
                    return Request.CreateResponse(HttpStatusCode.OK, listofHD.ToList());

            }
            catch (Exception e) { return Request.CreateResponse(HttpStatusCode.NotFound); }
        }

        
        [HttpPost]
        [Route("api/orders/start_shipping")]
        [IdentityAuthentication(true)]
        public HttpResponseMessage Xacnhangiaohang([FromBody]Shipping param)
        {
            try
            {
                var maNcc = from p in db.Oauths where p.Consumer_key == param.supplier_key select new { MaNCC = p.NccId };
                var MaNCC = maNcc.ToList()[0].MaNCC;
                var hopdong = db.HopDongNccs.FirstOrDefault(m => m.Id == param.order_id & m.NccId == MaNCC & m.SanPhamId == param.product_id);
                if(hopdong == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound,"Không tìm thấy dữ liệu");
                }
                hopdong.SLCungCap = param.product_quantity;
                hopdong.TGGiaoHang = DateTime.Parse(param.product_date);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
    }
}