using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Domain.EfModels
{
    [Table("ConfigAPI")]
    public class ConfigAPI
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int NhaCungCapId { get; set; }
        public string LinkRequesrToken { get; set; }
        public string LinkAccessToken { get; set; }
        public string LinkKiemTraLuongTon { get; set; }
        public string LinkXacNhanGiaoHang { get; set; }
        [ForeignKey(nameof(NhaCungCapId))]
        public virtual NhaCungCap NhaCungCap { get; set; }
    }
}
