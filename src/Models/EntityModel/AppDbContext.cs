/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System.Linq;
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

namespace Gestaoaju.Models.EntityModel
{
    public class AppDbContext : DbContext
    {
        public DbSet<ClosureRequest> ClosureRequests  => Set<ClosureRequest>();
        public DbSet<Customer> Customers  => Set<Customer>();
        public DbSet<FixedExpense> FixedExpenses  => Set<FixedExpense>();
        public DbSet<OtherCashActivity> OtherCashActivities  => Set<OtherCashActivity>();
        public DbSet<Partner> Partners  => Set<Partner>();
        public DbSet<PasswordRecovery> PasswordRecoveries  => Set<PasswordRecovery>();
        public DbSet<PaymentMethod> PaymentMethods  => Set<PaymentMethod>();
        public DbSet<PaymentMethodFee> PaymentMethodFees  => Set<PaymentMethodFee>();
        public DbSet<Product> Products  => Set<Product>();
        public DbSet<ProductMovement> ProductMovements  => Set<ProductMovement>();
        public DbSet<ProductPrice> ProductPrices  => Set<ProductPrice>();
        public DbSet<PurchaseExpense> PurchaseExpenses  => Set<PurchaseExpense>();
        public DbSet<PurchasedProduct> PurchasedProducts  => Set<PurchasedProduct>();
        public DbSet<PurchaseOrder> PurchaseOrders  => Set<PurchaseOrder>();
        public DbSet<PurchasePayment> PurchasePayments  => Set<PurchasePayment>();
        public DbSet<RentContract> RentContracts  => Set<RentContract>();
        public DbSet<RentIncome> RentIncomes  => Set<RentIncome>();
        public DbSet<RentedProduct> RentedProducts  => Set<RentedProduct>();
        public DbSet<RentPayment> RentPayments  => Set<RentPayment>();
        public DbSet<SaleOrder> SaleOrders  => Set<SaleOrder>();
        public DbSet<SaleIncome> SaleIncomes  => Set<SaleIncome>();
        public DbSet<SalePayment> SalePayments  => Set<SalePayment>();
        public DbSet<SaleProduct> SaleProducts  => Set<SaleProduct>();
        public DbSet<SaleService> SaleServices  => Set<SaleService>();
        public DbSet<Service> Services  => Set<Service>();
        public DbSet<ServicePrice> ServicePrices  => Set<ServicePrice>();
        public DbSet<Store> Stores  => Set<Store>();
        public DbSet<Supplier> Suppliers  => Set<Supplier>();
        public DbSet<Team> Teams  => Set<Team>();
        public DbSet<TeamMember> TeamMembers  => Set<TeamMember>();
        public DbSet<TeamRule> TeamRules  => Set<TeamRule>();
        public DbSet<Tenant> Tenants  => Set<Tenant>();
        public DbSet<User> Users  => Set<User>();
        public DbSet<Wallet> Wallets  => Set<Wallet>();
        public IQueryable<ItemPrice> ItemPrices => ProductPrices.AsItemPrice().Concat(ServicePrices.AsItemPrice());

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
