/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Commercial.SaleServices
{
    public static class SaleServiceMapping
    {
        public static void Map(this EntityTypeBuilder<SaleService> entity)
        {
            entity.ToTable(nameof(SaleService));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.SaleOrderId,
                p.ServiceId
            });
        }
    }
}
