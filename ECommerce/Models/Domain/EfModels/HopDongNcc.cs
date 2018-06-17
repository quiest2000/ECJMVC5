using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Domain.EfModels
{
    [Table("HopDongNCC")]
    public class HopDongNcc
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int NccId { get; set; }
        public DateTime? NgayKy { get; set; }
        public int? ThoiHanHD { get; set; }
        public DateTime? TGGiaoHang { get; set; }
        public int SanPhamId { get; set; }
        public int? SLToiThieu { get; set; }
        public int? SLCungCap { get; set; }
        public DateTime? Dateaccept { get; set; }
        public bool? IsBuy { get; set; }
        public int? SoNgayGiao { get; set; }
        public decimal? DonGia { get; set; }
        public bool? TinhTrang { get; set; }
        public bool? TTThanhToan { get; set; }
        [ForeignKey(nameof(SanPhamId))]
        public virtual SanPham SanPham { get; set; }
        [ForeignKey(nameof(NccId))]
        public virtual NhaCungCap NhaCungCap { get; set; }
    }
}
