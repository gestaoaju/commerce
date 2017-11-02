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
        public int MinimumNumberInstallments { get; set; }
        public decimal? Percentage { get; set; }
        public decimal? FixedValue { get; set; }
        public Tenant Tenant { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
