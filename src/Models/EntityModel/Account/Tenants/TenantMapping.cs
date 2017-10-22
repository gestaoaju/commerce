/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Account.Tenants
{
    public static class TenantMapping
    {
        public static void Map(this EntityTypeBuilder<Tenant> entity)
        {
            entity.ToTable(nameof(Tenant));

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id).ValueGeneratedOnAdd();
            entity.Property(p => p.Owner).HasMaxLength(80).IsRequired();
            entity.Property(p => p.CreatedAt).IsRequired();
            entity.Property(p => p.DeactivatedAt);

            entity.HasMany(p => p.Customers).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.FixedExpenses).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.OtherCashActivities).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.Partners).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.PaymentMethodFees).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.PaymentMethods).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.Products).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.ProductMovements).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.ProductPrices).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.PurchasedProducts).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.PurchaseExpenses).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.PurchaseOrders).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.PurchasePayments).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.RentContracts).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.RentIncomes).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.RentPayments).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.RentedProducts).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.SaleIncomes).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.SaleOrders).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.SalePayments).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.SaleProducts).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.SaleServices).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.Services).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.ServicePrices).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.Stores).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.Suppliers).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.Teams).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.TeamMembers).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.TeamRules).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.Users).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
            entity.HasMany(p => p.Wallets).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
        }
    }
}
