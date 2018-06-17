using System;

namespace ECommerce.Models.B2B
{
    public class HopDong
    {
        public int order_id { get; set; }
        public string product_id { get; set; }
        public string product_name { get; set; }
        public string product_quantity { get; set; }
        public DateTime product_date { get; set; }
        public string supplier_key { get; set; }
    }
}