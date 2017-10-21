/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Catalog.ProductPrices;
using Gestaoaju.Models.EntityModel.Catalog.ServicePrices;
using Gestaoaju.Models.EntityModel.Commercial.RentContracts;
using Gestaoaju.Models.EntityModel.Commercial.SaleOrders;
using Gestaoaju.Models.EntityModel.Financial.FixedExpenses;
using Gestaoaju.Models.EntityModel.Financial.OtherCashActivities;
using Gestaoaju.Models.EntityModel.Inventory.ProductMovements;
using Gestaoaju.Models.EntityModel.Inventory.PurchaseOrders;
using Gestaoaju.Models.EntityModel.Manage.TeamMembers;
using System.Collections.Generic;

namespace Gestaoaju.Models.EntityModel.Manage.Stores
{
    public class Store : ITenantScope
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public Tenant Tenant { get; set; }
        public ICollection<FixedExpense> FixedExpenses { get; set; }
        public ICollection<OtherCashActivity> OtherCashActivities { get; set; }
        public ICollection<ProductPrice> ProductPrices { get; set; }
        public ICollection<ProductMovement> ProductMovements { get; set; }
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public ICollection<RentContract> RentContracts { get; set; }
        public ICollection<SaleOrder> SaleOrders { get; set; }
        public ICollection<ServicePrice> ServicePrices { get; set; }
        public ICollection<TeamMember> TeamMembers { get; set; }
    }
}
