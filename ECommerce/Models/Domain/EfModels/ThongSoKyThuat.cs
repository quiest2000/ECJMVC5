using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Domain.EfModels
{
    public class ThongSoKyThuat
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }
        public int SanPhamId { get; set; }
        public string ThuocTinh { get; set; }
        public string GiaTri { get; set; }
        [ForeignKey(nameof(SanPhamId))]
        public virtual SanPham SanPham { get; set; }
    }
}
