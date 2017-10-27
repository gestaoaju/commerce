/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Catalog.ItemPrices;
using Gestaoaju.Models.EntityModel.Commercial.SaleItems;
using Gestaoaju.Models.EntityModel.Commercial.SaleOrders;
using Gestaoaju.Models.EntityModel.Financial;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestaoaju.Models.ServiceModel.Commercial
{
    public class SaleShoppingCart
    {
        public TenantDbContext Tenant { get; private set; }
        public SaleOrder SaleOrder { get; private set; }
        public bool HasOneOrMoreItemsNotFound { get; private set; }
        public bool HasOneOrMorePricesNotDefined { get; private set; }
        public bool HasItemWithTotalNegative { get; private set; }

        public SaleShoppingCart(TenantDbContext tenant)
        {
            Tenant = tenant;
        }

        private bool CanFractionOf(SaleItem item, IEnumerable<ItemPrice> priceList)
        {
            var itemPrice = priceList.Single(price =>
                price.ItemId == item.Id &&
                price.IsProduct == item.IsProduct &&
                price.IsService == item.IsService);

            return itemPrice.CanFraction;
        }

        private decimal PriceOf(SaleItem item, IEnumerable<ItemPrice> priceList)
        {
            var itemPrice = priceList.Single(price =>
                price.ItemId == item.Id &&
                price.IsProduct == item.IsProduct &&
                price.IsService == item.IsService);

            return itemPrice.UnitSalePrice.Value;
        }

        private async Task FindSaleOrder(int saleOrderId)
        {
            SaleOrder = await Tenant.SaleOrders
                .WhereId(saleOrderId)
                .IncludeSaleProducts()
                .IncludeSaleServices()
                .SingleOrDefaultAsync();
        }

        private async Task<IEnumerable<ItemPrice>> CheckPriceListFor(IEnumerable<SaleItem> itemList)
        {
            var priceList = await Tenant.ItemPrices
                .WhereStoreId(SaleOrder.StoreId)
                .WhereSaleItemsIn(itemList)
                .WhereItemMarketed()
                .ToListAsync();

            HasOneOrMoreItemsNotFound = priceList.Count() != itemList.Count();
            HasOneOrMorePricesNotDefined = !priceList.Any() ||
                priceList.Any(itemPrice => itemPrice.UnitSalePrice == null);

            return priceList;
        }

        private void CalculateTotals(IEnumerable<SaleItem> itemList, IEnumerable<ItemPrice> priceList)
        {
            foreach (var item in itemList)
            {
                if (!CanFractionOf(item, priceList))
                {
                    item.Quantity = (int)item.Quantity;
                }

                item.SaleOrderId = SaleOrder.Id;
                item.UnitPrice = PriceOf(item, priceList);
                item.Total = new Money(item.UnitPrice).Multiply(item.Quantity);
                item.TotalPayable = new Money(item.Total).Subtract(item.Discount ?? 0);

                HasItemWithTotalNegative = item.TotalPayable < 0;

                if (HasItemWithTotalNegative) return;
            }

            SaleOrder.Total = new Money(itemList.Sum(item => item.TotalPayable));
            SaleOrder.TotalPayable = new Money(SaleOrder.Total).SubtractPercentage(SaleOrder.Discount ?? 0);
        }

        private async Task Save(IEnumerable<SaleItem> itemList)
        {
            var saleProducts = itemList.Where(item => item.IsProduct).Select(item => item.SaleProduct);
            var saleServices = itemList.Where(item => item.IsService).Select(item => item.SaleService);

            Tenant.SaleProducts.RemoveRange(SaleOrder.SaleProducts);
            Tenant.SaleServices.RemoveRange(SaleOrder.SaleServices);
            Tenant.SaleProducts.AddRange(saleProducts);
            Tenant.SaleServices.AddRange(saleServices);

            await Tenant.SaveChangesAsync();
        }

        public async Task<bool> Checkout(int saleOrderId, IEnumerable<SaleItem> itemList)
        {
            await FindSaleOrder(saleOrderId);

            if (SaleOrder == null || SaleOrder.Confirmed)
            {
                return false;
            }

            IEnumerable<ItemPrice> priceList = await CheckPriceListFor(itemList);

            if (HasOneOrMoreItemsNotFound || HasOneOrMorePricesNotDefined)
            {
                return false;
            }

            CalculateTotals(itemList, priceList);

            if (HasItemWithTotalNegative)
            {
                return false;
            }

            await Save(itemList);

            return true;
        }
    }
}
