using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Storefront.Ordering.API.Authorization
{
    public sealed class ApiSecurityKey : SecurityKey
    {
        private readonly SecurityKey _securityKey;

        public ApiSecurityKey(string secret)
        {
            _securityKey = new SymmetricSecurityKey(
                key: Encoding.ASCII.GetBytes(secret)
            );
        }

        public override int KeySize => _securityKey.KeySize;
    }
}
