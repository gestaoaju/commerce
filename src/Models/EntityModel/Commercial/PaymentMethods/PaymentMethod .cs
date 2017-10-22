/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Commercial.PaymentMethodFees;
using Gestaoaju.Models.EntityModel.Financial.RentPayments;
using Gestaoaju.Models.EntityModel.Financial.SalePayments;
using System.Collections.Generic;

namespace Gestaoaju.Models.EntityModel.Commercial.PaymentMethods
{
    public class PaymentMethod : ITenantScope
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public int InstallmentLimit { get; set; }
        public int? DaysToReceive { get; set; }
        public bool EarlyReceipt { get; set; }
        public bool Active { get; set; }
        public Tenant Tenant { get; set; }
        public ICollection<PaymentMethodFee> PaymentMethodFees { get; set; }
        public ICollection<RentPayment> RentPayments { get; set; }
        public ICollection<SalePayment> SalePayments { get; set; }
    }
}
