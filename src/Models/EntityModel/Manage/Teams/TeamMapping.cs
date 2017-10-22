/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Manage.Teams
{
    public static class TeamMapping
    {
        public static void Map(this EntityTypeBuilder<Team> entity)
        {
            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.HasMany(p => p.TeamMembers)
                .WithOne(p => p.Team)
                .HasForeignKey(p => new { p.TenantId, p.TeamId });

            entity.HasMany(p => p.TeamRules)
                .WithOne(p => p.Team)
                .HasForeignKey(p => new { p.TenantId, p.TeamId });
        }
    }
}
