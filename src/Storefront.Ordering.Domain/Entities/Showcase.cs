namespace Storefront.Ordering.Domain.Entities
{
    public sealed class Showcase
    {
        private Showcase() { }

        public Showcase(string name, string description, Photo photo)
        {
            Name = name;
            Description = description;
            Photo = photo;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public Photo Photo { get; private set; }
    }
}
