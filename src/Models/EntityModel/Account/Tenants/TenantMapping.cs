// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Microsoft.EntityFrameworkCore;

namespace Gestaoaju.Models.EntityModel.Account.Tenants
{
    public static class TenantMapping
    {
        public static void MapTenant(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Tenant>();

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id).UseSqlServerIdentityColumn();
            entity.Property(p => p.Owner).HasMaxLength(80).IsRequired();
            entity.Property(p => p.CreatedAt).IsRequired();
            entity.Property(p => p.DeactivatedAt);

            entity.HasMany(p => p.Users).WithOne(p => p.Tenant).HasForeignKey(p => p.TenantId);
        }
    }
}
