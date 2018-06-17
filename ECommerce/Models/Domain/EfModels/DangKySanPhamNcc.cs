using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Domain.EfModels
{
    public class DangKySanPhamNcc
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Mã Sản phẩm cần mua")]
        public int? SanPhamCanMuaId { get; set; }
        [Display(Name = "Mã nhà cung cấp")]
        public int NccId { get; set; }
        [Display(Name = "Ghi chú")]
        public string Ghichu { get; set; }
        [Display(Name = "Ngày đăng kí")]
        public DateTime? NgayDK { get; set; }
        [Display(Name = "Trạng thái")]
        public int? Trangthai { get; set; }
        [Display(Name = "Đơn giá")]
        public int? TienmoiSP { get; set; }
        [ForeignKey(nameof(NccId))]
        public virtual NhaCungCap NhaCungCap { get; set; }
        [ForeignKey(nameof(SanPhamCanMuaId))]
        public virtual SanPhamCanMua SanPhamCanMua { get; set; }
        [NotMapped]
        public string TenNcc { get; set; }
    }
}
