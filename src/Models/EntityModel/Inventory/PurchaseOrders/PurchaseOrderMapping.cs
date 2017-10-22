/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Inventory.PurchaseOrders
{
    public static class PurchaseOrderMapping
    {
        public static void Map(this EntityTypeBuilder<PurchaseOrder> entity)
        {
            entity.ToTable(nameof(PurchaseOrder));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.HasMany(p => p.PurchasedProducts)
                .WithOne(p => p.PurchaseOrder)
                .HasForeignKey(p => new { p.TenantId, p.PurchaseOrderId });

            entity.HasMany(p => p.PurchasePayments)
                .WithOne(p => p.PurchaseOrder)
                .HasForeignKey(p => new { p.TenantId, p.PurchaseOrderId });
        }
    }
}
