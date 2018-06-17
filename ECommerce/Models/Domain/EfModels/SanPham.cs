using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ECommerce.Models.Domain.EfModels
{
    public class SanPham
    {
        public SanPham()
        {
            BinhLuans = new HashSet<BinhLuan>();
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            HopDongNCCs = new HashSet<HopDongNcc>();
            SanPhamKhuyenMais = new HashSet<SanPhamKhuyenMai>();
            ThongSoKyThuats = new HashSet<ThongSoKyThuat>();
            Sanphamcanmuas = new HashSet<SanPhamCanMua>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TenSP { get; set; }
        public int LoaiSpId { get; set; }
        [ForeignKey(nameof(LoaiSpId))]
        public virtual LoaiSanPham LoaiSanPham { get; set; }
        public int? SoLuotXemSP { get; set; }
        public int HangSxId { get; set; }
        public string XuatXu { get; set; }
        public decimal? GiaTien { get; set; }
        [AllowHtml]
        public string MoTa { get; set; }
        public string AnhDaiDien { get; set; }
        public string AnhNen { get; set; }
        public string AnhKhac { get; set; }
        public int? SoLuong { get; set; }
        public bool? isnew { get; set; }
        public bool? ishot { get; set; }
        public decimal? GiaGoc { get; set; }

        public virtual ICollection<BinhLuan> BinhLuans { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        [ForeignKey(nameof(HangSxId))]
        public virtual HangSanXuat HangSanXuat { get; set; }
        public virtual ICollection<HopDongNcc> HopDongNCCs { get; set; }
        public virtual ICollection<SanPhamKhuyenMai> SanPhamKhuyenMais { get; set; }
        public virtual ICollection<ThongSoKyThuat> ThongSoKyThuats { get; set; }
        public virtual ICollection<SanPhamCanMua> Sanphamcanmuas { get; set; }
    }
}
