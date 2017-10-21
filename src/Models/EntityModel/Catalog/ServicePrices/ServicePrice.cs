/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Catalog.Services;
using Gestaoaju.Models.EntityModel.Manage.Stores;

namespace Gestaoaju.Models.EntityModel.Catalog.ServicePrices
{
    public class ServicePrice : ITenantScope
    {
        public int StoreId { get; set; }
        public int ServiceId { get; set; }
        public int TenantId { get; set; }
        public decimal UnitPrice { get; set; }
        public Tenant Tenant { get; set; }
        public Store Store { get; set; }
        public Service Service { get; set; }
    }
}
