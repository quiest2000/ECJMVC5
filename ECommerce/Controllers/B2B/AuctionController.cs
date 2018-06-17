using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Models.B2B;
using ECommerce.Models.Constants;
using ECommerce.Models.Domain.EfModels;

namespace ECommerce.Controllers.B2B
{
    public class AuctionController : Controller
    {
        // GET: Auction
        [AuthLog(Roles = RoleNames.Administrator + "," + RoleNames.Employee + "," + RoleNames.Vendor)]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Danhsachcanmua()
        {
            var sp = new SanPhamCanMuaModel();
            var temp = sp.getDS(0, 5);
            return View(temp);
        }
        public ActionResult ProductFilterB2B()
        {
            var cat = new CategoryModel();
            return View(cat.GetCategory());
        }
        public ActionResult ChitietAuction(SanPhamCanMua a)
        {
            var sp = new SanPhamCanMuaModel();
            a.SanPham = sp.getSP(a.SanPhamId);
            return View(a);
        }
        public ActionResult ChitietSanphamAuction(int spcm)
        {
            var spmodel = new SanPhamCanMuaModel();
            var temp = spmodel.getSanphamcanmua(spcm);
            temp.SanPham = spmodel.getSP(temp.SanPhamId);
            return View(temp);
        }
        public ActionResult ModalRegister(int ID)
        {
            var model = new DangKySanPhamModel();
            var temp = model.createDangKiSanPham(User.Identity.Name, ID);
            return View(temp);
        }
        [HttpPost]
        public ActionResult RegisterProduct(DangKySanPhamModel a)
        {
            if (new DangKySanPhamModel().ThemDanhKi(a))
            {
                Session["TB"] = true;
            }
            else
            {
                Session["TB"] = false;
            }

            return RedirectToAction("ChitietSanphamAuction", new { spcm = a.MaSPCM });
        }
        public ActionResult DanhsachNCCDK(int IDSPCM)
        {
            var ds = new DangKySanPhamModel().getDSDKNCC(IDSPCM);
            return View(ds);
        }
        public ActionResult Hopdong()
        {
            return View();
        }
    }
}