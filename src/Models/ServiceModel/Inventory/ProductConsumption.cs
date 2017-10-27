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
    public class ProductConsumption
    {
        public TenantDbContext Tenant { get; private set; }
        public IOutgoingOrder Order { get; private set; }
        public IEnumerable<Product> Products { get; private set; }
        public IEnumerable<ProductQuantity> ProductQuantities { get; private set; }
        public bool InsufficientBalance { get; set; }

        public ProductConsumption(TenantDbContext tenant, IOutgoingOrder order)
        {
            Tenant = tenant;
            Order = order;
        }

        private async Task RetrieveProductsAndQuantities()
        {
            IEnumerable<int> productIds = Order.OutgoingList.Select(p => p.ProductId);

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

        private void CheckQuantityAvailableFor(IOutgoingProduct outgoing)
        {
            ProductQuantity productQuantity = ProductQuantities
                .SingleOrDefault(p => p.Id == outgoing.ProductId);

            InsufficientBalance = 
                productQuantity == null ||
                productQuantity.TotalAvailable < outgoing.Quantity;
        }

        private void RegisterStockMovementFor(IOutgoingProduct outgoing)
        {
            outgoing.TransactionId = Guid.NewGuid();

            Tenant.ProductMovements.Add(new ProductMovement
            {
                StoreId = Order.StoreId,
                Date = Order.Date,
                TransactionId = outgoing.TransactionId.Value,
                ProductId = outgoing.ProductId,
                Quantity = outgoing.Quantity * (-1)
            });
        }

        public async Task<bool> Confirm()
        {
            await RetrieveProductsAndQuantities();
            
            foreach (Product product in Products)
            {
                if (product.InventoryControl == InventoryControl.Unit)
                {
                    IOutgoingProduct outgoing = Order.OutgoingList
                        .Single(p => p.ProductId == product.Id);

                    CheckQuantityAvailableFor(outgoing);

                    if (InsufficientBalance) return false;

                    RegisterStockMovementFor(outgoing);
                }
            }

            return true;
        }

        public async Task Revert()
        {
            IEnumerable<Guid> transactionIds = Order.OutgoingList
                .Where(outgoing => outgoing.TransactionId != null)
                .Select(outgoing => outgoing.TransactionId.Value)
                .ToList();

            foreach (IOutgoingProduct outgoing in Order.OutgoingList)
            {
                outgoing.TransactionId = null;
            }

            ICollection<ProductMovement> productMovements = await Tenant.ProductMovements
                .WhereTransactionIdIn(transactionIds)
                .ToListAsync();

            Tenant.ProductMovements.RemoveRange(productMovements);
        }
    }
}
