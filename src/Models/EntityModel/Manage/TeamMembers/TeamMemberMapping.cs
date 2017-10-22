/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Account.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Manage.TeamMembers
{
    public static class TeamMemberMapping
    {
        public static void Map(this EntityTypeBuilder<TeamMember> entity)
        {
            entity.ToTable(nameof(TeamMember));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.UserId
            });
        }
    }
}
