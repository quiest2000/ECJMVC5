//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleEFCodeFirst.Repositories
{using System.ComponentModel.DataAnnotations;
    [System.ComponentModel.DataAnnotations.Schema.Table("GiaoDien")]
    public partial class GiaoDien
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        [StringLength(1024)] public string ThuocTinh { get; set; }
        [StringLength(1024)] public string GiaTri { get; set; }
    }
}
