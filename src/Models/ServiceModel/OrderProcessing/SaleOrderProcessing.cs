/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Commercial.SaleOrders;
using Gestaoaju.Models.ServiceModel.Financial;
using Gestaoaju.Models.ServiceModel.Inventory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gestaoaju.Models.ServiceModel.OrderProcessing
{
    public class SaleOrderProcessing
    {
        public TenantDbContext Tenant { get; private set; }
        public SaleOrder SaleOrder { get; private set; }
        public ProductConsumption ProductConsumption { get; private set; }
        public SaleBilling Billing { get; private set; }
        public bool HasNoItems { get; private set; }
        public bool HasPendingPayment { get; private set; }

        public SaleOrderProcessing(TenantDbContext tenant)
        {
            Tenant = tenant;
        }

        private bool SaleOrderIsPending()
        {
            HasNoItems = SaleOrder.TotalPayable == 0;
            HasPendingPayment = SaleOrder.TotalPayable != SaleOrder.SalePayments.Sum(p => p.Total);

            return HasNoItems || HasPendingPayment;
        }

        public async Task<bool> Process(int saleOrderId)
        {
            SaleOrder = await Tenant.SaleOrders
                .WhereId(saleOrderId)
                .IncludeStore()
                .IncludeSaleProducts()
                .IncludePaymentMethodsAndFees()
                .SingleOrDefaultAsync();

            if (SaleOrder == null || SaleOrder.Confirmed || SaleOrderIsPending())
            {
                return false;
            }

            ProductConsumption = new ProductConsumption(Tenant, SaleOrder);
            if (!await ProductConsumption.Confirm()) return false;

            Billing = new SaleBilling(Tenant, SaleOrder);
            Billing.Confirm();

            SaleOrder.ConfirmationDate = DateTime.UtcNow;

            await Tenant.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Revert(int saleOrderId)
        {
            SaleOrder = await Tenant.SaleOrders
                .WhereId(saleOrderId)
                .IncludeStore()
                .IncludeSaleProducts()
                .IncludeSaleIncomes()
                .SingleOrDefaultAsync();

            if (SaleOrder == null || !SaleOrder.Confirmed)
            {
                return false;
            }

            ProductConsumption = new ProductConsumption(Tenant, SaleOrder);
            await ProductConsumption.Revert();

            Billing = new SaleBilling(Tenant, SaleOrder);
            Billing.Revert();

            SaleOrder.ConfirmationDate = null;

            await Tenant.SaveChangesAsync();

            return true;
        }
    }
}
