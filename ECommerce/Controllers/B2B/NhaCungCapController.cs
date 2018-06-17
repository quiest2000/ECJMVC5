using System.Linq;
using System.Web.Mvc;
using ECommerce.Models.B2B;
using ECommerce.Models.Domain.EfModels;
using PagedList;

namespace ECommerce.Controllers.B2B
{
    public class NhaCungCapController : Controller
    {
        //
        // nha cung cap
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SearchByName(string term)
        {
            var sp = new NhaCungCapModel();
            var lst = sp.TimNCC(term);
            var splist = (from p in lst orderby p.TenNCC descending select new { MaNCC = p.Id, p.TenNCC }).Take(5);
            return Json(splist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TimNCC(string key, int? page)
        {
            var ncc = new NhaCungCapModel();
            ViewBag.key = key;
            return PhanTrangNCC(ncc.TimNCC(key).Where(m =>!string.IsNullOrEmpty(m.NetUserId)), page, null);
        }

        public ActionResult PhanTrangNCC(IQueryable<NhaCungCap> lst, int? page, int? pagesize)
        {
            var pageSize = (pagesize ?? 10);
            var pageNumber = (page ?? 1);
            return PartialView("NCCPartial", lst.OrderBy(m => m.TenNCC).ToPagedList(pageNumber, pageSize));
        }


    }
}