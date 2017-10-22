/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Inventory.ProductQuantities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gestaoaju.Models.EntityModel.Inventory.ProductMovements
{
    public static class ProductMovementQuery
    {
        public static IQueryable<ProductQuantity> AsProductQuantity(this IQueryable<ProductMovement> movements)
        {
            return movements
                .GroupBy(movement => movement.ProductId)
                .Select(productGroup => new ProductQuantity
                {
                    Id = productGroup.FirstOrDefault().Product.Id,
                    Code = productGroup.FirstOrDefault().Product.Code,
                    Name = productGroup.FirstOrDefault().Product.Name,
                    Marketed = productGroup.FirstOrDefault().Product.Marketed,
                    TotalAvailable = productGroup.Sum(product => product.Quantity)
                });
        }

        public static IQueryable<ProductMovement> WhereProductId(this IQueryable<ProductMovement> movements, int? productId)
        {
            if (productId == null)
            {
                return movements;
            }

            return movements.Where(movement => movement.ProductId == productId);
        }

        public static IQueryable<ProductMovement> WhereProductIdIn(this IQueryable<ProductMovement> movements, IEnumerable<int> productIds)
        {
            return movements.Where(movement => productIds.Contains(movement.ProductId));
        }

        public static IQueryable<ProductMovement> WhereStoreId(this IQueryable<ProductMovement> movements, int? storeId)
        {
            if (storeId == null)
            {
                return movements;
            }

            return movements.Where(movement => movement.StoreId == storeId);
        }

        public static IQueryable<ProductMovement> WhereTransactionIdIn(this IQueryable<ProductMovement> movements, IEnumerable<Guid> transactionIds)
        {
            return movements.Where(movement => transactionIds.Contains(movement.TransactionId));
        }

        public static IQueryable<ProductMovement> WhereUntilDate(this IQueryable<ProductMovement> movements, DateTime date)
        {
            date = date.Date;
            return movements.Where(movement => movement.Date <= date);
        }
    }
}
