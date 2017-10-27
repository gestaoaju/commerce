/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Catalog.Products;
using Gestaoaju.Models.EntityModel.Commercial.RentContracts;
using Gestaoaju.Models.EntityModel.Inventory;
using System;

namespace Gestaoaju.Models.EntityModel.Commercial.RentedProducts
{
    public class RentedProduct : ITenantScope, IIncomingProduct, IOutgoingProduct
    {
        public int RentContractId { get; set; }
        public int ProductId { get; set; }
        public int TenantId { get; set; }
        public decimal Quantity { get; set; }
        public decimal? ReturnedQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public decimal? Discount { get; set; }
        public decimal TotalPayable { get; set; }
        public Guid? TransactionIdIn { get; set; }
        public Guid? TransactionIdOut { get; set; }
        public virtual RentContract RentContract { get; set; }
        public virtual Product Product { get; set; }
        public virtual Tenant Tenant { get; set; }
        decimal IOutgoingProduct.Quantity => Quantity;
        decimal IIncomingProduct.Quantity => ReturnedQuantity ?? 0;
        Guid? IOutgoingProduct.TransactionId
        {
            get { return TransactionIdOut; }
            set { TransactionIdOut = value; }
        }
        Guid? IIncomingProduct.TransactionId
        {
            get { return TransactionIdIn; }
            set { TransactionIdIn = value; }
        }
    }
}
