/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Gestaoaju.Models.EntityModel.Inventory.PurchasedProducts
{
    public static class PurchasedProductQuery
    {
        public static IQueryable<PurchasedProduct> IncludeProduct(this IQueryable<PurchasedProduct> purchasedProducts)
        {
            return purchasedProducts.Include(purchasedProduct => purchasedProduct.Product);
        }

        public static IQueryable<PurchasedProduct> WhereProductId(this IQueryable<PurchasedProduct> purchasedProducts, int? productId)
        {
            return purchasedProducts.Where(purchasedProduct => purchasedProduct.ProductId == productId.Value);
        }

        public static IQueryable<PurchasedProduct> WherePurchaseOrderId(this IQueryable<PurchasedProduct> purchasedProducts, int purchaseOrderId)
        {
            return purchasedProducts.Where(purchasedProduct => purchasedProduct.PurchaseOrderId == purchaseOrderId);
        }
    }
}
