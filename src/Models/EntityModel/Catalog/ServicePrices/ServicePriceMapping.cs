/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Catalog.ServicePrices
{
    public static class ServicePriceMapping
    {
        public static void Map(this EntityTypeBuilder<ServicePrice> entity)
        {
            entity.ToTable(nameof(ServicePrice));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.StoreId,
                p.ServiceId
            });
        }
    }
}
