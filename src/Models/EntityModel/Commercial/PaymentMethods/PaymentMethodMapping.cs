/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Commercial.PaymentMethods
{
    public static class PaymentMethodMapping
    {
        public static void Map(this EntityTypeBuilder<PaymentMethod> entity)
        {
            entity.ToTable(nameof(PaymentMethod));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.HasMany(p => p.PaymentMethodFees)
                .WithOne(p => p.PaymentMethod)
                .HasForeignKey(p => new { p.TenantId, p.PaymentMethodId })
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(p => p.RentPayments)
                .WithOne(p => p.PaymentMethod)
                .HasForeignKey(p => new { p.TenantId, p.PaymentMethodId });

            entity.HasMany(p => p.SalePayments)
                .WithOne(p => p.PaymentMethod)
                .HasForeignKey(p => new { p.TenantId, p.PaymentMethodId });
        }
    }
}
