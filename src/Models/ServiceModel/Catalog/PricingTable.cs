/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Catalog.ProductPrices;
using Gestaoaju.Models.EntityModel.Catalog.ServicePrices;
using Gestaoaju.Models.EntityModel.Catalog.ItemPrices;
using Gestaoaju.Models.EntityModel.Manage.Stores;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestaoaju.Models.ServiceModel.Catalog
{
    public class PricingTable
    {
        public TenantDbContext Tenant { get; private set; }
        public int StoreId { get; private set; }

        public PricingTable(TenantDbContext tenant, int storeId)
        {
            Tenant = tenant;
            StoreId = storeId;
        }

        private async Task<Store> FindStoreAndPrices(int storeId)
        {
            return await Tenant.Stores
                .IncludePrices()
                .WhereId(storeId)
                .SingleOrDefaultAsync();
        }

        private IEnumerable<ProductPrice> CopyPricesOf(IEnumerable<ProductPrice> productPrices)
        {
            foreach (var productPrice in productPrices)
            {
                yield return new ProductPrice
                {
                    StoreId = StoreId,
                    ProductId = productPrice.ProductId,
                    UnitSalePrice = productPrice.UnitSalePrice
                };
            }
        }

        private IEnumerable<ServicePrice> CopyPricesOf(IEnumerable<ServicePrice> servicePrices)
        {
            foreach (var servicePrice in servicePrices)
            {
                yield return new ServicePrice
                {
                    StoreId = StoreId,
                    ServiceId = servicePrice.ServiceId,
                    UnitPrice = servicePrice.UnitPrice
                };
            }
        }

        public async Task<bool> Clear()
        {
            var store = await FindStoreAndPrices(StoreId);

            if (store == null)
            {
                return false;
            }

            Tenant.ProductPrices.RemoveRange(store.ProductPrices);
            Tenant.ServicePrices.RemoveRange(store.ServicePrices);

            await Tenant.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CopyFrom(int sourceStoreId)
        {
            var store = await FindStoreAndPrices(StoreId);
            var sourceStore = await FindStoreAndPrices(sourceStoreId);

            if (store == null || sourceStore == null)
            {
                return false;
            }

            Tenant.ProductPrices.RemoveRange(store.ProductPrices);
            Tenant.ServicePrices.RemoveRange(store.ServicePrices);

            Tenant.ProductPrices.AddRange(CopyPricesOf(sourceStore.ProductPrices));
            Tenant.ServicePrices.AddRange(CopyPricesOf(sourceStore.ServicePrices));

            await Tenant.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(IEnumerable<ItemPrice> itemPrices)
        {
            var store = await Tenant.Stores
                .WhereId(StoreId)
                .SingleOrDefaultAsync();

            if (store == null)
            {
                return false;
            }

            var oldProductPrices = await Tenant.ProductPrices
                .WhereStoreId(StoreId)
                .WhereProductIdIn(itemPrices.Where(i => i.IsProduct).Select(p => p.ItemId))
                .ToListAsync();

            var oldServicePrices = await Tenant.ServicePrices
                .WhereStoreId(StoreId)
                .WhereServiceIdIn(itemPrices.Where(i => i.IsService).Select(p => p.ItemId))
                .ToListAsync();

            Tenant.ProductPrices.RemoveRange(oldProductPrices);
            Tenant.ServicePrices.RemoveRange(oldServicePrices);

            var itemsToRentOrSell = itemPrices.Where(itemPrice =>
                itemPrice.UnitRentPrice != null || itemPrice.UnitSalePrice != null);

            var newProductPrices = itemsToRentOrSell
                .Where(itemPrice => itemPrice.IsProduct)
                .Select(itemPrice => itemPrice.ProductPrice);

            var newServicePrices = itemsToRentOrSell
                .Where(itemPrice => itemPrice.IsService)
                .Select(itemPrice => itemPrice.ServicePrice);

            Tenant.ProductPrices.AddRange(newProductPrices);
            Tenant.ServicePrices.AddRange(newServicePrices);

            await Tenant.SaveChangesAsync();

            return true;
        }
    }
}
