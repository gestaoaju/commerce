/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Catalog.ProductPrices;
using Gestaoaju.Models.EntityModel.Commercial.RentedProducts;
using Gestaoaju.Models.EntityModel.Commercial.SaleProducts;
using Gestaoaju.Models.EntityModel.Inventory;
using Gestaoaju.Models.EntityModel.Inventory.ProductMovements;
using Gestaoaju.Models.EntityModel.Inventory.PurchasedProducts;
using System.Collections.Generic;

namespace Gestaoaju.Models.EntityModel.Catalog.Products
{
    public class Product : ITenantScope
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string AdditionalInformation { get; set; }
        public bool Marketed { get; set; }
        public bool IsManufactured { get; set; }
        public bool CanFraction { get; set; }
        public InventoryControl InventoryControl { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
        public virtual ICollection<ProductMovement> ProductMovements { get; set; }
        public virtual ICollection<PurchasedProduct> PurchasedProducts { get; set; }
        public virtual ICollection<RentedProduct> RentedProducts { get; set; }
        public virtual ICollection<SaleProduct> SaleProducts { get; set; }
    }
}
