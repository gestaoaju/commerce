/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Manage.TeamMembers;
using Gestaoaju.Models.EntityModel.Manage.TeamRules;
using System.Collections.Generic;

namespace Gestaoaju.Models.EntityModel.Manage.Teams
{
    public class Team : ITenantScope
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public Tenant Tenant { get; set; }
        public ICollection<TeamMember> TeamMembers { get; set; }
        public ICollection<TeamRule> TeamRules { get; set; }
    }
}
