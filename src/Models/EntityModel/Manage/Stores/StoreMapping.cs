/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Manage.Stores
{
    public static class StoreMapping
    {
        public static void Map(this EntityTypeBuilder<Store> entity)
        {
            entity.ToTable(nameof(Store));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.HasMany(p => p.FixedExpenses)
                .WithOne(p => p.Store)
                .HasForeignKey(p => new { p.TenantId, p.StoreId });

            entity.HasMany(p => p.OtherCashActivities)
                .WithOne(p => p.Store)
                .HasForeignKey(p => new { p.TenantId, p.StoreId });

            entity.HasMany(p => p.ProductPrices)
                .WithOne(p => p.Store)
                .HasForeignKey(p => new { p.TenantId, p.StoreId });

            entity.HasMany(p => p.ProductMovements)
                .WithOne(p => p.Store)
                .HasForeignKey(p => new { p.TenantId, p.StoreId });

            entity.HasMany(p => p.PurchaseOrders)
                .WithOne(p => p.Store)
                .HasForeignKey(p => new { p.TenantId, p.StoreId });

            entity.HasMany(p => p.RentContracts)
                .WithOne(p => p.Store)
                .HasForeignKey(p => new { p.TenantId, p.StoreId });

            entity.HasMany(p => p.SaleOrders)
                .WithOne(p => p.Store)
                .HasForeignKey(p => new { p.TenantId, p.StoreId });

            entity.HasMany(p => p.ServicePrices)
                .WithOne(p => p.Store)
                .HasForeignKey(p => new { p.TenantId, p.StoreId });

            entity.HasMany(p => p.TeamMembers)
                .WithOne(p => p.Store)
                .HasForeignKey(p => new { p.TenantId, p.StoreId });
        }
    }
}
