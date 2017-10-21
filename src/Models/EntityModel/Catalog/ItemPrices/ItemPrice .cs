/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Catalog.ProductPrices;
using Gestaoaju.Models.EntityModel.Catalog.ServicePrices;

namespace Gestaoaju.Models.EntityModel.Catalog.ItemPrices
{
    public class ItemPrice : ITenantScope
    {
        public int ItemId => (ProductId ?? ServiceId).Value;
        public int? ProductId { get; set; }
        public int? ServiceId { get; set; }
        public int TenantId { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Marketed { get; set; }
        public bool CanFraction { get; set; }
        public decimal? UnitRentPrice { get; set; }
        public decimal? UnitSalePrice { get; set; }
        public bool IsProduct => ProductId != null;
        public bool IsService => ServiceId != null;
        public bool HasPrice => UnitRentPrice != null || UnitSalePrice != null;

        public ProductPrice ProductPrice => new ProductPrice
        {
            StoreId = StoreId,
            ProductId = ProductId.Value,
            UnitRentPrice = UnitRentPrice,
            UnitSalePrice = UnitSalePrice
        };

        public ServicePrice ServicePrice => new ServicePrice
        {
            StoreId = StoreId,
            ServiceId = ServiceId.Value,
            UnitPrice = UnitSalePrice.Value
        };
    }
}
