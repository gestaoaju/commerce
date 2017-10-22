/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Financial.SalePayments
{
    public static class SalePaymentMapping
    {
        public static void Map(this EntityTypeBuilder<SalePayment> entity)
        {
            entity.ToTable(nameof(SalePayment));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            entity.HasMany(p => p.SaleIncomes)
                .WithOne(p => p.SalePayment)
                .HasForeignKey(p => new { p.TenantId, p.SalePaymentId });
        }
    }
}
