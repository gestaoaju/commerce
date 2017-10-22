/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Financial.PurchasePayments
{
    public static class PurchasePaymentMapping
    {
        public static void Map(this EntityTypeBuilder<PurchasePayment> entity)
        {
            entity.ToTable(nameof(PurchasePayment));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            entity.HasMany(p => p.PurchaseExpenses)
                .WithOne(p => p.PurchasePayment)
                .HasForeignKey(p => new { p.TenantId, p.PurchasePaymentId });
        }
    }
}
