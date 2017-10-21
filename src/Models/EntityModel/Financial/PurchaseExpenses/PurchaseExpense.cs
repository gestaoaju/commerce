/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Inventory.PurchasePayments;
using System;

namespace Gestaoaju.Models.EntityModel.Financial.PurchaseExpenses
{
    public class PurchaseExpense
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int PurchasePaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }
        public Tenant Tenant { get; set; }
        public PurchasePayment PurchasePayment { get; set; }
    }
}
