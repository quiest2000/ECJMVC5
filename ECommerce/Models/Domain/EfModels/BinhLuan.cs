using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Domain.EfModels
{
    public class BinhLuan
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã bình luận")]
        public int MaBL { get; set; }
        [Display(Name = "Mã sản phẩm")]
        public int SanPhamId { get; set; }
        [Display(Name = "Mã Khách Hàng")]
        public string KhachHangId { get; set; }
        [Display(Name = "Nội Dung Bình Luận")]
        public string NoiDung { get; set; }
        [Display(Name = "Ngày Đăng")]
        public DateTime? NgayDang { get; set; }
        [Display(Name = "Họ Tên")]
        public string HoTen { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Đã trả lời")]
        public string DaTraLoi { get; set; }
        public int? Parent { get; set; }
        [ForeignKey(nameof(KhachHangId))]
        public virtual AspNetUser AspNetUser { get; set; }
        [ForeignKey(nameof(SanPhamId))]
        public virtual SanPham SanPham { get; set; }
    }
}
