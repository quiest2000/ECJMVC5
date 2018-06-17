using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Domain.EfModels
{
    public class AspNetUserLogin
    {
        [Key, Column(Order = 0)]
        public string LoginProvider { get; set; }
        [Key, Column(Order = 1)]
        public string ProviderKey { get; set; }
        [Key, Column(Order = 2)]
        public string AspNetUserId { get; set; }
        [ForeignKey(nameof(AspNetUserId))]
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
