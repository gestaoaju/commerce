/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Commercial.SaleOrders
{
    public static class SaleOrderMapping
    {
        public static void Map(this EntityTypeBuilder<SaleOrder> entity)
        {
            entity.ToTable(nameof(SaleOrder));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.HasMany(p => p.SalePayments)
                .WithOne(p => p.SaleOrder)
                .HasForeignKey(p => new { p.TenantId, p.SaleOrderId });

            entity.HasMany(p => p.SaleProducts)
                .WithOne(p => p.SaleOrder)
                .HasForeignKey(p => new { p.TenantId, p.SaleOrderId });

            entity.HasMany(p => p.SaleServices)
                .WithOne(p => p.SaleOrder)
                .HasForeignKey(p => new { p.TenantId, p.SaleOrderId });
        }
    }
}
