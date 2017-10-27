/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Commercial.RentContracts;
using Gestaoaju.Models.EntityModel.Financial.RentIncomes;
using System.Collections.Generic;
using System.Linq;

namespace Gestaoaju.Models.ServiceModel.Financial
{
    public class RentBilling : Billing<RentIncome>
    {
        public TenantDbContext Tenant { get; private set; }
        
        public RentContract RentContract { get; private set; }

        public RentBilling(TenantDbContext tenant, RentContract saleOrder) : base(saleOrder)
        {
            Tenant = tenant;
            RentContract = saleOrder;
        }

        protected override RentIncome NewIncome() => new RentIncome();

        protected override void AddIncomes(IEnumerable<RentIncome> incomes) => Tenant.RentIncomes.AddRange(incomes);

        protected override void RemoveIncomes() => Tenant.RentIncomes
            .RemoveRange(RentContract.RentPayments.SelectMany(payment => payment.RentIncomes));
    }
}
