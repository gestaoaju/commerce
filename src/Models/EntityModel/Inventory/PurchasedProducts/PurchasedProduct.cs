/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Catalog.Products;
using Gestaoaju.Models.EntityModel.Inventory;
using Gestaoaju.Models.EntityModel.Inventory.PurchaseOrders;
using System;

namespace Gestaoaju.Models.EntityModel.Inventory.PurchasedProducts
{
    public class PurchasedProduct : ITenantScope, IIncomingProduct
    {
        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }
        public int TenantId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Total { get; set; }
        public decimal? Discount { get; set; }
        public decimal TotalCost { get; set; }
        public Guid? TransactionId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public Product Product { get; set; }
        public Tenant Tenant { get; set; }
    }
}
