/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Catalog.ProductPrices
{
    public static class ProductPriceMapping
    {
        public static void Map(this EntityTypeBuilder<ProductPrice> entity)
        {
            entity.ToTable(nameof(ProductPrice));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.StoreId,
                p.ProductId
            });
        }
    }
}
