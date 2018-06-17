namespace ECommerce.Utils
{
    public static class StringExtensions
    {
        public static int ToInt(this string src)
        {
            int.TryParse(src, out var rs);
            return rs;
        }
    }
}