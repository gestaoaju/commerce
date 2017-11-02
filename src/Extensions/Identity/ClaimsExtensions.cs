/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Gestaoaju.Extensions.Identity
{
    public static class ClaimsExtensions
    {
        public static int CustomId(this IEnumerable<Claim> claims, string type)
        {
            var claim = claims.FirstOrDefault(c => c.Type == type);
            var customId = 0;

            Int32.TryParse(claim?.Value, out customId);

            return customId;
        }

        public static int TenantId(this IEnumerable<Claim> claims)
        {
            return claims.CustomId("TenantId");
        }

        public static int UserId(this IEnumerable<Claim> claims)
        {
            return claims.CustomId("UserId");
        }
    }
}
