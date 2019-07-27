namespace Storefront.Ordering.API.Models.IntegrationModel.FileStorage
{
    public sealed class StoredFile
    {
        public string Id { get; set; }
        public string ContentType { get; set; }
        public byte[] Binary { get; set; }
    }
}
