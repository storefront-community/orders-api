namespace Storefront.Ordering.Domain.Entities
{
    public sealed class Contact
    {
        private Contact() { }

        public Contact(string name, string mobile, string email)
        {
            Name = name;
            Mobile = mobile;
            Email = email;
        }

        public string Name { get; private set; }
        public string Mobile { get; private set; }
        public string Email { get; private set; }
    }
}
