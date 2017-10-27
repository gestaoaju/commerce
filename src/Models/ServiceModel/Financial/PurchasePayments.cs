/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Financial;
using Gestaoaju.Models.EntityModel.Financial.PurchasePayments;
using Gestaoaju.Models.EntityModel.Inventory.PurchaseOrders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestaoaju.Models.ServiceModel.Financial
{
    public class PurchasePayments
    {
        public TenantDbContext Tenant { get; private set; }
        public PurchaseOrder PurchaseOrder { get; private set; }
        public bool HasPendingPayment { get; private set; }
        public decimal? ShippingCost { get; set; }
        public decimal? OtherTaxes { get; set; }
        public decimal? Discount { get; set; }

        public PurchasePayments(TenantDbContext tenant)
        {
            Tenant = tenant;
        }

        private void CalculateTotals(IEnumerable<PurchasePayment> payments)
        {
            decimal totalPaid = payments.Sum(payment => payment.Total);

            PurchaseOrder.Discount = Discount;
            PurchaseOrder.ShippingCost = ShippingCost;
            PurchaseOrder.OtherTaxes = OtherTaxes;
            PurchaseOrder.TotalPayable = new Money(PurchaseOrder.Total)
                .Sum(PurchaseOrder.ShippingCost ?? 0)
                .Sum(PurchaseOrder.OtherTaxes ?? 0)
                .SubtractPercentage(PurchaseOrder.Discount ?? 0);

            HasPendingPayment = totalPaid != PurchaseOrder.TotalPayable;
        }

        public async Task<bool> RegisterPayments(int purchaseOrderId, IEnumerable<PurchasePayment> payments)
        {
            PurchaseOrder = await Tenant.PurchaseOrders
                .WhereId(purchaseOrderId)
                .IncludePurchasePayments()
                .SingleOrDefaultAsync();

            if (PurchaseOrder == null || PurchaseOrder.Confirmed)
            {
                return false;
            }

            CalculateTotals(payments);

            if (HasPendingPayment)
            {
                return false;
            }

            Tenant.PurchasePayments.RemoveRange(PurchaseOrder.PurchasePayments);
            Tenant.PurchasePayments.AddRange(payments);

            await Tenant.SaveChangesAsync();

            return true;
        }
    }
}
