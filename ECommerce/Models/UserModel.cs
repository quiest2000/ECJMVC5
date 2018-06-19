using System.Data.Entity;
using System.Linq;
using ECommerce.Models.Domain;
using ECommerce.Models.Domain.EfModels;
using ECommerce.Utils;

namespace ECommerce.Models
{
    public class UserModel
    {
        MainContext db = new MainContext();
        internal AspNetUser FindById(int id)
        {
            return db.AspNetUsers.Find(id);
        }

        internal void UpdateInfo(EditInfoModel info, int id)
        {
            var user = new AspNetUser();

            user = db.AspNetUsers.Find(id);
            if (user != null)
            {
                user.Email = info.Email;
                user.PhoneNumber = info.DienThoai;
                user.CMND = info.CMND;
                user.HoTen = info.HoTen;
                user.NgaySinh = info.NgaySinh;
                user.GioiTinh = info.GioiTinh;
                user.DiaChi = info.DiaChi;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        internal void UpdateImage(int id)
        {
            var user = new AspNetUser();
            user = db.AspNetUsers.Find(id);
            if (user != null)
            {
                user.Avatar = id + ".jpg";
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        internal IQueryable<AspNetUser> SearchUser(string key, string email, string hoten, string phone, string quyen)
        {
            var iQuyen = quyen.ToInt();
            IQueryable<AspNetUser> lst = db.AspNetUsers;
            if (!string.IsNullOrEmpty(key))
                lst = lst.Where(m => m.UserName.Contains(key));
            if (!string.IsNullOrEmpty(email))
                lst = lst.Where(m => m.Email.Contains(email));
            if (!string.IsNullOrEmpty(hoten))
                lst = lst.Where(m => m.HoTen.Contains(hoten));
            if (!string.IsNullOrEmpty(phone))
                lst = lst.Where(m => m.PhoneNumber.Contains(phone));
            if (!string.IsNullOrEmpty(quyen))
                lst = lst.Where(m => m.AspNetRoles.FirstOrDefault().Id==(iQuyen));
            return lst;
        }

        internal IQueryable<AspNetRole> GetAllRole()
        {
            return db.AspNetRoles;
        }

        internal void deleleallrole(int userId)
        {
            var user = db.AspNetUsers.Find(userId);
            db.Entry(user).Collection("AspNetRoles").Load();
            user.AspNetRoles.Remove(user.AspNetRoles.FirstOrDefault());
            db.SaveChanges();
        }


        internal bool ConfirmMail(int id)
        {
            var user = new AspNetUser();

            user = db.AspNetUsers.Find(id);
            if (user != null)
            {
                user.EmailConfirmed = true;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        internal void SendMailConfirm(int userId, string url)
        {
            var sendmail = new EmailTool();
            var us = db.AspNetUsers.Find(userId);
            if (us != null)
            {
                var mail = us.Email;
                var sub = "[Xác nhận email] Xác nhận đăng ký tại TMDT_J shop";
                var bo = "";
                bo += "Xin chào " + us.HoTen + ",<br>";
                bo += "Cảm ơn bạn đã đăng ký tịa TMDT_Shop, đây là link xác nhận email của bạn <br>";
                bo += "Click vào link bên dưới để xác nhận:<br>";
                bo += "<a href=\"" + url + "\">" + url + "</a><br>";
                bo += "Xin cảm ơn.";
                sendmail.SendMail(new EmailModel(mail, sub, bo));
            }
        }
    }
}