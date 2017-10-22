/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Commercial.SaleProducts
{
    public static class SaleProductMapping
    {
        public static void Map(this EntityTypeBuilder<SaleProduct> entity)
        {
            entity.ToTable(nameof(SaleProduct));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.SaleOrderId,
                p.ProductId
            });
        }
    }
}
