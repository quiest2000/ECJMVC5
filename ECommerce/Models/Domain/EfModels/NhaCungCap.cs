using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ECommerce.Models.Domain.EfModels
{
    public class NhaCungCap
    {
        public NhaCungCap()
        {
            HopDongNCCs = new HashSet<HopDongNcc>();
            Oauths = new HashSet<Oauth>();
            ConfigAPIs = new HashSet<ConfigAPI>();
            DanhsachdangkisanphamNCCs = new HashSet<DangKySanPhamNcc>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TenNCC { get; set; }
        public string DiaChi { get; set; }
        public string SDT_NCC { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public int NetUserId { get; set; }
    
        public virtual ICollection<HopDongNcc> HopDongNCCs { get; set; }
        public virtual ICollection<Oauth> Oauths { get; set; }
        [ForeignKey(nameof(NetUserId))]
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<ConfigAPI> ConfigAPIs { get; set; }
        public virtual ICollection<DangKySanPhamNcc> DanhsachdangkisanphamNCCs { get; set; }
    }
}
