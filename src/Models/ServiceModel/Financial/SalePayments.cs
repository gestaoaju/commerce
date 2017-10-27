/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Commercial.SaleOrders;
using Gestaoaju.Models.EntityModel.Financial;
using Gestaoaju.Models.EntityModel.Financial.SalePayments;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestaoaju.Models.ServiceModel.Financial
{
    public class SalePayments
    {
        public TenantDbContext Tenant { get; private set; }
        public SaleOrder SaleOrder { get; private set; }
        public bool HasPendingPayment { get; set; }

        public SalePayments(TenantDbContext tenant)
        {
            Tenant = tenant;
        }
        
        private void CalculateTotals(IEnumerable<SalePayment> payments, decimal? discount)
        {
            decimal totalPaid = payments.Sum(payment => payment.Total);

            SaleOrder.Discount = discount;
            SaleOrder.TotalPayable = new Money(SaleOrder.Total)
                .SubtractPercentage(SaleOrder.Discount ?? 0);

            HasPendingPayment = totalPaid != SaleOrder.TotalPayable;
        }

        public async Task<bool> RegisterPayments(int saleOrderId, IEnumerable<SalePayment> payments, decimal? discount)
        {
            SaleOrder = await Tenant.SaleOrders
                .WhereId(saleOrderId)
                .IncludeSalePayments()
                .SingleOrDefaultAsync();

            if (SaleOrder == null || SaleOrder.Confirmed)
            {
                return false;
            }

            CalculateTotals(payments, discount);

            if (HasPendingPayment)
            {
                return false;
            }

            Tenant.SalePayments.RemoveRange(SaleOrder.SalePayments);
            Tenant.SalePayments.AddRange(payments);

            await Tenant.SaveChangesAsync();

            return true;
        }
    }
}
