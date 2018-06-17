using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Domain.EfModels
{
    public class LoaiSanPham
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TenLoai { get; set; }
    
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
