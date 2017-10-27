/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Commercial.RentContracts;
using Gestaoaju.Models.EntityModel.Financial;
using Gestaoaju.Models.EntityModel.Financial.RentPayments;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestaoaju.Models.ServiceModel.Financial
{
    public class RentPayments
    {
        public TenantDbContext Tenant { get; private set; }
        public RentContract RentContract { get; private set; }
        public bool HasPendingPayment { get; set; }

        public RentPayments(TenantDbContext tenant)
        {
            Tenant = tenant;
        }

        private void CalculateTotals(IEnumerable<RentPayment> payments, decimal? discount)
        {
            decimal totalPaid = payments.Sum(payment => payment.Total);

            RentContract.Discount = discount;
            RentContract.TotalPayable = new Money(RentContract.Total)
                .SubtractPercentage(RentContract.Discount ?? 0);

            HasPendingPayment = totalPaid != RentContract.TotalPayable;
        }

        public async Task<bool> RegisterPayments(int rentContractId, IEnumerable<RentPayment> payments, decimal? discount)
        {
            RentContract = await Tenant.RentContracts
                .WhereId(rentContractId)
                .IncludeRentPayments()
                .SingleOrDefaultAsync();

            if (RentContract == null || RentContract.Confirmed)
            {
                return false;
            }

            CalculateTotals(payments, discount);

            if (HasPendingPayment)
            {
                return false;
            }

            Tenant.RentPayments.RemoveRange(RentContract.RentPayments);
            Tenant.RentPayments.AddRange(payments);

            await Tenant.SaveChangesAsync();

            return true;
        }
    }
}
