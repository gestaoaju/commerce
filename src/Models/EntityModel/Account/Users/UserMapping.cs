/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;

namespace Gestaoaju.Models.EntityModel.Account.Users
{
    public static class UserMapping
    {
        public static void MapUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(nameof(User));

                entity.HasKey(p => new { p.Id, p.TenantId });
                entity.HasAlternateKey(p => p.Email);

                entity.Property(p => p.Id).UseSqlServerIdentityColumn();
                entity.Property(p => p.TenantId).IsRequired();
                entity.Property(p => p.Name).HasMaxLength(80).IsRequired();
                entity.Property(p => p.Email).HasMaxLength(80).IsRequired();
                entity.Property(p => p.Salt).HasMaxLength(50).IsRequired();
                entity.Property(p => p.Password).HasMaxLength(255).IsRequired();
                entity.Property(p => p.AccessCode).HasMaxLength(80);
                entity.Property(p => p.LastLogin).IsRequired();
                entity.Property(p => p.LastChangePassword).IsRequired();

                entity.HasOne(p => p.Tenant).WithMany(p => p.Users).HasForeignKey(p => p.TenantId);
            });
        }
    }
}
