using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ECommerce.Models.B2B
{
    public class SanPhamCanMuaAdd
    {
        [Required]
        [Display(Name = "Mã sản phẩm")]
        public int MaSP { get; set; }

        [Display(Name = "Số lượng")]
        public int? Soluong { get; set; }

        [Display(Name = "Ngày kết thúc")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Ngayketthuc { get; set; }

        [Display(Name = "Mô tả")]
        [AllowHtml]
        public string Mota { get; set; }
    }

    public class SanPhamCanMuaEdit
    {
        [Required]
        [Display(Name = "Id")]
        public int ID { get; set; }

        [Display(Name = "Số lượng")]
        public int? Soluong { get; set; }

        [Display(Name = "Ngày kết thúc")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Ngayketthuc { get; set; }

        [Display(Name = "Mô tả")]
        [AllowHtml]
        public string Mota { get; set; }
    }

}