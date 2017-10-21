/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Commercial.RentContracts;
using Gestaoaju.Models.EntityModel.Commercial.SaleOrders;
using Gestaoaju.Models.EntityModel.Inventory.PurchaseOrders;
using System.Collections.Generic;

namespace Gestaoaju.Models.EntityModel.Financial.Wallets
{
    public class Wallet : ITenantScope
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string AdditionalInformation { get; set; }
        public bool Active { get; set; }
        public Tenant Tenant { get; set; }
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public ICollection<RentContract> RentContracts { get; set; }
        public ICollection<SaleOrder> SaleOrders { get; set; }
    }
}
