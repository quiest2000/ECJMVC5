using System;
using System.Data.Entity;
using System.Linq;
using ECommerce.Models.Domain;
using ECommerce.Models.Domain.EfModels;

namespace ECommerce.Models
{
    public class CommentModel
    {
        private MainContext db = new MainContext();

        public void AddComment(BinhLuan bl)
        {
            db.BinhLuans.Add(bl);
            db.SaveChanges();
        }

        internal IQueryable<BinhLuan> FindByMaSP(int masp)
        {
            return db.BinhLuans.Where(m => m.SanPhamId == masp && m.Parent == null);
        }

        internal void UpdateComment(BinhLuan Comment)
        {
            var cmpar = db.BinhLuans.Find(Comment.Parent);
            cmpar.DaTraLoi = "R";
            db.Entry(cmpar).State = EntityState.Modified;
            db.SaveChanges();
            var cm = db.BinhLuans.Find(Comment.MaBL);
            cm.DaTraLoi = "N";
            db.Entry(cm).State = EntityState.Modified;
            db.SaveChanges();
            var sendmail = new EmailTool();
            sendmail.SendMail(GetParent(Comment));
        }

        private EmailModel GetParent(BinhLuan Comment)
        {
            var par = db.BinhLuans.Find(Comment.Parent);
            var sub = "Bình luận sản phẩm " + par.SanPham.TenSP + " của bạn đã được phản hồi";
            var bo = "";
            if (!string.IsNullOrEmpty(par.KhachHangId))
            {
                bo += "Xin chào " + par.AspNetUser.UserName + ",\n";
                bo += "Bình luận sản phẩm " + par.SanPham.TenSP + " đã được phản hồi.\n";
                bo += "Bình luận của bạn: \"" + par.NoiDung + "\"\n";
                bo += "Phản hồi của chúng tôi: \"" + Comment.NoiDung + "\"\n";
                bo += "Xin cảm ơn.";
                var email = new EmailModel(par.AspNetUser.Email, sub, bo);
                return email;
            }
            bo += "Xin chào " + par.HoTen + ",\n";
            bo += "Bình luận sản phẩm " + par.SanPham.TenSP + " đã được phản hồi.\n";
            bo += "Bình luận của bạn: \"" + par.NoiDung + "\"\n";
            bo += "Phản hồi của chúng tôi: \"" + Comment.NoiDung + "\"\n";
            bo += "Xin cảm ơn.";
            var mail = new EmailModel(par.Email, sub, bo);
            return mail;

        }


        internal IQueryable<BinhLuan> TimBinhLuan(string key, DateTime? date, string status)
        {
            IQueryable<BinhLuan> lst = db.BinhLuans;
            if (!string.IsNullOrEmpty(key))
                lst = lst.Where(m => m.SanPham.TenSP.Contains(key));
            if (date != null)
            {
                lst = lst.Where(m => m.NgayDang.Value.Year == date.Value.Year && m.NgayDang.Value.Month == date.Value.Month && m.NgayDang.Value.Day == date.Value.Day);
            }
            if (!string.IsNullOrEmpty(status))
                lst = lst.Where(m => m.DaTraLoi == status);
            return lst;
        }

        internal void DeleteBinhLuan(int mabl)
        {
            var bl = db.BinhLuans.Find(mabl);
            if (bl == null) return;
            if (bl.DaTraLoi == "R")
            {
                var blchil = db.BinhLuans.Where(m => m.Parent == mabl);
                if (blchil.First() != null)
                {
                    BasicDel(blchil.First().MaBL);
                }
            }
            if (bl.DaTraLoi == "N")
            {
                var blpar = db.BinhLuans.Find(bl.Parent);
                blpar.DaTraLoi = "C";
                db.Entry(blpar).State = EntityState.Modified;
                db.SaveChanges();
            }
            BasicDel(mabl);
        }

        private void BasicDel(int mabl)
        {
            var bl = db.BinhLuans.Find(mabl);
            db.BinhLuans.Remove(bl);
            db.SaveChanges();
        }

        internal BinhLuan FindById(string id)
        {
            throw new NotImplementedException();
        }

        internal IQueryable<BinhLuan> FindChild(int mabl)
        {
            return db.BinhLuans.Where(m => m.Parent == mabl);
        }
    }
}