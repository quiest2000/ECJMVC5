//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
namespace ConsoleEFCodeFirst.Repositories
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Trackingaction")]
    public partial class Trackingaction
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        [StringLength(1024)] public string Username { get; set; }
        [StringLength(1024)] public string MaSP { get; set; }
        [StringLength(1024)] public string Controller { get; set; }
        [StringLength(1024)] public string Action { get; set; }
        public Nullable<System.DateTime> Ngaythuchien { get; set; }
    }
}