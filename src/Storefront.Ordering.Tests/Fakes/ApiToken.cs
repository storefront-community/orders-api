using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Storefront.Ordering.API.Authorization;

namespace Storefront.Ordering.Tests.Fakes
{
    public sealed class ApiToken
    {
        private readonly JwtSecurityToken _token;

        public ApiToken(JwtOptions jwtOptions)
        {
            _token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(
                        key: Encoding.ASCII.GetBytes(jwtOptions.Secret)
                    ),
                    algorithm: SecurityAlgorithms.HmacSha256
                )
            );

            _token.Payload.Add("tenantId", 10);
            _token.Payload.Add("userId", null);
        }

        public override string ToString()
        {
            return new JwtSecurityTokenHandler().WriteToken(_token);
        }
    }
}
