/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Account.ClosureRequests;
using Gestaoaju.Models.EntityModel.Account.PasswordRecoveries;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Account.Users;
using Gestaoaju.Models.EntityModel.Catalog.ItemPrices;
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
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Gestaoaju.Models.EntityModel
{
    public class AppDbContext : DbContext
    {
        public DbSet<ClosureRequest> ClosureRequests { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FixedExpense> FixedExpenses { get; set; }
        public DbSet<OtherCashActivity> OtherCashActivities { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<PasswordRecovery> PasswordRecoveries { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentMethodFee> PaymentMethodFees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductMovement> ProductMovements { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<PurchaseExpense> PurchaseExpenses { get; set; }
        public DbSet<PurchasedProduct> PurchasedProducts { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchasePayment> PurchasePayments { get; set; }
        public DbSet<RentContract> RentContracts { get; set; }
        public DbSet<RentIncome> RentIncomes { get; set; }
        public DbSet<RentedProduct> RentedProducts { get; set; }
        public DbSet<RentPayment> RentPayments { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<SaleIncome> SaleIncomes { get; set; }
        public DbSet<SalePayment> SalePayments { get; set; }
        public DbSet<SaleProduct> SaleProducts { get; set; }
        public DbSet<SaleService> SaleServices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServicePrice> ServicePrices { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<TeamRule> TeamRules { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClosureRequest>().Map();
            modelBuilder.Entity<Customer>().Map();
            modelBuilder.Entity<FixedExpense>().Map();
            modelBuilder.Entity<OtherCashActivity>().Map();
            modelBuilder.Entity<Partner>().Map();
            modelBuilder.Entity<PasswordRecovery>().Map();
            modelBuilder.Entity<PaymentMethod>().Map();
            modelBuilder.Entity<PaymentMethodFee>().Map();
            modelBuilder.Entity<Product>().Map();
            modelBuilder.Entity<ProductMovement>().Map();
            modelBuilder.Entity<ProductPrice>().Map();
            modelBuilder.Entity<PurchasedProduct>().Map();
            modelBuilder.Entity<PurchaseExpense>().Map();
            modelBuilder.Entity<PurchaseOrder>().Map();
            modelBuilder.Entity<PurchasePayment>().Map();
            modelBuilder.Entity<RentContract>().Map();
            modelBuilder.Entity<RentIncome>().Map();
            modelBuilder.Entity<RentPayment>().Map();
            modelBuilder.Entity<RentedProduct>().Map();
            modelBuilder.Entity<SaleIncome>().Map();
            modelBuilder.Entity<SaleOrder>().Map();
            modelBuilder.Entity<SalePayment>().Map();
            modelBuilder.Entity<SaleProduct>().Map();
            modelBuilder.Entity<SaleService>().Map();
            modelBuilder.Entity<Service>().Map();
            modelBuilder.Entity<ServicePrice>().Map();
            modelBuilder.Entity<Store>().Map();
            modelBuilder.Entity<Supplier>().Map();
            modelBuilder.Entity<Tenant>().Map();
            modelBuilder.Entity<Team>().Map();
            modelBuilder.Entity<TeamMember>().Map();
            modelBuilder.Entity<TeamRule>().Map();
            modelBuilder.Entity<User>().Map();
            modelBuilder.Entity<Wallet>().Map();
        }
    }
}
