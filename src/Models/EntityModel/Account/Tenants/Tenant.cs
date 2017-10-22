/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Account.Users;
using Gestaoaju.Models.EntityModel.Catalog.ProductPrices;
using Gestaoaju.Models.EntityModel.Catalog.Products;
using Gestaoaju.Models.EntityModel.Catalog.ServicePrices;
using Gestaoaju.Models.EntityModel.Catalog.Services;
using Gestaoaju.Models.EntityModel.Commercial.Customers;
using Gestaoaju.Models.EntityModel.Commercial.Partners;
using Gestaoaju.Models.EntityModel.Commercial.PaymentMethodFees;
using Gestaoaju.Models.EntityModel.Commercial.PaymentMethods;
using Gestaoaju.Models.EntityModel.Commercial.RentContracts;
using Gestaoaju.Models.EntityModel.Commercial.RentedProducts;
using Gestaoaju.Models.EntityModel.Commercial.SaleOrders;
using Gestaoaju.Models.EntityModel.Commercial.SaleProducts;
using Gestaoaju.Models.EntityModel.Commercial.SaleServices;
using Gestaoaju.Models.EntityModel.Financial.FixedExpenses;
using Gestaoaju.Models.EntityModel.Financial.OtherCashActivities;
using Gestaoaju.Models.EntityModel.Financial.PurchaseExpenses;
using Gestaoaju.Models.EntityModel.Financial.PurchasePayments;
using Gestaoaju.Models.EntityModel.Financial.RentIncomes;
using Gestaoaju.Models.EntityModel.Financial.RentPayments;
using Gestaoaju.Models.EntityModel.Financial.SaleIncomes;
using Gestaoaju.Models.EntityModel.Financial.SalePayments;
using Gestaoaju.Models.EntityModel.Financial.Wallets;
using Gestaoaju.Models.EntityModel.Inventory.ProductMovements;
using Gestaoaju.Models.EntityModel.Inventory.PurchasedProducts;
using Gestaoaju.Models.EntityModel.Inventory.PurchaseOrders;
using Gestaoaju.Models.EntityModel.Inventory.Suppliers;
using Gestaoaju.Models.EntityModel.Manage.Stores;
using Gestaoaju.Models.EntityModel.Manage.TeamMembers;
using Gestaoaju.Models.EntityModel.Manage.TeamRules;
using Gestaoaju.Models.EntityModel.Manage.Teams;
using System;
using System.Collections.Generic;

namespace Gestaoaju.Models.EntityModel.Account.Tenants
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeactivatedAt { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<FixedExpense> FixedExpenses { get; set; }
        public ICollection<OtherCashActivity> OtherCashActivities { get; set; }
        public ICollection<Partner> Partners { get; set; }
        public ICollection<PaymentMethod> PaymentMethods { get; set; }
        public ICollection<PaymentMethodFee> PaymentMethodFees { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<ProductPrice> ProductPrices { get; set; }
        public ICollection<ProductMovement> ProductMovements { get; set; }
        public ICollection<PurchaseExpense> PurchaseExpenses { get; set; }
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public ICollection<PurchasePayment> PurchasePayments { get; set; }
        public ICollection<PurchasedProduct> PurchasedProducts { get; set; }
        public ICollection<RentContract> RentContracts { get; set; }
        public ICollection<RentPayment> RentPayments { get; set; }
        public ICollection<RentedProduct> RentedProducts { get; set; }
        public ICollection<RentIncome> RentIncomes { get; set; }
        public ICollection<SaleIncome> SaleIncomes { get; set; }
        public ICollection<SaleOrder> SaleOrders { get; set; }
        public ICollection<SalePayment> SalePayments { get; set; }
        public ICollection<SaleProduct> SaleProducts { get; set; }
        public ICollection<SaleService> SaleServices { get; set; }
        public ICollection<Service> Services { get; set; }
        public ICollection<ServicePrice> ServicePrices { get; set; }
        public ICollection<Store> Stores { get; set; }
        public ICollection<Supplier> Suppliers { get; set; }
        public ICollection<Team> Teams { get; set; }
        public ICollection<TeamMember> TeamMembers { get; set; }
        public ICollection<TeamRule> TeamRules { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Wallet> Wallets { get; set; }
    }
}
