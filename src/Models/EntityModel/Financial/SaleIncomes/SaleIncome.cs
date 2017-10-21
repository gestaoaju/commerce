/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Financial.SalePayments;
using System;

namespace Gestaoaju.Models.EntityModel.Financial.SaleIncomes
{
    public class SaleIncome : ITenantScope, IIncome
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int SalePaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public decimal AmountReceived { get; set; }
        public DateTime? AnticipationDate { get; set; }
        public decimal? AmountAnticipated { get; set; }
        public Tenant Tenant { get; set; }
        public SalePayment SalePayment { get; set; }
        int IIncome.PaymentId
        {
            get { return SalePaymentId; }
            set { SalePaymentId = value; }
        }
    }
}
