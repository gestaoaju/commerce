/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Account.ClosureRequests;
using Gestaoaju.Models.EntityModel.Account.PasswordRecoveries;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Account.Users;
using Microsoft.EntityFrameworkCore;

namespace Gestaoaju.Models.EntityModel
{
    public class AppDbContext : DbContext
    {
        public DbSet<ClosureRequest> ClosureRequests => Set<ClosureRequest>();
        public DbSet<PasswordRecovery> PasswordRecoveries => Set<PasswordRecovery>();
        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<User> Users => Set<User>();

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClosureRequest>().Map();
            modelBuilder.Entity<PasswordRecovery>().Map();
            modelBuilder.Entity<Tenant>().Map();
            modelBuilder.Entity<User>().Map();
        }
    }
}
