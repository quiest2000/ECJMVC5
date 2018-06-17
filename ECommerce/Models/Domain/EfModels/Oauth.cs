using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Domain.EfModels
{
    [Table("Oauth")]
    public class Oauth
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Consumer_key { get; set; }
        public string Callback { get; set; }
        public string Request_token { get; set; }
        public string Verifier_token { get; set; }
        public DateTime? Date_comsumer { get; set; }
        public int NccId { get; set; }
        public string Token { get; set; }
        public DateTime? ExpiresTime { get; set; }
        [ForeignKey(nameof(NccId))]
        public virtual NhaCungCap NhaCungCap { get; set; }
    }
}
