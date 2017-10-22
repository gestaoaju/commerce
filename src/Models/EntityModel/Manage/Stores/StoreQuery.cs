/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Gestaoaju.Models.EntityModel.Manage.Stores
{
    public static class StoreQuery
    {
        public static IQueryable<Store> IncludePrices(this IQueryable<Store> stores)
        {
            return stores
                .Include(store => store.ProductPrices)
                .Include(store => store.ServicePrices);
        }

        public static IQueryable<Store> OrderByName(this IQueryable<Store> stores)
        {
            return stores.OrderBy(store => store.Name);
        }

        public static IQueryable<Store> WhereId(this IQueryable<Store> stores, int id)
        {
            return stores.Where(store => store.Id == id);
        }

        public static IQueryable<Store> WhereNameContains(this IQueryable<Store> stores, params string[] words)
        {
            foreach (string word in words)
            {
                stores = stores.Where(store => store.Name.Contains(word));
            }

            return stores;
        }
    }
}
