/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Catalog.Products;
using Gestaoaju.Models.EntityModel.Financial;
using Gestaoaju.Models.EntityModel.Inventory.PurchasedProducts;
using Gestaoaju.Models.EntityModel.Inventory.PurchaseOrders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestaoaju.Models.ServiceModel.Inventory
{
    public class ProductPurchasing
    {
        public TenantDbContext Tenant { get; private set; }
        public PurchaseOrder PurchaseOrder { get; private set; }
        public bool HasOneOrMoreProductsNotFound { get; private set; }
        public bool HasProductWithTotalNegative { get; private set; }

        public ProductPurchasing(TenantDbContext tenant)
        {
            Tenant = tenant;
        }
        
        private async Task FindPurchaseOrder(int purchaseOrderId)
        {
            PurchaseOrder = await Tenant.PurchaseOrders
                .WhereId(purchaseOrderId)
                .IncludePurchasedProducts()
                .SingleOrDefaultAsync();
        }

        private async Task CheckProductsIn(IEnumerable<PurchasedProduct> purchasedProductList)
        {
            int productsCount = await Tenant.Products
                .WhereIdIn(purchasedProductList.Select(p => p.ProductId))
                .CountAsync();

            HasOneOrMoreProductsNotFound = purchasedProductList.Count() != productsCount;
        }

        private void CalculateTotalsOf(IEnumerable<PurchasedProduct> purchasedProductList)
        {
            foreach (PurchasedProduct purchasedProduct in purchasedProductList)
            {
                purchasedProduct.Total = new Money(purchasedProduct.UnitCost)
                    .Multiply(purchasedProduct.Quantity);

                purchasedProduct.TotalCost = new Money(purchasedProduct.Total)
                    .Subtract(purchasedProduct.Discount ?? 0);

                HasProductWithTotalNegative = purchasedProduct.TotalCost < 0;

                if (HasProductWithTotalNegative) return;
            }

            PurchaseOrder.Total = new Money(purchasedProductList.Sum(product => product.TotalCost));

            PurchaseOrder.TotalPayable = new Money(PurchaseOrder.Total)
                .SubtractPercentage(PurchaseOrder.Discount ?? 0);
        }

        private async Task Save(IEnumerable<PurchasedProduct> purchasedProductList)
        {
            Tenant.PurchasedProducts.RemoveRange(PurchaseOrder.PurchasedProducts);
            Tenant.PurchasedProducts.AddRange(purchasedProductList);

            await Tenant.SaveChangesAsync();
        }

        public async Task<bool> Approve(int purchaseOrderId, IEnumerable<PurchasedProduct> purchasedProductList)
        {
            await FindPurchaseOrder(purchaseOrderId);

            if (PurchaseOrder == null || PurchaseOrder.Confirmed)
            {
                return false;
            }

            await CheckProductsIn(purchasedProductList);

            if (HasOneOrMoreProductsNotFound)
            {
                return false;
            }

            CalculateTotalsOf(purchasedProductList);

            if (HasProductWithTotalNegative)
            {
                return false;
            }

            await Save(purchasedProductList);

            return true;
        }
    }
}
