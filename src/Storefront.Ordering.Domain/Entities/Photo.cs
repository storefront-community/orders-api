using System;

namespace Storefront.Ordering.Domain.Entities
{
    public sealed class Photo
    {
        public Photo(byte[] binary, string contentType)
            : this(Guid.NewGuid().ToString(), binary, contentType) { }


        public Photo(string id, byte[] binary, string contentType)
        {
            Id = id;
            Binary = binary;
            ContentType = contentType;
        }

        public string Id { get; private set; }
        public byte[] Binary { get; private set; }
        public string ContentType { get; private set; }
    }
}
