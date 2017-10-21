/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Commercial.RentPayments;
using System;

namespace Gestaoaju.Models.EntityModel.Financial.RentIncomes
{
    public class RentIncome : ITenantScope, IIncome
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int RentPaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public decimal AmountReceived { get; set; }
        public DateTime? AnticipationDate { get; set; }
        public decimal? AmountAnticipated { get; set; }
        public Tenant Tenant { get; set; }
        public RentPayment RentPayment { get; set; }
        int IIncome.PaymentId
        {
            get { return RentPaymentId; }
            set { RentPaymentId = value; }
        }
    }
}
