/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Catalog.Products;
using Gestaoaju.Models.EntityModel.Inventory;
using Gestaoaju.Models.EntityModel.Inventory.ProductMovements;
using Gestaoaju.Models.EntityModel.Inventory.ProductQuantities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestaoaju.Models.ServiceModel.Inventory
{
    public class ProductReplenishment
    {
        public TenantDbContext Tenant { get; private set; }
        public IIncomingOrder Order { get; private set; }
        public IEnumerable<Product> Products { get; private set; }
        public IEnumerable<ProductQuantity> ProductQuantities { get; private set; }
        public bool HasProductThatNotMovementStock { get; private set; }
        public bool InsufficientBalance { get; set; }

        public ProductReplenishment(TenantDbContext tenant, IIncomingOrder order)
        {
            Tenant = tenant;
            Order = order;
        }

        private async Task RetrieveProductsAndQuantities()
        {
            IEnumerable<int> productIds = Order.IncomingList.Select(p => p.ProductId);

            Products = await Tenant.Products
                .WhereIdIn(productIds)
                .ToListAsync();

            ProductQuantities = await Tenant.ProductMovements
                .WhereStoreId(Order.StoreId)
                .WhereProductIdIn(productIds)
                .WhereUntilDate(Order.Date)
                .AsProductQuantity()
                .ToListAsync();
        }

        private void CheckQuantityAvailableForRevert(IIncomingProduct incoming)
        {
            ProductQuantity productQuantity = ProductQuantities
                .SingleOrDefault(p => p.Id == incoming.ProductId);

            InsufficientBalance =
                productQuantity == null ||
                productQuantity.TotalAvailable < incoming.Quantity;
        }

        public async Task<bool> Confirm()
        {
            Products = await Tenant.Products
                .WhereIdIn(Order.IncomingList.Select(p => p.ProductId))
                .ToListAsync();

            foreach (Product product in Products)
            {
                if (HasProductThatNotMovementStock = product.InventoryControl == InventoryControl.None)
                {
                    return false;
                }

                IIncomingProduct incoming = Order.IncomingList
                    .Single(p => p.ProductId == product.Id);

                incoming.TransactionId = Guid.NewGuid();

                Tenant.ProductMovements.Add(new ProductMovement
                {
                    StoreId = Order.StoreId,
                    Date = Order.Date,
                    TransactionId = incoming.TransactionId.Value,
                    ProductId = incoming.ProductId,
                    Quantity = incoming.Quantity
                });
            }

            return true;
        }

        public async Task<bool> Revert()
        {
            await RetrieveProductsAndQuantities();

            IEnumerable<Guid> transactionIds = Order.IncomingList
                .Where(incoming => incoming.TransactionId != null)
                .Select(incoming => incoming.TransactionId.Value)
                .ToList();

            foreach (IIncomingProduct incoming in Order.IncomingList)
            {
                incoming.TransactionId = null;

                CheckQuantityAvailableForRevert(incoming);

                if (InsufficientBalance) return false;
            }

            ICollection<ProductMovement> productMovements = await Tenant.ProductMovements
                .WhereTransactionIdIn(transactionIds)
                .ToListAsync();

            Tenant.ProductMovements.RemoveRange(productMovements);

            return true;
        }
    }
}
