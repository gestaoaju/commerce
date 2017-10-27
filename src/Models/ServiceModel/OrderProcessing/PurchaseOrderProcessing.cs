/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Inventory.PurchaseOrders;
using Gestaoaju.Models.ServiceModel.Financial;
using Gestaoaju.Models.ServiceModel.Inventory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gestaoaju.Models.ServiceModel.OrderProcessing
{
    public class PurchaseOrderProcessing
    {
        public TenantDbContext Tenant { get; private set; }
        public PurchaseOrder PurchaseOrder { get; private set; }
        public ProductReplenishment ProductReplenishment { get; private set; }
        public PurchaseCost PurchaseCost { get; private set; }
        public bool HasNoItems { get; private set; }
        public bool HasPendingPayment { get; private set; }

        public PurchaseOrderProcessing(TenantDbContext tenant)
        {
            Tenant = tenant;
        }

        private bool PurchaseOrderIsPending()
        {
            HasNoItems = PurchaseOrder.TotalPayable == 0;
            HasPendingPayment = PurchaseOrder.TotalPayable != PurchaseOrder.PurchasePayments.Sum(p => p.Total);

            return HasNoItems || HasPendingPayment;
        }

        public async Task<bool> Process(int purchaseOrderId)
        {
            PurchaseOrder = await Tenant.PurchaseOrders
                .WhereId(purchaseOrderId)
                .IncludeStore()
                .IncludePurchasePayments()
                .IncludePurchasedProducts()
                .SingleOrDefaultAsync();

            if (PurchaseOrder == null || PurchaseOrder.Confirmed || PurchaseOrderIsPending())
            {
                return false;
            }
            
            ProductReplenishment = new ProductReplenishment(Tenant, PurchaseOrder);
            if (!await ProductReplenishment.Confirm()) return false;

            PurchaseCost = new PurchaseCost(Tenant, PurchaseOrder);
            PurchaseCost.Confirm();

            PurchaseOrder.ConfirmationDate = DateTime.UtcNow;

            await Tenant.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Revert(int purchaseOrderId)
        {
            PurchaseOrder = await Tenant.PurchaseOrders
                .WhereId(purchaseOrderId)
                .IncludeStore()
                .IncludePurchasedProducts()
                .IncludePurchaseExpenses()
                .SingleOrDefaultAsync();

            if (PurchaseOrder == null || !PurchaseOrder.Confirmed)
            {
                return false;
            }

            ProductReplenishment = new ProductReplenishment(Tenant, PurchaseOrder);
            if (!await ProductReplenishment.Revert()) return false;

            PurchaseCost = new PurchaseCost(Tenant, PurchaseOrder);
            PurchaseCost.Revert();

            PurchaseOrder.ConfirmationDate = null;

            await Tenant.SaveChangesAsync();

            return true;
        }
    }
}
