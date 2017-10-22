/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Manage.TeamRules
{
    public static class TeamRuleMapping
    {
        public static void Map(this EntityTypeBuilder<TeamRule> entity)
        {
            entity.ToTable(nameof(TeamRule));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.TeamId,
                p.Scope
            });
        }
    }
}
