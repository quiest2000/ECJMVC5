using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Domain.EfModels
{
    public class HangSanXuat
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Hãng sản xuất")]
        public int Id { get; set; }
        [Display(Name = "Tên Hãng")]
        public string TenHang { get; set; }
        [Display(Name = "Trụ sở chính")]
        public string TruSoChinh { get; set; }
        [Display(Name = "Quốc gia")]
        public string QuocGia { get; set; }


        private ICollection<SanPham> _sanPhams;
        public virtual ICollection<SanPham> SanPhams
        {
            get => _sanPhams ?? (_sanPhams = new List<SanPham>());
            set => _sanPhams = value;
        }
    }
}
