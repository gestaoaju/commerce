/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.EntityFramework;
using Gestaoaju.Infrastructure.Tenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gestaoaju.Infrastructure.Tenancy
{
    public class TenantScopeDbContext : DbContext
    {
        private DbContext context;
        private ITenantScopeProvider tenantScopeProvider;

        public TenantScopeDbContext(DbContext context, ITenantScopeProvider tenantScopeProvider)
        {
            this.context = context;
            this.tenantScopeProvider = tenantScopeProvider;
        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            if (typeof(ITenantScope).IsAssignableFrom(typeof(TEntity)))
            {
                return new FilteredDbSet<TEntity>(context.Set<TEntity>(),
                    new PropertyFilterExpression<TEntity>(nameof(ITenantScope.TenantId), tenantScopeProvider.CurrentId));
            }

            throw new ArgumentException($"The entity '{typeof(TEntity).FullName}' is inaccessible to the tenant scope.");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (EntityEntry entry in context.ChangeTracker.Entries())
            {
                if (entry.Entity is ITenantScope entity)
                {
                    entity.TenantId = tenantScopeProvider.CurrentId;
                }
            }

            return context.SaveChangesAsync();
        }
    }
}
