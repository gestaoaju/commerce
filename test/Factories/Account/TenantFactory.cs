// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using System;

namespace Gestaoaju.Factories.Account
{
    public static class TenantFactory
    {
        public static Tenant CreateTenant(this ApplicationContext context,
             string owner = "tenant@email.com")
        {
            var tenant = new Tenant
            {
                Owner = owner,
                CreatedAt = DateTime.UtcNow
            };

            tenant = context.Tenants.Add(tenant).Entity;
            context.SaveChanges();

            return tenant;
        }
    }
}
