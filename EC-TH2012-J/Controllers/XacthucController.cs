using EC_TH2012_J.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EC_TH2012_J.Models.Domain;

namespace EC_TH2012_J.Controllers
{
    public class XacthucController : Controller
    {
        private static string Request_token;
        private static MainContext db = new MainContext();
        // GET: Xacthuc
        public ActionResult authenticate()
        {
            ViewBag.name = User.Identity.Name;
            return View();
        }
        [HttpPost]
        public ActionResult authenticate(string btn)
        {
            try
            {
                var temp = db.Oauths.Where(m => m.Request_token == Request_token).FirstOrDefault();
                if (btn == "Không đồng ý")
                {
                    return Redirect(temp.Callback);
                }
                else
                {
                    var url = temp.Callback;
                    var ver = create_verifier(Request_token);
                    url += "?verifier_token=" + ver + "&request_token=" + Request_token;
                    temp.Verifier_token = ver;
                    db.SaveChanges();
                    return Redirect(url);
                }
            }
            catch (Exception e) { return RedirectToAction("Index","Home"); }
        }

        private string create_verifier(string Request_token)
        {
            var rand = new Random();
            var username = User.Identity.Name;
            var verifier = Request_token;
            for(var i = 0 ; i < username.Length ; i++)
            {
                var index = rand.Next() % username.Length;
                verifier += username[index];
            }
            for(var i = 0 ; i < 5 ; i++)
            {
                var index = rand.Next() % 10;
                verifier += index.ToString();
            }
            
            return verifier;
        }
        public ActionResult Kiemtra(string id)
        {
            Request_token = id;
            db = new MainContext();
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("authenticate", "Xacthuc");
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("authenticate","Xacthuc") });
            }
        }
    }
}