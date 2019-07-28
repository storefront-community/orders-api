using System;
using System.IdentityModel.Tokens.Jwt;

namespace Storefront.Ordering.API.Authorization
{
    public class AuthPayload
    {
        public AuthPayload(string authorization)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = authorization.Replace("Bearer ", string.Empty);
            var payload = handler.ReadJwtToken(token).Payload;
            var userId = payload["userId"];
            var tenantId = payload["tenantId"];

            if (userId != null)
            {
                UserId = Convert.ToInt64(userId);
            }

            TenantId = Convert.ToInt64(tenantId);
        }

        public long? UserId { get; }
        public long TenantId { get; }
    }
}
