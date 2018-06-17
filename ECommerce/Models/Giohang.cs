using System;
using System.Collections.Generic;

namespace ECommerce.Models
{
    public class Giohang
    {
        private List<ChiTietGioHang> Cart;
        public double phiVanChuyen = 0;
        public Giohang()
        {
            Cart = new List<ChiTietGioHang>();
        }
        public List<ChiTietGioHang> getGiohang()
        {
            return Cart;
        }
        public void addCart(ChiTietGioHang a)
        {
            Cart.Add(a);
        }
        public bool removeCart(int index)
        {
            try
            {
                Cart.RemoveAt(index);
                return true;
            }
            catch (Exception e) { return false; }
        }
        /// <summary>
        /// Tính tổng tiền của các sản phẩm trong giỏ hàng
        /// </summary>
        /// <returns></returns>
        public double Tinhtongtiensanpham()
        {
            double count = 0;
            foreach(var temp in Cart)
            {
                count += temp.Thanhtien;
            }
            return count;
        }
        public double TinhtongtienCart()
        {
            return Tinhtongtiensanpham() + phiVanChuyen;
        }
        public bool Changequanlity(int index, string soluong)
        {
            try
            {
                Cart[index].Soluong = Int32.Parse(soluong);
                Cart[index].Tinhtien();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public int Tinhtongsoluongtronggio()
        {
            var count = 0;
            foreach (var temp in Cart)
            {
                count += temp.Soluong;
            }
            return count;
        }
    }
}