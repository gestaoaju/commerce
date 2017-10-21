/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Commercial.RentContracts;
using Gestaoaju.Models.EntityModel.Commercial.SaleOrders;
using System;
using System.Collections.Generic;

namespace Gestaoaju.Models.EntityModel.Commercial.Customers
{
    public class Customer : ITenantScope
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternativePhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public Tenant Tenant { get; set; }
        public ICollection<RentContract> RentContracts { get; set; }
        public ICollection<SaleOrder> SaleOrders { get; set; }
    }
}
