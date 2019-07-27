namespace Storefront.Ordering.API.Models.TransferModel.Stores
{
    public sealed class ErrorStoreNotFoundJson : UnprocessableEntityJson
    {
        public ErrorStoreNotFoundJson()
        {
            Error = "STORE_NOT_FOUND";
        }
    }
}
