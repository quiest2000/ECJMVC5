using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ECommerce.Models.ClassPartial
{
    [MetadataType(typeof(SanphamPartial.Metadata))]
    public partial class SanphamPartial
    {
        internal sealed class Metadata
        {
            [AllowHtml]
            public string MoTa { get; set; }
        }
    }

    public partial class DanhsachdangkisanphamNCC
    {
        public string TenNCC { get; set; }
    }
    [MetadataType(typeof(HopDongNCCPartitial.Metadata))]
    public partial class HopDongNCCPartitial
    {
        public string TenNCC { get; set; }
        internal sealed class Metadata
        {
            [Range(1,999999999,ErrorMessage="Mời bạn nhập trong khoảng 1 -> 999999999")]
            public int? ThoiHanHD { get; set; }
            [Range(1, 999999, ErrorMessage = "Mời bạn nhập trong khoảng 1 -> 999999")]
            public int? SLToiThieu { get; set; }
            [Range(1, 999999, ErrorMessage = "Mời bạn nhập trong khoảng 1 -> 999999")]
            public int? SLCungCap { get; set; }
        }
    }
}