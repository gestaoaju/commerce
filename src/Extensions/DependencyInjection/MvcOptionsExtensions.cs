// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Gestaoaju.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Gestaoaju.Extensions.DependencyInjection
{
    public static class MvcOptionsExtensions
    {
        public static void UseJwtAuthorizeFilter(this MvcOptions options)
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            options.Filters.Add(new AuthorizeFilter(policy));
        }

        public static void UseCustomFilters(this MvcOptions options)
        {
            options.Filters.Add(typeof(ErrorHandlerAttribute));
            options.Filters.Add(typeof(ModelValidationAttribute));
        }
    }
}
