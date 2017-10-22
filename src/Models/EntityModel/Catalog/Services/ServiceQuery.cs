/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Catalog.ItemPrices;
using System.Linq;

namespace Gestaoaju.Models.EntityModel.Catalog.Services
{
    public static class ServiceQuery
    {
        public static IQueryable<ItemPrice> AsItemPrice(this IQueryable<Service> services, int storeId)
        {
            return services.Select(service => new ItemPrice
            {
                TenantId = service.TenantId,
                StoreId = storeId,
                ProductId = null,
                ServiceId = service.Id,
                Name = service.Name,
                Code = service.Code,
                Marketed = service.Marketed,
                CanFraction = service.CanFraction,
                UnitRentPrice = null,
                UnitSalePrice = service.ServicePrices
                    .Where(servicePrice => servicePrice.StoreId == storeId)
                    .Select(servicePrice => (decimal?)servicePrice.UnitPrice)
                    .FirstOrDefault()
            });
        }

        public static IQueryable<Service> OrderByName(this IQueryable<Service> products)
        {
            return products.OrderBy(product => product.Name);
        }

        public static IQueryable<Service> WhereNameOrCode(this IQueryable<Service> services, params string[] words)
        {
            if (words.Count() == 1)
            {
                string word = words.First();
                return services.Where(service => service.Code == word || service.Name.Contains(word));
            }

            foreach (string word in words)
            {
                services = services.Where(service => service.Name.Contains(word));
            }

            return services;
        }

        public static IQueryable<Service> WhereId(this IQueryable<Service> products, int id)
        {
            return products.Where(product => product.Id == id);
        }
    }
}
