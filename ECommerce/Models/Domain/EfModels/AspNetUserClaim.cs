using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Domain.EfModels
{
    public class AspNetUserClaim
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string AspNetUserId { get; set; }
        [ForeignKey(nameof(AspNetUserId))]
        public virtual AspNetUser AspNetUser { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
