/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Financial.PurchaseExpenses;
using Gestaoaju.Models.EntityModel.Inventory.PurchaseOrders;
using System;
using System.Collections.Generic;

namespace Gestaoaju.Models.EntityModel.Inventory.PurchasePayments
{
    public class PurchasePayment : ITenantScope
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int PurchaseOrderId { get; set; }
        public int NumberOfInstallments { get; set; }
        public decimal InstallmentValue { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public Tenant Tenant { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public ICollection<PurchaseExpense> PurchaseExpenses { get; set; }
    }
}
