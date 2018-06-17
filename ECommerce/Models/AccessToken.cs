namespace ECommerce.Models
{
    public class AccessToken
    {
        public string consumer_key { get; set; }
        public string request_token { get; set; }
        public string verifier_token { get; set; }
    }
}