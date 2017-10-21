/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Account.Users;
using Gestaoaju.Models.EntityModel.Manage.Stores;
using Gestaoaju.Models.EntityModel.Manage.Teams;

namespace Gestaoaju.Models.EntityModel.Manage.TeamMembers
{
    public class TeamMember : ITenantScope
    {
        public int UserId { get; set; }
        public int TenantId { get; set; }
        public int TeamId { get; set; }
        public int StoreId { get; set; }        
        public User User { get; set; }
        public Team Team { get; set; }
        public Store Store { get; set; }
        public Tenant Tenant { get; set; }
    }
}
