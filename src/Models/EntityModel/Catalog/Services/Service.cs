/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Catalog.ServicePrices;
using Gestaoaju.Models.EntityModel.Commercial.SaleServices;
using System.Collections.Generic;

namespace Gestaoaju.Models.EntityModel.Catalog.Services
{
    public class Service : ITenantScope
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string AdditionalInformation { get; set; }
        public bool Marketed { get; set; }
        public bool CanFraction { get; set; }
        public Tenant Tenant { get; set; }
        public ICollection<SaleService> SaleServices { get; set; }
        public ICollection<ServicePrice> ServicePrices { get; set; }
    }
}
