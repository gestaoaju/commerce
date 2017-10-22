/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Catalog.Products
{
    public static class ProductMapping
    {
        public static void Map(this EntityTypeBuilder<Product> entity)
        {
            entity.ToTable(nameof(Product));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.HasMany(p => p.ProductPrices)
                .WithOne(p => p.Product)
                .HasForeignKey(p => new { p.TenantId, p.ProductId });

            entity.HasMany(p => p.ProductMovements)
                .WithOne(p => p.Product)
                .HasForeignKey(p => new { p.TenantId, p.ProductId });

            entity.HasMany(p => p.PurchasedProducts)
                .WithOne(p => p.Product)
                .HasForeignKey(p => new { p.TenantId, p.ProductId });

            entity.HasMany(p => p.RentedProducts)
                .WithOne(p => p.Product)
                .HasForeignKey(p => new { p.TenantId, p.ProductId });

            entity.HasMany(p => p.SaleProducts)
                .WithOne(p => p.Product)
                .HasForeignKey(p => new { p.TenantId, p.ProductId });
        }
    }
}
