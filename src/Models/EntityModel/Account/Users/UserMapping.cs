/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Manage.TeamMembers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Account.Users
{
    public static class UserMapping
    {
        public static void Map(this EntityTypeBuilder<User> entity)
        {
            entity.ToTable(nameof(User));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.HasAlternateKey(p => p.Email);

            entity.Property(p => p.Id).ValueGeneratedOnAdd();
            entity.Property(p => p.TenantId).IsRequired();
            entity.Property(p => p.Name).HasMaxLength(80).IsRequired();
            entity.Property(p => p.Email).HasMaxLength(80).IsRequired();
            entity.Property(p => p.Salt).HasMaxLength(50).IsRequired();
            entity.Property(p => p.Password).HasMaxLength(255).IsRequired();
            entity.Property(p => p.LastLogin).IsRequired();
            entity.Property(p => p.LastChangePassword).IsRequired();

            entity.HasOne(p => p.TeamMember)
                .WithOne(p => p.User)
                .HasForeignKey<TeamMember>(p => new { p.TenantId, p.UserId });
        }
    }
}
