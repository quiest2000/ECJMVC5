//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;
namespace ConsoleEFCodeFirst.Repositories
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Link")]
    public partial class Link
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int id { get; set; }
        [StringLength(1024)] public string Url { get; set; }
        [StringLength(1024)] public string Image { get; set; }
        [StringLength(1024)] public string Text { get; set; }
        [StringLength(1024)] public string Group { get; set; }
    }
}