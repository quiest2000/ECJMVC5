using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Models.Domain;
using ECommerce.Models.Domain.EfModels;

namespace ECommerce.Models
{
    public class ManagerObiect
    {
        private static ManagerObiect manager;
        public Giohang giohang = new Giohang();
        public static string Token ="";
        public static int DoitacID;
        private List<SanPham> sanPhamMoiXem = new List<SanPham>();
        public static ConfigAPI configAPI = null;
        public static string consumer_key;
        public static string reDirectUrl;
        public string userName { get; set; }
        public static ManagerObiect getIntance()
        {
            if(manager == null)
            {
                manager = new ManagerObiect();
            }
            return manager;
        }
        public void saveCarttoCookie(HttpResponseBase reponse,Giohang gh)
        {

        }
        public void LoadCartfromCookie(HttpRequestBase request)
        {
            var cookie = request.Cookies["Cart"];
            if(cookie == null)
            {
                giohang = new Giohang();
                return;
            }
            

        }
        public void SaveTrackingLog(TrackingAction a)
        {
            using (var db = new MainContext())
            {
                db.Trackingactions.Add(a);
                db.SaveChanges();
            }
        }
        public void Themsanphammoixem(SanPham a)
        {
            var count = sanPhamMoiXem.Count(m => m.Id == a.Id);
            if (count == 0)
                sanPhamMoiXem.Add(a);
        }
        public List<SanPham> Laydanhsachsanphammoixem()
        {
            return sanPhamMoiXem;
        }
        public static string GetBaseUrl(string url)
        {
            url = url.Replace("//","*");
            var ds = url.Split('/');
            url = ds[0].Replace("*","//");
            return url;
        }
    }
}