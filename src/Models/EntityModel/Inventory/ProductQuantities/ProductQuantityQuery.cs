/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System.Linq;

namespace Gestaoaju.Models.EntityModel.Inventory.ProductQuantities
{
    public static class ProductQuantityQuery
    {
        public static IQueryable<ProductQuantity> OrderByName(this IQueryable<ProductQuantity> products)
        {
            return products.OrderBy(product => product.Name);
        }
    }
}
