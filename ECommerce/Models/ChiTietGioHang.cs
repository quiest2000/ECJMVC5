using ECommerce.Models.Domain.EfModels;

namespace ECommerce.Models
{
    public class ChiTietGioHang
    {
        public SanPham sanPham { get; set; }
        public int Soluong { get; set; }
        private double thanhtien;

        public double Thanhtien
        {
            get { return (double)sanPham.GiaTien * Soluong; }
            set { thanhtien = value; }
        }
        public Domain.EfModels.DonHangKH Donhangkh { get; set; }
       
        public void Tinhtien()
        {
            Thanhtien = (double)sanPham.GiaTien * Soluong;
        }
    }
}