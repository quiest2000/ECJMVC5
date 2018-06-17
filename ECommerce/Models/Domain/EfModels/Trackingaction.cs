using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Domain.EfModels
{
    public class TrackingAction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public int SanPhamId { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public DateTime? Ngaythuchien { get; set; }
    }
}
