/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Commercial.Customers
{
    public static class CustomerMapping
    {
        public static void Map(this EntityTypeBuilder<Customer> entity)
        {
            entity.ToTable(nameof(Customer));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.HasMany(p => p.RentContracts)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => new { p.TenantId, p.CustomerId });

            entity.HasMany(p => p.SaleOrders)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => new { p.TenantId, p.CustomerId });
        }
    }
}
