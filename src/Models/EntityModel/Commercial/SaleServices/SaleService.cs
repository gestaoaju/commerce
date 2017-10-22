/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Catalog.Services;
using Gestaoaju.Models.EntityModel.Commercial.SaleOrders;

namespace Gestaoaju.Models.EntityModel.Commercial.SaleServices
{
    public class SaleService : ITenantScope
    {
        public int SaleOrderId { get; set; }
        public int ServiceId { get; set; }
        public int TenantId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public decimal? Discount { get; set; }
        public decimal TotalPayable { get; set; }
        public SaleOrder SaleOrder { get; set; }
        public Service Service { get; set; }
        public Tenant Tenant { get; set; }
    }
}
