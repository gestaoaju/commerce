/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Catalog.ItemPrices;
using Gestaoaju.Models.EntityModel.Inventory;
using Gestaoaju.Models.EntityModel.Inventory.ProductQuantities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Gestaoaju.Models.EntityModel.Catalog.Products
{
    public static class ProductQuery
    {
        public static IQueryable<ItemPrice> AsItemPrice(this IQueryable<Product> products, int storeId)
        {
            return products.Select(product => new ItemPrice
            {
                TenantId = product.TenantId,
                StoreId = storeId,
                ProductId = product.Id,
                ServiceId = null,
                Name = product.Name,
                Code = product.Code,
                Marketed = product.Marketed,
                CanFraction = product.CanFraction,
                UnitRentPrice = product.ProductPrices
                    .Where(productPrice => productPrice.StoreId == storeId)
                    .Select(productPrice => productPrice.UnitRentPrice)
                    .FirstOrDefault(),
                UnitSalePrice = product.ProductPrices
                    .Where(productPrice => productPrice.StoreId == storeId)
                    .Select(productPrice => productPrice.UnitSalePrice)
                    .FirstOrDefault()
            });
        }

        public static IQueryable<ProductQuantity> AsProductQuantity(this IQueryable<Product> products, int storeId)
        {
            return products
                .WhereHasInventoryControl(true)
                .Select(product => new ProductQuantity
                {
                    Id = product.Id,
                    Code = product.Code,
                    Name = product.Name,
                    Marketed = product.Marketed,
                    TotalAvailable = product.ProductMovements
                        .Where(productMovement => productMovement.StoreId == storeId)
                        .Sum(productMovement => (decimal?)productMovement.Quantity) ?? 0
                });
        }

        public static IQueryable<Product> IncludePrices(this IQueryable<Product> products)
        {
            return products.Include(product => product.ProductPrices);
        }

        public static IQueryable<Product> OrderByName(this IQueryable<Product> products)
        {
            return products.OrderBy(product => product.Name);
        }
        
        public static IQueryable<Product> WhereId(this IQueryable<Product> products, int id)
        {
            return products.Where(product => product.Id == id);
        }

        public static IQueryable<Product> WhereIdIn(this IQueryable<Product> products, IEnumerable<int> ids)
        {
            return products.Where(product => ids.Contains(product.Id));
        }

        public static IQueryable<Product> WhereNameOrCode(this IQueryable<Product> products, params string[] words)
        {
            if (words.Count() == 1)
            {
                string word = words.First();
                return products.Where(product => product.Code == word || product.Name.Contains(word));
            }

            foreach (string word in words)
            {
                products = products.Where(product => product.Name.Contains(word));
            }

            return products;
        }

        public static IQueryable<Product> WhereMarketed(this IQueryable<Product> products, bool? marketed)
        {
            if (marketed == null)
            {
                return products;
            }

            return products.Where(product => product.Marketed == marketed);
        }
        
        public static IQueryable<Product> WhereHasInventoryControl(this IQueryable<Product> products, bool? hasInventoryControl)
        {
            if (hasInventoryControl == null)
            {
                return products;
            }
            
            return products.Where(product => product.InventoryControl != InventoryControl.None);
        }

        public static IQueryable<Product> WhereInventoryControl(this IQueryable<Product> products, InventoryControl? inventoryControl)
        {
            if (inventoryControl == null)
            {
                return products;
            }

            return products.Where(product => product.InventoryControl == inventoryControl);
        }
    }
}
