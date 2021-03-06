/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Account.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gestaoaju.Extensions.Http
{
    public static class HttpContextExtentions
    {
        public static async Task SignInAsync(this HttpContext httpContext, User user)
        {
            var scheme = CookieAuthenticationDefaults.AuthenticationScheme;
            var userId = new Claim("UserId", user.Id.ToString());
            var tenantId = new Claim("TenantId", user.TenantId.ToString());
            var identity = new ClaimsIdentity(new[] { userId, tenantId }, scheme);
            var principal = new ClaimsPrincipal(identity);

            await httpContext.SignInAsync(scheme, principal);
        }
    }
}
