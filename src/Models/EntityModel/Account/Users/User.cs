// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System;
using Gestaoaju.Models.EntityModel.Account.Tenants;

namespace Gestaoaju.Models.EntityModel.Account.Users
{
    public class User
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime LastChangePassword { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
