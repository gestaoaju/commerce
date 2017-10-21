/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Commercial.PaymentMethods;
using Gestaoaju.Models.EntityModel.Commercial.RentContracts;
using Gestaoaju.Models.EntityModel.Financial;
using Gestaoaju.Models.EntityModel.Financial.RentIncomes;
using System;
using System.Collections.Generic;

namespace Gestaoaju.Models.EntityModel.Commercial.RentPayments
{
    public class RentPayment : ITenantScope, IPayment
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int RentContractId { get; set; }
        public int PaymentMethodId { get; set; }
        public int NumberOfInstallments { get; set; }
        public decimal InstallmentValue { get; set; }
        public decimal InstallmentBilling { get; set; }
        public decimal? FeePercentage { get; set; }
        public decimal? FeeFixedValue { get; set; }
        public decimal Total { get; set; }
        public decimal BilledAmount { get; set; }
        public DateTime Date { get; set; }
        public Tenant Tenant { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public RentContract RentContract { get; set; }
        public ICollection<RentIncome> RentIncomes { get; set; }
    }
}
