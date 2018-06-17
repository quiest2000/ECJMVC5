using System.Linq;
using System.Web.Mvc;
using ECommerce.Models;
using ECommerce.Models.Domain.EfModels;
using PagedList;

namespace ECommerce.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult SearchForm()
        {
            return PartialView("_SearchFormPartial");
        }
        public ActionResult CategoryList()
        {
            var cat = new CategoryModel();
            var lst = cat.GetCategory().ToList();
            return PartialView("_CategoryListPartial", lst);
        }

        [HttpPost]
        public ActionResult SearchByName(string term)
        {
            var sp = new SanPhamModel();
            var lst = sp.SearchByName(term);
            var splist = (from p in lst orderby p.Id descending select new { MaSP = p.Id, p.TenSP, p.GiaTien, p.AnhDaiDien }).Take(5);
            return Json(splist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AdvancedSearchView(string term, string loai, string hangsx, int? minprice, int? maxprice)
        {
            ViewBag.Name = term;
            ViewBag.loai = loai;
            ViewBag.hangsx = hangsx;
            ViewBag.minprice = minprice;
            ViewBag.maxprice = maxprice;
            return View("AdvancedSearchView");
        }

        public ActionResult AdvancedSearchP(string term, string loai, string hangsx, string typeview, int? page, int? minprice, int? maxprice)
        {
            ViewBag.Name = term;
            ViewBag.loai = loai;
            ViewBag.hangsx = hangsx;
            ViewBag.minprice = minprice;
            ViewBag.maxprice = maxprice;
            ViewBag.type = typeview;
            var sp = new SanPhamModel();
            var lst = sp.AdvancedSearch(term, loai, hangsx, minprice, maxprice);
            return PhanTrangAdvanced(lst, page);
        }

        private ActionResult PhanTrangAdvanced(IQueryable<SanPham> lst, int? page)
        {
            var pageSize = 6;
            var pageNumber = (page ?? 1);
            lst = lst.OrderByDescending(m => m.Id);
            return View("_AdvancedSearchPartial", lst.ToPagedList(pageNumber, pageSize));
        }
    }
}