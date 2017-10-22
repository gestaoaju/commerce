/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Catalog.ItemPrices;
using System.Linq;
using System.Collections.Generic;

namespace Gestaoaju.Models.EntityModel.Catalog.ServicePrices
{
    public static class ServicePriceQuery
    {
        public static IQueryable<ItemPrice> AsItemPrice(this IQueryable<ServicePrice> servicePrices)
        {
            return servicePrices.Select(servicePrice => new ItemPrice
            {
                TenantId = servicePrice.TenantId,
                StoreId = servicePrice.StoreId,
                ProductId = null,
                ServiceId = servicePrice.ServiceId,
                Name = servicePrice.Service.Name,
                Code = servicePrice.Service.Code,
                Marketed = servicePrice.Service.Marketed,
                CanFraction = servicePrice.Service.CanFraction,
                UnitRentPrice = null,
                UnitSalePrice = servicePrice.UnitPrice
            });
        }

        public static IQueryable<ServicePrice> WhereServiceId(this IQueryable<ServicePrice> servicePrices, int serviceId)
        {
            return servicePrices.Where(servicePrice => servicePrice.ServiceId == serviceId);
        }

        public static IQueryable<ServicePrice> WhereServiceIdIn(this IQueryable<ServicePrice> servicePrices, IEnumerable<int> serviceIds)
        {
            return servicePrices.Where(servicePrice => serviceIds.Contains(servicePrice.ServiceId));
        }

        public static IQueryable<ServicePrice> WhereServiceMarketed(this IQueryable<ServicePrice> servicePrices)
        {
            return servicePrices.Where(servicePrice => servicePrice.Service.Marketed);
        }

        public static IQueryable<ServicePrice> WhereStoreId(this IQueryable<ServicePrice> servicePrices, int storeId)
        {
            return servicePrices.Where(servicePrice => servicePrice.StoreId == storeId);
        }
    }
}
