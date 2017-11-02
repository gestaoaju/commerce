/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;

namespace Gestaoaju.Models.EntityModel.Commercial.PaymentMethodPlans
{
    public class PaymentMethodPlan : ITenantScope
    {
        public int PaymentMethodId { get; set; }
        public int TenantId { get; set; }
        public int MaximumNumberInstallments { get; set; }
        public int MinimumValue { get; set; }
    }
}
