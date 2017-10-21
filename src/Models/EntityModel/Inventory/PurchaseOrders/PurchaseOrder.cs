/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Financial;
using Gestaoaju.Models.EntityModel.Financial.Wallets;
using Gestaoaju.Models.EntityModel.Inventory;
using Gestaoaju.Models.EntityModel.Manage.Stores;
using Gestaoaju.Models.EntityModel.Inventory.Suppliers;
using Gestaoaju.Models.EntityModel.Inventory.PurchasedProducts;
using Gestaoaju.Models.EntityModel.Inventory.PurchasePayments;
using System;
using System.Collections.Generic;

namespace Gestaoaju.Models.EntityModel.Inventory.PurchaseOrders
{
    public class PurchaseOrder : ITenantScope, IIncomingOrder
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int StoreId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public int? SupplierId { get; set; }
        public int? WalletId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Total { get; set; }
        public decimal? Discount { get; set; }
        public decimal? ShippingCost { get; set; }
        public decimal? OtherTaxes { get; set; }
        public decimal TotalPayable { get; set; }
        public decimal TotalCost { get; set; }
        public string Author { get; set; }
        public string AdditionalInformation { get; set; }
        public bool Confirmed { get { return ConfirmationDate != null; } }
        public bool Budget => !Confirmed && Total > 0 && new Money(Total)
            .SubtractPercentage(Discount ?? 0) == TotalPayable;
        public bool Draft => !Confirmed && (Total == 0 || new Money(Total)
            .SubtractPercentage(Discount ?? 0) != TotalPayable);
        DateTime IIncomingOrder.Date => IssueDate;
        IEnumerable<IIncomingProduct> IIncomingOrder.IncomingList => PurchasedProducts;
        public Tenant Tenant { get; set; }
        public Store Store { get; set; }
        public Supplier Supplier { get; set; }
        public Wallet Wallet { get; set; }
        public ICollection<PurchasedProduct> PurchasedProducts { get; set; }
        public ICollection<PurchasePayment> PurchasePayments { get; set; }
    }
}
