/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Manage.TeamMembers;
using System;

namespace Gestaoaju.Models.EntityModel.Account.Users
{
    public class User : ITenantScope
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime LastChangePassword { get; set; }
        public Tenant Tenant { get; set; }
        public TeamMember TeamMember { get; set; }
    }
}
