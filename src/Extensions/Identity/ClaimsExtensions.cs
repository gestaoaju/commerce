/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Gestaoaju.Extensions.Identity
{
    public static class ClaimsExtensions
    {
        public static string NameIdentifier(this IEnumerable<Claim> claims)
        {
            var claim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return claim?.Value;
        }
    }
}
