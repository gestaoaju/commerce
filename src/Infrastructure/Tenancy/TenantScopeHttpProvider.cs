/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Extensions.Identity;
using Microsoft.AspNetCore.Http;

namespace Gestaoaju.Infrastructure.Tenancy
{
    public class TenantScopeHttpProvider : ITenantScopeProvider
    {
        private IHttpContextAccessor httpContextAccessor;

        public TenantScopeHttpProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int CurrentId => httpContextAccessor.HttpContext.User.Claims.TenantId();
    }
}
