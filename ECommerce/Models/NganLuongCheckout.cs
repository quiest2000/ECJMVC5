using System;
using System.Collections;
using System.Web;

namespace ECommerce.Models
{
    public class NganLuongCheckout 
    { 
        private string nganluong_url = "http://sandbox.nganluong.vn/checkout.php";
        private string merchant_site_code = "275";    //thay ma merchant site ma ban da dang ky vao day 
        private string secure_pass = "123456";    //thay mat khau giao tiep giua website cua ban voi NganLuong.vn ma ban da dang ky vao day//91f020425a901101 
        public string GetMD5Hash(string input) 
        { 
            var x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var bs = System.Text.Encoding.UTF8.GetBytes(input); 
            bs = x.ComputeHash(bs); 
            var s = new System.Text.StringBuilder();
            foreach (var b in bs) 
            { 
                s.Append(b.ToString("x2").ToLower()); 
            } 
            var md5String = s.ToString();
            return md5String; } 
        public string buildCheckoutUrl(string return_url, string receiver, string transaction_info, string order_code, string price) 
        { 
            // Tạo biến secure code 
            var secure_code = ""; 
            secure_code += this.merchant_site_code; 
            secure_code += " " + HttpUtility.UrlEncode(return_url).ToLower(); 
            secure_code += " " + receiver; 
            secure_code += " " + transaction_info; 
            secure_code += " " + order_code; 
            secure_code += " " + price;
            secure_code += " " + this.secure_pass;
            // Tạo mảng băm 
            var ht = new Hashtable(); 
            ht.Add("merchant_site_code", this.merchant_site_code); 
            ht.Add("return_url", HttpUtility.UrlEncode(return_url).ToLower());
            ht.Add("receiver", receiver);
            ht.Add("transaction_info", transaction_info); 
            ht.Add("order_code", order_code); 
            ht.Add("price", price); 
            ht.Add("secure_code", this.GetMD5Hash(secure_code));
            // Tạo url redirect 
            var redirect_url = this.nganluong_url;
            if (redirect_url.IndexOf("?") == -1) 
            { 
                redirect_url += "?"; 
            } 
            else if (redirect_url.Substring(redirect_url.Length - 1, 1) != "?" && redirect_url.IndexOf("&") == -1) 
            { 
                redirect_url += "&"; 
            } 
            var url = "";
            // Duyêt các phần tử trong mảng băm ht dể tạo redirect url 
            var en = ht.GetEnumerator();
            while (en.MoveNext()) 
            { 
                if (url == "") url += en.Key.ToString() + "=" + en.Value.ToString();
                else url += "&" + en.Key.ToString() + "=" + en.Value;
            }
            var rdu = redirect_url + url;return rdu;
        } 
        public bool verifyPaymentUrl(string transaction_info, string order_code, string price, string payment_id, string payment_type, string error_text, string secure_code) 
        { 
            // Tạo mã xác thực từ web 
            var str = ""; 
            str += " " + transaction_info;
            str += " " + order_code; 
            str += " " + price; 
            str += " " + payment_id;
            str += " " + payment_type; 
            str += " " + error_text;
            str += " " + this.merchant_site_code;
            str += " " + this.secure_pass;
            // Mã hóa các tham số
            var verify_secure_code = ""; 
            verify_secure_code = this.GetMD5Hash(str);
            // Xác thực mã của web với mã trả về từ nganluong.vn 
            if (verify_secure_code == secure_code) return true;return false; 
        } 
        protected void btnSubmit_Click(object sender, EventArgs e) 
        { 
            var return_url = "http://localhost:35921/WebSite/"; 
            var transaction_info = "DEMO"; 
            var order_code = DateTime.Now.ToString("yyyyMMddHHmmss");
            var receiver = "webmaster@dotnet.vn";
            //Tài khoản nhận tiền 
            var price = "100000";
            var nganLuong = new NganLuongCheckout();
            string url; url = nganLuong.buildCheckoutUrl(return_url, receiver, transaction_info, order_code, price); 
            //Response.Redirect(url); 
        } 

    }
}

