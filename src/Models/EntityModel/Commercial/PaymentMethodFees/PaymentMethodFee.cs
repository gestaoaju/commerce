/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Commercial.PaymentMethods;

namespace Gestaoaju.Models.EntityModel.Commercial.PaymentMethodFees
{
    public class PaymentMethodFee : ITenantScope
    {
        public int PaymentMethodId { get; set; }
        public int TenantId { get; set; }
        public int MinimumInstallment { get; set; }
        public decimal? FeePercentage { get; set; }
        public decimal? FeeFixedValue { get; set; }
        public Tenant Tenant { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
