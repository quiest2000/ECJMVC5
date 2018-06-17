using System.Linq;
using ECommerce.Models.Domain;

namespace ECommerce.Models.B2B
{
    public class DSDangKyModel
    {
        private MainContext db = new MainContext();


        internal IQueryable<Domain.EfModels.DangKySanPhamNcc> TimDS(string tensp, string tenncc, int? tt)
        {
            IQueryable<Domain.EfModels.DangKySanPhamNcc> lst = db.DanhSachDangkySanPhamNccs;
            if (!string.IsNullOrEmpty(tensp))
                lst = lst.Where(m => m.SanPhamCanMua.SanPham.TenSP.Contains(tensp));
            if (!string.IsNullOrEmpty(tenncc))
                lst = lst.Where(m => m.NhaCungCap.TenNCC.Contains(tenncc));
            if (tt != null)
                lst = lst.Where(m => m.Trangthai == tt);
            return lst;
        }

        internal object Findbyid(int id)
        {
            return db.DanhSachDangkySanPhamNccs.Find(id);
        }

        internal void DeleteDSDK(int id)
        {
            var loai = db.DanhSachDangkySanPhamNccs.Find(id);
            db.DanhSachDangkySanPhamNccs.Remove(loai);
            db.SaveChanges();
        }
    }
}