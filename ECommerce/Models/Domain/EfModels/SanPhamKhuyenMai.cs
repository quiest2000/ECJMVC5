using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ECommerce.Models.Domain.EfModels
{
    public class SanPhamKhuyenMai
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int KhuyenMaiId { get; set; }
        public int SanPhamId { get; set; }

        [AllowHtml]
        public string MoTa { get; set; }
        public int? GiamGia { get; set; }
        [ForeignKey(nameof(KhuyenMaiId))]
        public virtual KhuyenMai KhuyenMai { get; set; }
        [ForeignKey(nameof(SanPhamId))]
        public virtual SanPham SanPham { get; set; }
    }
}
