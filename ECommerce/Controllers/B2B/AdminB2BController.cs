using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Models.B2B;
using ECommerce.Models.Constants;
using ECommerce.Models.Domain.EfModels;
using PagedList;
using HopDongNcc = ECommerce.Models.Domain.EfModels.HopDongNcc;

namespace ECommerce.Controllers.B2B
{
    public class AdminB2BController : Controller
    {
        // GET: AdminB2B
        public ActionResult Taohopdong()
        {
            var Ncc = new HopdongNCCModel();
            ViewBag.TenNCC = new SelectList(Ncc.getDsNhaCC(), "NhaCungCapId", "TenNcc");
            ViewBag.MaSP = new SelectList(Ncc.getDsSanPham(), "Id", "TenSP");
            return View();
        }
        [HttpPost]
        public ActionResult Taohopdong(HopDongNcc hopDong)
        {
            var Ncc = new HopdongNCCModel();
            if (ModelState.IsValid)
            {
                string MaHD;
                if(Ncc.ThemmoiHopDongNCC(hopDong) != 0)
                {
                    var a1 = new ConfigAPI();
                    a1.NhaCungCapId = hopDong.NccId;
                    return View("ConfigAPI", a1);
                }
            }
            
            ViewBag.TenNCC = new SelectList(Ncc.getDsNhaCC(), "NhaCungCapId", "TenNcc");
            ViewBag.MaSP = new SelectList(Ncc.getDsSanPham(), "Id", "TenSP");
            return View(hopDong);
        }
        // nhan call back trả về
        public ActionResult NhanCallback(string verifier_token, string request_token)
        {
            return RedirectToAction("GetTokenRequest", new { verifier_token = verifier_token, request_token = request_token });
        }
        public ActionResult ConfigAPI(int MaNCC)
        {
            var a1 = new ConfigAPI();
            a1.NhaCungCapId = MaNCC;
            return View(a1);
        }
        [HttpPost]
        public ActionResult ConfigAPI(ConfigAPI a)
        {
            var model = new ConfigAPIModel();
            if(model.ThemmoiConfig(a))
            {
                return RedirectToAction("GetNhaDoiTac");
            }
            return View(a);
        }
        // luu consumer_key
        public JsonResult Saveconsumer_key(string consumer_key)
        {
            ManagerObiect.consumer_key = consumer_key;
            return Json("success");
        }
        public ActionResult GetTokenRequest(string verifier_token, string request_token)
        {
            if (verifier_token != null)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                    );
                    var model = new
                    {
                        consumer_key = ManagerObiect.consumer_key,
                        verifier_token = verifier_token,
                        request_token = request_token
                    };
                    var response = client.PostAsJsonAsync(
                        ManagerObiect.configAPI.LinkAccessToken,
                        model
                    ).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var value = response.Content.ReadAsAsync<Token>().Result;
                        ManagerObiect.Token = value.access_token;
                    }
                    else
                        ViewBag.thongbao = response.StatusCode.ToString();
                    return Redirect(ManagerObiect.reDirectUrl);
                }
            }
            return RedirectToAction("","");
        }
        //xu li get orders
        [HttpPost]
        [AuthLog(Roles = RoleNames.Administrator+","+RoleNames.Employee)]
        public ActionResult GetOrders(string user, string pass, string supplier_key)
        {
            if(ManagerObiect.DoitacID != null)
            {
                using(var client = new HttpClient())
                {
                    var authInfo = user + ":" + pass;
                    authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);

                    //client.BaseAddress = new Uri(ManagerObiect.GetBaseUrl(ManagerObiect.configAPI.LinkAccessToken));

                    var response = client.GetAsync(ManagerObiect.configAPI.LinkKiemTraLuongTon + supplier_key).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var code = response.StatusCode.ToString();
                        // Parse the response body. Blocking!
                        var data = response.Content.ReadAsAsync<List<HopDong>>().Result;
                        return View(data);
                    }
                }
            }
            return Content("Error");
        }
        [HttpPost]
        [AuthLog(Roles = RoleNames.Administrator+","+RoleNames.Employee)]
        public ActionResult GetOrdersOauth(string access_token,string supplier_key)
        {
            if (ManagerObiect.DoitacID != null)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", access_token);

                    var response = client.GetAsync(ManagerObiect.configAPI.LinkKiemTraLuongTon + supplier_key).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var code = response.StatusCode.ToString();
                        // Parse the response body. Blocking!
                        var data = response.Content.ReadAsAsync<List<HopDong>>().Result;
                        return View("GetOrders", data);
                    }
                }
            }
            ManagerObiect.Token = "";
            return Content("Error");
        }
        public ActionResult Laysoluongton(int doiacID)
        {
            ManagerObiect.DoitacID = doiacID;
            ManagerObiect.configAPI = new ConfigAPIModel().getConfig(doiacID);
            if (ManagerObiect.configAPI == null)
            {
                return RedirectToAction("ConfigAPI", new { MaNCC = doiacID });
            }
            ViewBag.doitac = doiacID;
            return View();
        }
        public ActionResult GetAccesstoken(string redirectUrl)
        {
            if(ManagerObiect.configAPI != null)
            {
                ManagerObiect.reDirectUrl = redirectUrl;
                ViewBag.action = ManagerObiect.configAPI.LinkRequesrToken;
                return View();
            }
            return RedirectToAction("", "");
        }
        [AuthLog(Roles = RoleNames.Administrator+","+RoleNames.Employee)]
        public ActionResult GetDanhsachNhaDoiTac()
        {
            return View();
        }
        // get danh sach các Nhà đối tác.
        [AuthLog(Roles = RoleNames.Administrator+","+RoleNames.Employee)]
        public ActionResult GetNhaDoiTac()
        {
            return View();
        }
        [AuthLog(Roles = RoleNames.Administrator+","+RoleNames.Employee)]
        public ActionResult TimDoiTac(string key, int? page)
        {
            var model = new DoiTacModel();
            ViewBag.key = key;
            return PhanTrangDoitac(model.LayDoitac(), page, null);
        }
        [AuthLog(Roles = RoleNames.Administrator+","+RoleNames.Employee)]
        public ActionResult PhanTrangDoitac(List<NhaCungCap> lst, int? page, int? pagesize)
        {
            var pageSize = (pagesize ?? 10);
            var pageNumber = (page ?? 1);
            return PartialView("DoitacPartial", lst.OrderBy(m => m.TenNCC).ToPagedList(pageNumber, pageSize));
        }
        // Xác nhận giao hàng
        public ActionResult XemXacNhanGiaoHang(int doiTacID)
        {
            ManagerObiect.DoitacID = doiTacID;
            ManagerObiect.configAPI = new ConfigAPIModel().getConfig(doiTacID);
            if (ManagerObiect.configAPI == null)
            {
                return RedirectToAction("ConfigAPI", new { MaNCC = doiTacID });
            }
            var model = new HopdongNCCModel();
            ViewBag.doitac = doiTacID;
            ViewBag.HD = new SelectList(model.getMaHD(doiTacID), "Mahd", "Mahd");
            return View();
        }
        //
        public ActionResult BasicParitial()
        {
            return View();
        }
        public ActionResult BasicOauthParitial()
        {
            return View();
        }
        // 
        [HttpPost]
        public ActionResult XemXacNhanGiaoHang(HopDong hd, string access_token, string username, string password, int doitacId)
        {
            var modelNCC = new NhaCungCapModel();
            var modelhd = new HopdongNCCModel();
            ViewBag.doitac = doitacId;
            ViewBag.HD = new SelectList(modelhd.getMaHD(doitacId), "Mahd", "Mahd");
            if(!modelNCC.Checkthanhtoan(hd.order_id))
            {
                ModelState.AddModelError("", "Hợp đồng này lúc trước chưa thanh toán");
                return View(hd);
            }
            
            if (string.IsNullOrEmpty(access_token))
            {
                if ((username != "" && password != "") && (username != null && password!=null))
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json")
                        );
                        var authInfo = username + ":" + password;
                        authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
                        var model = new
                        {
                            supplier_key = hd.supplier_key,
                            order_id = hd.order_id,
                            product_id = hd.product_id,
                            product_quantity = hd.product_quantity,
                            product_date = hd.product_date
                        };
                        var response = client.PostAsJsonAsync(
                            ManagerObiect.configAPI.LinkXacNhanGiaoHang,
                            model
                        ).Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            ModelState.AddModelError("", "Đã ghi nhận thành công");
                            modelhd.SetXacnhangiaohang(hd.order_id);
                        }
                        else
                        {
                            ViewBag.thongbao = "Thất bại " + response.Content.ReadAsStringAsync().Result;
                            ModelState.AddModelError("", ViewBag.thongbao);
                        }
                    }
                }
                else
                {
                    ViewBag.thongbao = "Thông tin nhập không chính xác";
                    return View(hd);
                }
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                    );
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", access_token);
                    var model = new
                    {
                        supplier_key = hd.supplier_key,
                        order_id = hd.order_id,
                        product_id = hd.product_id,
                        product_quantity = hd.product_quantity,
                        product_date = hd.product_date
                    };
                    var response = client.PostAsJsonAsync(
                        ManagerObiect.configAPI.LinkXacNhanGiaoHang,
                        model
                    ).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ModelState.AddModelError("", "Đã ghi nhận thành công");
                        modelhd.SetXacnhangiaohang(hd.order_id);
                    }
                    else
                    {
                        ViewBag.thongbao = "Thất bại " + response.Content.ReadAsStringAsync().Result;
                        ModelState.AddModelError("", ViewBag.thongbao);
                    }
                }
            }
            return View(hd);
        }
        public JsonResult GetMaspFromMahd(int maHd)
        {
            var model = new HopdongNCCModel();
            var masp = model.GetMaSP(maHd);
            return Json(masp, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Theodoithanhtoan()
        {
            return View();
        }
    }
}