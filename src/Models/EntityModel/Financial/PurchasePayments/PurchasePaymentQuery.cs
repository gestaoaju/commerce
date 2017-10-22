/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System.Linq;

namespace Gestaoaju.Models.EntityModel.Financial.PurchasePayments
{
    public static class PurchasePaymentQuery
    {
        public static IQueryable<PurchasePayment> ForPurchaseOrderId(this IQueryable<PurchasePayment> salePayments, int saleOrderId)
        {
            return salePayments.Where(salePayment => salePayment.PurchaseOrderId == saleOrderId);
        }
    }
}
