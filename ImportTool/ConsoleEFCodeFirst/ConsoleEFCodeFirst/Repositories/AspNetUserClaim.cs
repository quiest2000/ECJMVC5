//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ConsoleEFCodeFirst.Repositories
{
    [System.ComponentModel.DataAnnotations.Schema.Table("AspNetUserClaims")]
    public partial class AspNetUserClaim
    {
        public int Id { get; set; }
        [StringLength(1024)] public string UserId { get; set; }
        [StringLength(1024)] public string ClaimType { get; set; }
        [StringLength(1024)] public string ClaimValue { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
