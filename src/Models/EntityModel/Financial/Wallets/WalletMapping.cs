/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Financial.Wallets
{
    public static class WalletMapping
    {
        public static void Map(this EntityTypeBuilder<Wallet> entity)
        {
            entity.ToTable(nameof(Wallet));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.HasMany(p => p.PurchaseOrders)
                .WithOne(p => p.Wallet)
                .HasForeignKey(p => new { p.TenantId, p.WalletId });

            entity.HasMany(p => p.RentContracts)
                .WithOne(p => p.Wallet)
                .HasForeignKey(p => new { p.TenantId, p.WalletId });

            entity.HasMany(p => p.SaleOrders)
                .WithOne(p => p.Wallet)
                .HasForeignKey(p => new { p.TenantId, p.WalletId });
        }
    }
}
