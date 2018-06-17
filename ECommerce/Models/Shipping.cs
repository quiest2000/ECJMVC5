namespace ECommerce.Models
{
    public class Shipping
    {
        public string supplier_key { get; set; }
        public int order_id { get; set; }
        public int product_id { get; set; }
        public int product_quantity { get; set; }
        public string product_date { get; set; }
        public string access_token { get; set; }
    }
}