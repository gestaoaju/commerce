/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Inventory.PurchasedProducts
{
    public static class PurchasedProductMapping
    {
        public static void Map(this EntityTypeBuilder<PurchasedProduct> entity)
        {
            entity.ToTable(nameof(PurchasedProduct));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.PurchaseOrderId,
                p.ProductId
            });
        }
    }
}
