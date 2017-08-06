// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System;
using System.Collections.Generic;
using Gestaoaju.Models.EntityModel.Account.Users;

namespace Gestaoaju.Models.EntityModel.Account.Tenants
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeactivatedAt { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
