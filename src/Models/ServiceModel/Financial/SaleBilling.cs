/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Commercial.SaleOrders;
using Gestaoaju.Models.EntityModel.Financial.SaleIncomes;
using System.Collections.Generic;
using System.Linq;

namespace Gestaoaju.Models.ServiceModel.Financial
{
    public class SaleBilling : Billing<SaleIncome>
    {
        public TenantDbContext Tenant { get; private set; }
        
        public SaleOrder SaleOrder { get; private set; }

        public SaleBilling(TenantDbContext tenant, SaleOrder saleOrder) : base(saleOrder)
        {
            Tenant = tenant;
            SaleOrder = saleOrder;
        }

        protected override SaleIncome NewIncome() => new SaleIncome();

        protected override void AddIncomes(IEnumerable<SaleIncome> incomes) => Tenant.SaleIncomes.AddRange(incomes);

        protected override void RemoveIncomes() => Tenant.SaleIncomes
            .RemoveRange(SaleOrder.SalePayments.SelectMany(payment => payment.SaleIncomes));
    }
}
