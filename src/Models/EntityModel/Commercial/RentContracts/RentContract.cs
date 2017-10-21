/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Commercial.Customers;
using Gestaoaju.Models.EntityModel.Commercial.RentedProducts;
using Gestaoaju.Models.EntityModel.Commercial.RentPayments;
using Gestaoaju.Models.EntityModel.Financial;
using Gestaoaju.Models.EntityModel.Financial.Wallets;
using Gestaoaju.Models.EntityModel.Inventory;
using Gestaoaju.Models.EntityModel.Manage.Stores;
using System;
using System.Collections.Generic;

namespace Gestaoaju.Models.EntityModel.Commercial.RentContracts
{
    public class RentContract : ITenantScope, IInvoice, IOutgoingOrder, IIncomingOrder
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int StoreId { get; set; }
        public int? WalletId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? DateOfReturn { get; set; }
        public decimal Total { get; set; }
        public decimal? Discount { get; set; }
        public decimal TotalPayable { get; set; }
        public decimal BilledAmount { get; set; }
        public string Author { get; set; }
        public string Location { get; set; }
        public string AdditionalInformation { get; set; }
        public bool Confirmed => ConfirmationDate != null;
        public bool ReturnPending => Confirmed && DateOfReturn == null;
        public bool Budget =>!Confirmed && Total > 0 && new Money(Total)
            .SubtractPercentage(Discount ?? 0) == TotalPayable;
        public bool Draft => !Confirmed && (Total == 0 || new Money(Total)
            .SubtractPercentage(Discount ?? 0) != TotalPayable);
        public int TotalDays => (int)EndDate.Subtract(StartDate).TotalDays + 1;
        public Customer Customer { get; set; }
        public Tenant Tenant { get; set; }
        public Store Store { get; set; }
        public Wallet Wallet { get; set; }
        public ICollection<RentedProduct> RentedProducts { get; set; }
        public ICollection<RentPayment> RentPayments { get; set; }
        DateTime IOutgoingOrder.Date => StartDate;
        DateTime IIncomingOrder.Date => DateOfReturn ?? EndDate;
        IEnumerable<IOutgoingProduct> IOutgoingOrder.OutgoingList => RentedProducts;
        IEnumerable<IIncomingProduct> IIncomingOrder.IncomingList => RentedProducts;
        decimal IInvoice.Total
        {
            get { return BilledAmount; }
            set { BilledAmount = value; }
        }
        IEnumerable<IPayment> IInvoice.Payments => RentPayments;
    }
}
