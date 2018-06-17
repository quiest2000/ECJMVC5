using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ECommerce.Models.Domain.EfModels
{
    public class SanPhamCanMua
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SanPhamId { get; set; }
        public int? Soluong { get; set; }
        public DateTime? Ngayketthuc { get; set; }
        public DateTime? Ngaydang { get; set; }

        [AllowHtml]
        public string Mota { get; set; }
        [ForeignKey(nameof(SanPhamId))]
        public virtual SanPham SanPham { get; set; }
        public virtual ICollection<DangKySanPhamNcc> DanhsachdangkisanphamNCCs { get; set; }
    }
}
