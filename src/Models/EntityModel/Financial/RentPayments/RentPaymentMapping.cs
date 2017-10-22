/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Financial.RentPayments
{
    public static class RentPaymentMapping
    {
        public static void Map(this EntityTypeBuilder<RentPayment> entity)
        {
            entity.ToTable(nameof(RentPayment));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.HasMany(p => p.RentIncomes)
                .WithOne(p => p.RentPayment)
                .HasForeignKey(p => new { p.TenantId, p.RentPaymentId });
        }
    }
}
