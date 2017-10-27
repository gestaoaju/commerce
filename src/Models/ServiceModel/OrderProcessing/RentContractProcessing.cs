/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Commercial.RentContracts;
using Gestaoaju.Models.EntityModel.Commercial.RentedProducts;
using Gestaoaju.Models.ServiceModel.Financial;
using Gestaoaju.Models.ServiceModel.Inventory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestaoaju.Models.ServiceModel.OrderProcessing
{
    public class RentContractProcessing
    {
        public TenantDbContext Tenant { get; private set; }
        public RentContract RentContract { get; private set; }
        public ProductConsumption ProductConsumption { get; private set; }
        public ProductReplenishment ProductReplenishment { get; private set; }
        public RentBilling Billing { get; private set; }
        public bool HasNoItems { get; private set; }
        public bool HasPendingPayment { get; private set; }
        public bool HasInvalidReturnedQuantity { get; private set; }
        public bool HasInvalidDateOfReturn { get; private set; }

        public RentContractProcessing(TenantDbContext tenant)
        {
            Tenant = tenant;
        }

        private bool RentContractIsPending()
        {
            HasNoItems = RentContract.TotalPayable == 0;
            HasPendingPayment = RentContract.TotalPayable != RentContract.RentPayments.Sum(p => p.Total);

            return HasNoItems || HasPendingPayment;
        }

        private bool SetQuantitiesFor(IEnumerable<RentedProduct> returnedProducts)
        {
            foreach (RentedProduct rentedProduct in RentContract.RentedProducts)
            {
                RentedProduct returnedProduct = returnedProducts
                    .SingleOrDefault(p => p.ProductId == rentedProduct.ProductId);

                HasInvalidReturnedQuantity = returnedProduct == null ||
                    returnedProduct.ReturnedQuantity > rentedProduct.Quantity;
                
                if (HasInvalidReturnedQuantity) return false;

                rentedProduct.ReturnedQuantity = returnedProduct.ReturnedQuantity;
            }

            return true;
        }
        
        public async Task<bool> ProcessDelivery(int rentContractId)
        {
            RentContract = await Tenant.RentContracts
                .WhereId(rentContractId)
                .IncludeStore()
                .IncludeRentedProducts()
                .IncludePaymentMethodsAndFees()
                .SingleOrDefaultAsync();

            if (RentContract == null || RentContract.Confirmed || RentContractIsPending())
            {
                return false;
            }

            ProductConsumption = new ProductConsumption(Tenant, RentContract);
            if (!await ProductConsumption.Confirm()) return false;

            Billing = new RentBilling(Tenant, RentContract);
            Billing.Confirm();

            RentContract.ConfirmationDate = DateTime.UtcNow;

            await Tenant.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ProcessReturn(int rentContractId, DateTime dateOfReturn, IEnumerable<RentedProduct> returnedProducts)
        {
            RentContract = await Tenant.RentContracts
                .WhereId(rentContractId)
                .IncludeStore()
                .IncludeRentedProducts()
                .SingleOrDefaultAsync();

            if (RentContract == null || !RentContract.Confirmed) return false;
            if (HasInvalidDateOfReturn = dateOfReturn < RentContract.StartDate) return false;
            if (!SetQuantitiesFor(returnedProducts)) return false;

            ProductReplenishment = new ProductReplenishment(Tenant, RentContract);
            if (!await ProductReplenishment.Revert()) return false;
            if (!await ProductReplenishment.Confirm()) return false;
            
            RentContract.DateOfReturn = dateOfReturn;

            await Tenant.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Revert(int rentContractId)
        {
            RentContract = await Tenant.RentContracts
                .WhereId(rentContractId)
                .IncludeStore()
                .IncludeRentedProducts()
                .IncludeRentIncomes()
                .SingleOrDefaultAsync();

            if (RentContract == null || !RentContract.Confirmed) return false;

            ProductConsumption = new ProductConsumption(Tenant, RentContract);
            await ProductConsumption.Revert();

            ProductReplenishment = new ProductReplenishment(Tenant, RentContract);
            if (!await ProductReplenishment.Revert()) return false;

            Billing = new RentBilling(Tenant, RentContract);
            Billing.Revert();

            RentContract.ConfirmationDate = null;
            RentContract.DateOfReturn = null;

            await Tenant.SaveChangesAsync();

            return true;
        }
    }
}
