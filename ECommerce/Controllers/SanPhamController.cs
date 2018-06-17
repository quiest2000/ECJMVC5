using System.Linq;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Models.Domain;

namespace ECommerce.Controllers
{
    public class SanPhamController : Controller
    {
        private MainContext db = new MainContext();
        private SanPhamModel sp = new SanPhamModel();
        // GET: SanPham
        [Trackingactionfilter]
        public ActionResult Index(string id)
        {
            var sp = db.SanPhams.Find(id);
            return View("ProductDetail",sp);
        }
        public ActionResult SearchByName(string tensp)
        {
            var splist = db.SanPhams.Where(u => u.TenSP.Contains(tensp));
            ViewBag.CurrentFilter = tensp;
            splist = splist.OrderByDescending(u => u.TenSP);
            return View(splist);
        }

        public ActionResult SearchByType(string MaLoai)
        {
            var splist = (from p in db.SanPhams where p.LoaiSpId.Equals(MaLoai) select p);
            return View(splist);
        }
        public ActionResult Loadsplienquan(string maloai,int sl)
        {
            var splist = sp.SearchByType(maloai);
            splist = splist.Take(sl);
            return PartialView("_PartialSanPhamLienQuan", splist);
        }
        
        public ActionResult ThongSoKyThuat(int maSp)
        {
            var spm = new SanPhamModel();
            return PartialView("ThongSoKyThuatPartial", spm.GetTSKT(maSp));
        }

    }
}