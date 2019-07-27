using Storefront.Ordering.API.Models.DataModel.Stores;

namespace Storefront.Ordering.API.Models.TransferModel.Stores
{
    public sealed class ReadStoreJson
    {
        public ReadStoreJson() { }

        public ReadStoreJson(Store store)
        {
            Id = store.Id;
            Name = store.Name;
            Active = store.Active;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
