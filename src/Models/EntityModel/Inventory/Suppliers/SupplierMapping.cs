/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Inventory.Suppliers
{
    public static class SupplierMapping
    {
        public static void Map(this EntityTypeBuilder<Supplier> entity)
        {
            entity.ToTable(nameof(Supplier));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.HasMany(p => p.PurchaseOrders)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => new { p.TenantId, p.SupplierId });
        }
    }
}
