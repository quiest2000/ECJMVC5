//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ConsoleEFCodeFirst.Repositories
{
    [System.ComponentModel.DataAnnotations.Schema.Table("LoaiSP")]
    public partial class LoaiSP
    {
        public LoaiSP()
        {
            this.SanPhams = new List<SanPham>();
        }
        [System.ComponentModel.DataAnnotations.Key]
        [StringLength(1024)] public string MaLoai { get; set; }
        [StringLength(1024)] public string TenLoai { get; set; }
    
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
