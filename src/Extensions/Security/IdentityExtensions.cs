// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Gestaoaju.Extensions.Security
{
    public static class IdentityExtensions
    {
        public static string AccessCode(this IIdentity identity)
        {
            return (identity as ClaimsIdentity).Claims
                .Where(c => c.Type == "AccessCode")
                .Select(c => c.Value)
                .FirstOrDefault();
        }
    }
}
