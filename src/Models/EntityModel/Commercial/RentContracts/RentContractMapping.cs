/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Commercial.RentContracts
{
    public static class RentContractMapping
    {
        public static void Map(this EntityTypeBuilder<RentContract> entity)
        {
            entity.ToTable(nameof(RentContract));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.HasMany(p => p.RentedProducts)
                .WithOne(p => p.RentContract)
                .HasForeignKey(p => new { p.TenantId, p.RentContractId });

            entity.HasMany(p => p.RentPayments)
                .WithOne(p => p.RentContract)
                .HasForeignKey(p => new { p.TenantId, p.RentContractId });
        }
    }
}
