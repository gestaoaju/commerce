/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Catalog.Services
{
    public static class ServiceMapping
    {
        public static void Map(this EntityTypeBuilder<Service> entity)
        {
            entity.ToTable(nameof(Service));
            
            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.HasMany(p => p.SaleServices)
                .WithOne(p => p.Service)
                .HasForeignKey(p => new { p.TenantId, p.ServiceId });

            entity.HasMany(p => p.ServicePrices)
                .WithOne(p => p.Service)
                .HasForeignKey(p => new { p.TenantId, p.ServiceId });
        }
    }
}
