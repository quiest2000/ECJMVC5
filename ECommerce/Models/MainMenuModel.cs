using System.Collections.Generic;
using System.Linq;
using ECommerce.Models.Domain;
using ECommerce.Models.Domain.EfModels;

namespace ECommerce.Models
{
    public class MainMenuModel
    {
        private MainContext db = new MainContext();

        public List<Link> GetMenuLink()
        {
            return db.Links.Where(m => m.Group.Equals("MainMenu")).ToList();
        }
        public List<MenuItem> GetMenuList() 
        { 
            var mnlist = new List<MenuItem>();
            var loaiSPlst = db.LoaiSPs.OrderBy(a => a.Id).Where(a => !a.Id.Equals("NOTTT")).ToList();
            foreach (var item in loaiSPlst)
            {
                var mnitem = new MenuItem();
                mnitem.LoaiSP = item;
                mnitem.HangSX = this.GetHangSXLst(item.Id);
                mnlist.Add(mnitem);
            }
            return mnlist;
        }

        private List<HangSanXuat> GetHangSXLst(int maloai)
        {
            var hsxlist = (from p in db.SanPhams where p.LoaiSpId == maloai select p.HangSanXuat).Distinct().ToList();
            return hsxlist;
        }
    }

    public class MenuItem
    {
        public LoaiSanPham LoaiSP { get; set; }
        public List<HangSanXuat> HangSX { get; set; }
    }
}

