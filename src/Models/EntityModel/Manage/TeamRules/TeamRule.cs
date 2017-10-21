/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Manage.Teams;

namespace Gestaoaju.Models.EntityModel.Manage.TeamRules
{
    public class TeamRule : ITenantScope
    {
        public int TeamId { get; set; }
        public int TenantId { get; set; }
        public string Scope { get; set; }
        public Team Team { get; set; }
        public Tenant Tenant { get; set; }
    }
}
