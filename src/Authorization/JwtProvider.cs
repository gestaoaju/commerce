// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Security.Claims;
using System.Text;

namespace Gestaoaju.Authorization
{
    public class JwtProvider
    {
        private JwtOptions options;

        public JwtProvider(JwtOptions options)
        {
            this.options = options;
        }

        private byte[] EncodedSecretKey => Encoding.ASCII.GetBytes(options.SecretKey);

        public SymmetricSecurityKey SecurityKey => new SymmetricSecurityKey(EncodedSecretKey);

        public SigningCredentials SigningCredentials =>
            new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

        public string CreateToken(string accessCode)
        {
            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken
            (
                issuer: options.Issuer,
                audience: options.Audience,
                notBefore: DateTime.UtcNow,
                claims: new[] { new Claim("AccessCode", accessCode) },
                expires: DateTime.UtcNow.AddMonths(1),
                signingCredentials: SigningCredentials
            ));
        }        
    }
}
