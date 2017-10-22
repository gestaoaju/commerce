/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System.Linq;

namespace Gestaoaju.Models.EntityModel.Inventory.Suppliers
{
    public static class SupplierQuery
    {
        public static IQueryable<Supplier> OrderedByName(this IQueryable<Supplier> suppliers)
        {
            return suppliers.OrderBy(supplier => supplier.Name);
        }

        public static IQueryable<Supplier> WhereId(this IQueryable<Supplier> suppliers, int id)
        {
            return suppliers.Where(supplier => supplier.Id == id);
        }

        public static IQueryable<Supplier> WhereNameContains(this IQueryable<Supplier> suppliers, params string[] words)
        {
            foreach (string word in words)
            {
                suppliers = suppliers.Where(supplier => supplier.Name.Contains(word));
            }

            return suppliers;
        }
    }
}
