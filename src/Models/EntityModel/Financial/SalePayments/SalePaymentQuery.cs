/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System.Linq;

namespace Gestaoaju.Models.EntityModel.Financial.SalePayments
{
    public static class SalePaymentQuery
    {
        public static IQueryable<SalePayment> ForSaleOrderId(this IQueryable<SalePayment> salePayments, int saleOrderId)
        {
            return salePayments.Where(salePayment => salePayment.SaleOrderId == saleOrderId);
        }
    }
}
