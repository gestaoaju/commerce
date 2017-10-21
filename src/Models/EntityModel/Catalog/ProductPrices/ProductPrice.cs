/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Catalog.Products;
using Gestaoaju.Models.EntityModel.Manage.Stores;

namespace Gestaoaju.Models.EntityModel.Catalog.ProductPrices
{
    public class ProductPrice : ITenantScope
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int TenantId { get; set; }
        public decimal? UnitSalePrice { get; set; }
        public decimal? UnitRentPrice { get; set; }
        public Tenant Tenant { get; set; }
        public Store Store { get; set; }
        public Product Product { get; set; }
    }
}
