namespace Storefront.Ordering.API.Authorization
{
    public sealed class ApiTokenOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
    }

}
