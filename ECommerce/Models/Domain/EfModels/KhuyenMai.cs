using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ECommerce.Models.Domain.EfModels
{
    public class KhuyenMai
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã khuyến mãi ")]
        public int Id { get; set; }
        [Display(Name = "Ngày bắt đầu")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? NgayBatDau { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Ngày kết thúc")]
        public DateTime? NgayKetThuc { get; set; }
        [Display(Name = "Nội dung")]
        [AllowHtml]
        public string NoiDung { get; set; }
        [Display(Name = "Tên chương trình")]
        public string TenCT { get; set; }
        [Display(Name = "Ảnh chương trình")]
        public string AnhCT { get; set; }


        private ICollection<SanPhamKhuyenMai> _sanPhamKhuyenMai;
        public virtual ICollection<SanPhamKhuyenMai> SanPhamKhuyenMai
        {
            get => _sanPhamKhuyenMai ?? (_sanPhamKhuyenMai = new List<SanPhamKhuyenMai>());
            set => _sanPhamKhuyenMai = value;
        }

    }
}
