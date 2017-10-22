/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Inventory.ProductMovements
{
    public static class ProductMovementMapping
    {
        public static void Map(this EntityTypeBuilder<ProductMovement> entity)
        {
            entity.ToTable(nameof(ProductMovement));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.TransactionId,
                p.ProductId,
                p.StoreId
            });
        }
    }
}
