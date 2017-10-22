/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System.Linq;

namespace Gestaoaju.Models.EntityModel.Financial.RentPayments
{
    public static class RentPaymentQuery
    {
        public static IQueryable<RentPayment> ForRentContractId(this IQueryable<RentPayment> rentPayments, int saleOrderId)
        {
            return rentPayments.Where(rentPayment => rentPayment.RentContractId == saleOrderId);
        }
    }
}
