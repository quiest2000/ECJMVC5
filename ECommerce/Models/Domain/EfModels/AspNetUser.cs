using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Domain.EfModels
{
    public class AspNetUser
    {
        public AspNetUser()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            BinhLuans = new HashSet<BinhLuan>();
            DonHangKHs = new HashSet<DonHangKH>();
            AspNetRoles = new HashSet<AspNetRole>();
            NhaCungCaps = new HashSet<NhaCungCap>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string MaNV { get; set; }
        public string CMND { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public bool? GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string Avatar { get; set; }
    
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }
        public virtual ICollection<DonHangKH> DonHangKHs { get; set; }
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
        public virtual ICollection<NhaCungCap> NhaCungCaps { get; set; }
    }
}
