/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Financial;
using Gestaoaju.Models.EntityModel.Financial.PurchaseExpenses;
using Gestaoaju.Models.EntityModel.Financial.PurchasePayments;
using Gestaoaju.Models.EntityModel.Inventory.PurchaseOrders;
using System.Collections.Generic;
using System.Linq;

namespace Gestaoaju.Models.ServiceModel.Financial
{
    public class PurchaseCost
    {
        public TenantDbContext Tenant { get; private set; }
        public PurchaseOrder PurchaseOrder { get; private set; }

        public PurchaseCost(TenantDbContext tenant, PurchaseOrder purchaseOrder)
        {
            Tenant = tenant;
            PurchaseOrder = purchaseOrder;
        }
        
        private IEnumerable<PurchaseExpense> CalculateExpenses()
        {
            foreach (PurchasePayment payment in PurchaseOrder.PurchasePayments)
            {
                for (int installment = 1; installment <= payment.NumberOfInstallments; installment++)
                {
                    PurchaseExpense expense = new PurchaseExpense();
                    expense.PurchasePaymentId = payment.Id;
                    expense.AmountPaid = payment.InstallmentValue;
                    expense.PaymentDate = payment.Date.AddMonths(installment - 1);

                    yield return expense;
                }
            }
        }

        public void Confirm()
        {
            Tenant.PurchaseExpenses.AddRange(CalculateExpenses());

            PurchaseOrder.TotalCost = new Money(PurchaseOrder.PurchasePayments.Sum(p => p.Total))
                .Sum(PurchaseOrder.ShippingCost ?? 0)
                .Sum(PurchaseOrder.OtherTaxes ?? 0)
                .SubtractPercentage(PurchaseOrder.Discount ?? 0);
        }

        public void Revert()
        {
            PurchaseOrder.TotalCost = 0;

            Tenant.PurchaseExpenses.RemoveRange(PurchaseOrder.PurchasePayments
                .SelectMany(s => s.PurchaseExpenses));
        }
    }
}
