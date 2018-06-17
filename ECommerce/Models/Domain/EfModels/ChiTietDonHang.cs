using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Domain.EfModels
{
    [Table("ChiTietDonHang")]
    public class ChiTietDonHang
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Mã đơn hàng")]
        public int DonHangId { get; set; }
        [Display(Name = "Mã sản phẩm")]
        public int SanPhamId { get; set; }
        [Display(Name = "Số Lượng")]
        public int? SoLuong { get; set; }
        [Display(Name = "Thành tiền")]
        public decimal? ThanhTien { get; set; }

        public decimal DonGia { get; set; }
        [ForeignKey(nameof(DonHangId))]
        public virtual DonHangKH DonHangKH { get; set; }
        [ForeignKey(nameof(SanPhamId))]
        public virtual SanPham SanPham { get; set; }
    }
}
