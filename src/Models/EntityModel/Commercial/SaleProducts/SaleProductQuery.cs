/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Commercial.SaleItems;
using System.Linq;

namespace Gestaoaju.Models.EntityModel.Commercial.SaleProducts
{
    public static class SaleProductQuery
    {
        public static IQueryable<SaleItem> AsSaleItem(this IQueryable<SaleProduct> saleProducts)
        {
            return saleProducts.Select(saleProduct => new SaleItem
            {
                Id = saleProduct.ProductId,
                IsProduct = true,
                IsService = false,
                Name = saleProduct.Product.Name,
                Code = saleProduct.Product.Code,
                CanFraction = saleProduct.Product.CanFraction,
                Quantity = saleProduct.Quantity,
                UnitPrice = saleProduct.UnitPrice,
                Total = saleProduct.Total,
                Discount = saleProduct.Discount,
                TotalPayable = saleProduct.TotalPayable
            });
        }

        public static IQueryable<SaleProduct> WhereProductId(this IQueryable<SaleProduct> saleProducts, int? productId)
        {
            return saleProducts.Where(saleProduct => saleProduct.ProductId == productId.Value);
        }

        public static IQueryable<SaleProduct> WhereSaleOrderId(this IQueryable<SaleProduct> saleProducts, int saleOrderId)
        {
            return saleProducts.Where(saleProduct => saleProduct.SaleOrderId == saleOrderId);
        }
    }
}
