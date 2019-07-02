using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Storefront.Ordering.API.Authorization
{
    public sealed class ApiToken
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _secret;

        public ApiToken(ApiTokenOptions options)
        {
            _issuer = options.Issuer;
            _audience = options.Audience;
            _secret = options.Secret;
        }

        public SigningCredentials SigningCredentials =>
            new SigningCredentials(new ApiSecurityKey(_secret), SecurityAlgorithms.HmacSha256);

        public string Generate()
        {
            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken
            (
                issuer: _issuer,
                audience: _audience,
                notBefore: DateTime.UtcNow,
                signingCredentials: SigningCredentials
            ));
        }
    }
}
