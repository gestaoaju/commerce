/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Catalog.Products;
using Gestaoaju.Models.EntityModel.Manage.Stores;
using System;

namespace Gestaoaju.Models.EntityModel.Inventory.ProductMovements
{
    public class ProductMovement : ITenantScope
    {
        public Guid TransactionId { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int TenantId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime Date { get; set; }
        public Product Product { get; set; }
        public Store Store { get; set; }
        public Tenant Tenant { get; set; }
    }
}
