using System;
using System.Data.Entity;
using System.Linq;
using ECommerce.Models.Domain;
using ECommerce.Models.Domain.EfModels;

namespace ECommerce.Models.B2B
{
    public class NhaCungCapModel
    {
        protected MainContext db = new MainContext();


        internal IQueryable<NhaCungCap> TimNCC(string key)
        {
            IQueryable<NhaCungCap> lst = db.NhaCungCaps;
            if (!string.IsNullOrEmpty(key))
                lst = lst.Where(m => m.TenNCC.Contains(key));
            return lst;
        }

        internal object FindById(int id)
        {
            return db.HopDongNccs.Find(id);
        }

        internal void DeleteHopDong(int id)
        {
            var loai = db.HopDongNccs.Find(id);
            db.HopDongNccs.Remove(loai);
            db.SaveChanges();
        }

        internal int ThemHopDong(HopDongNcc hopDong)
        {
            //hopDong.Id = TaoMa();
            hopDong.TinhTrang = true;
            hopDong.TTThanhToan = true;
            db.HopDongNccs.Add(hopDong);
            db.SaveChanges();
            var sendmail = new EmailTool();
            sendmail.SendMail(GetParent(hopDong.Id));
            return hopDong.Id;
        }

        private EmailModel GetParent(int mahd)
        {
            var loai = db.HopDongNccs.FirstOrDefault(m => m.Id.Equals(mahd));
            var ncc = db.NhaCungCaps.FirstOrDefault(m => m.Id.Equals(loai.NccId));
            var sp = db.SanPhams.FirstOrDefault(m => m.Id.Equals(loai.SanPhamId));
            var mail = ncc.Email;
            var sub = "[Thông báo] Đã chấp nhận đăng ký cung cấp sản phẩm";
            var bo = "";
            bo += "Xin chào " + ncc.TenNCC + ",<br>";
            bo += "Chúng tôi rất vinh hạnh được hợp tác với các bạn với sản phẩm: " + sp.TenSP + ". Và sau đây là chi tiết hợp đồng:<br>";
            bo += "Mã hợp đồng: <strong>" + loai.Id + "</strong><br>";
            bo += "Ngày ký hợp đồng: <strong>" + loai.NgayKy + "</strong><br>";
            bo += "Thời hạn hợp đồng theo tháng: <strong>" + loai.ThoiHanHD + "</strong><br>";
            bo += "Sản phẩm trong hợp đồng: <strong>" + sp.TenSP + "</strong><br>";
            bo += "Số lượng tồn kho tối thiểu để cung cấp hàng: <strong>" + loai.SLToiThieu + "</strong><br>";
            bo += "Số lượng cần cung cấp: <strong>" + loai.SLCungCap + "</strong><br>";
            bo += "Số ngày giao kể từ ngày xác nhận giao hàng: <strong>" + loai.SoNgayGiao + "</strong><br>";
            bo += "Xin cảm ơn.";
            var email = new EmailModel(mail, sub, bo);
            return email;
        }

        //private string TaoMa()
        //{
        //    string maID;
        //    var rand = new Random();
        //    do
        //    {
        //        maID = "";
        //        for (var i = 0; i < 5; i++)
        //        {
        //            maID += rand.Next(9);
        //        }
        //    }
        //    while (!KiemtraID(maID));
        //    return maID;
        //}

        //private bool KiemtraID(string maID)
        //{
        //    var temp = db.HopDongNccs.Find(maID);
        //    if (temp == null)
        //        return true;
        //    return false;
        //}


        internal IQueryable<HopDongNcc> TimHopDong(int maHd, string tensp, bool? loaihd)
        {
            IQueryable<HopDongNcc> lst = db.HopDongNccs;
            if (maHd > 0)
                lst = lst.Where(m => m.Id == maHd);
            if (!string.IsNullOrEmpty(tensp))
                lst = lst.Where(m => m.SanPham.TenSP.Contains(tensp));
            if (loaihd != null)
                lst = lst.Where(m => m.IsBuy == loaihd);
            return lst;

        }

        internal void ThemNCC(Register2B2ViewModel model, int userId)
        {
            var ncc = new NhaCungCap();
            //ncc.Id = TaoMaNCC();
            ncc.TenNCC = model.TenNCC;
            ncc.NetUserId = userId.ToString();
            ncc.DiaChi = model.DiaChi;
            ncc.SDT_NCC = model.SDT_NCC;
            ncc.Email = model.Email;
            db.NhaCungCaps.Add(ncc);
            db.SaveChanges();
        }

        //private string TaoMaNCC()
        //{
        //    string maID;
        //    var rand = new Random();
        //    do
        //    {
        //        maID = "";
        //        for (var i = 0; i < 5; i++)
        //        {
        //            maID += rand.Next(9);
        //        }
        //    }
        //    while (!KiemtraIDNCC(maID));
        //    return maID;
        //}

        //private bool KiemtraIDNCC(string maID)
        //{
        //    var temp = db.NhaCungCaps.Find(maID);
        //    if (temp == null)
        //        return true;
        //    return false;
        //}

        internal NhaCungCap FindByNetUser(string p)
        {
            return db.NhaCungCaps.Where(m => m.NetUserId.Equals(p)).FirstOrDefault();
        }

        internal void UpdateInfo(EditInfo2B2ViewModel info)
        {
            var lsp = db.NhaCungCaps.Find(info.MaNCC);
            lsp.TenNCC = info.TenNCC;
            lsp.DiaChi = info.DiaChi;
            lsp.SDT_NCC = info.SDT_NCC;
            lsp.Email = info.Email;
            db.Entry(lsp).State = EntityState.Modified;
            db.SaveChanges();
        }

        internal IQueryable<HopDongNcc> TimHopDong(int maHd, string tensp, bool? loaihd, bool? tt)
        {
            IQueryable<HopDongNcc> lst = db.HopDongNccs;
            if (maHd>0)
                lst = lst.Where(m => m.Id==maHd);
            if (!string.IsNullOrEmpty(tensp))
                lst = lst.Where(m => m.SanPham.TenSP.Contains(tensp));
            if (loaihd != null)
                lst = lst.Where(m => m.IsBuy == loaihd);
            if (tt != null)
                lst = lst.Where(m => m.TinhTrang == tt);
            return lst;
        }

        internal void XacNhanDaGiao(int hopDongId, bool tt)
        {
            var lsp = db.HopDongNccs.Find(hopDongId);

            if (lsp.TinhTrang == false && tt == true)
            {
                var sp = new SanPhamModel();
                sp.UpdateSL(lsp.SanPhamId, lsp.SLCungCap, lsp.IsBuy);
            }

            lsp.TinhTrang = tt;
            db.Entry(lsp).State = EntityState.Modified;
            db.SaveChanges();

        }

        internal void XacNhanDaTT(int hopDongId, bool tt)
        {
            var lsp = db.HopDongNccs.Find(hopDongId);

            lsp.TTThanhToan = tt;
            db.Entry(lsp).State = EntityState.Modified;
            db.SaveChanges();

        }
        public bool Checkthanhtoan(int maHopDong)
        {
            var check = (from hopDong in db.HopDongNccs where hopDong.Id == maHopDong select new { tt = hopDong.TTThanhToan }).FirstOrDefault();
            return (bool)check.tt;
        }

    }
}