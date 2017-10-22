/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Gestaoaju.Models.EntityModel.Commercial.RentedProducts
{
    public static class RentedProductQuery
    {
        public static IQueryable<RentedProduct> IncludeProduct(this IQueryable<RentedProduct> rentedProducts)
        {
            return rentedProducts.Include(rentedProduct => rentedProduct.Product);
        }

        public static IQueryable<RentedProduct> WhereProductId(this IQueryable<RentedProduct> rentedProducts, int? productId)
        {
            return rentedProducts.Where(rentedProduct => rentedProduct.ProductId == productId.Value);
        }

        public static IQueryable<RentedProduct> WhereRentContractId(this IQueryable<RentedProduct> rentedProducts, int rentContractId)
        {
            return rentedProducts.Where(rentedProduct => rentedProduct.RentContractId == rentContractId);
        }
    }
}
