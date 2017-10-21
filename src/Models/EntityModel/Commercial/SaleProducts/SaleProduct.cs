/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Catalog.Products;
using Gestaoaju.Models.EntityModel.Commercial.SaleOrders;
using Gestaoaju.Models.EntityModel.Inventory;
using System;

namespace Gestaoaju.Models.EntityModel.Commercial.SaleProducts
{
    public class SaleProduct : ITenantScope, IOutgoingProduct
    {
        public int SaleOrderId { get; set; }
        public int ProductId { get; set; }
        public int TenantId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public decimal? Discount { get; set; }
        public decimal TotalPayable { get; set; }
        public Guid? StockTransactionId { get; set; }
        public SaleOrder SaleOrder { get; set; }
        public Product Product { get; set; }
        public Tenant Tenant { get; set; }
    }
}
