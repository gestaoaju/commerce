// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).'     

using Gestaoaju.Infrastructure.EntityFramework;
using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gestaoaju.Models.EntityModel
{
    public class TenantContext : ApplicationContext
    {
        private ITenantIdProvider tenantIdProvider;

        public TenantContext(DbContextOptions options, ITenantIdProvider tenantIdProvider)
            : base(options)
        {
            this.tenantIdProvider = tenantIdProvider;
        }

        public IQueryable<Tenant> Current => base.Set<Tenant>()
            .Where(tentant => tentant.Id == tenantIdProvider.CurrentId);

        public override DbSet<TEntity> Set<TEntity>()
        {
            if (typeof(ITenantScope).IsAssignableFrom(typeof(TEntity)))
            {
                return new FilteredDbSet<TEntity>(base.Set<TEntity>(),
                    new PropertyFilterExpression<TEntity>(
                        nameof(ITenantScope.TenantId), tenantIdProvider.CurrentId));
            }

            return base.Set<TEntity>();
        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                if (entry.Entity is ITenantScope)
                {
                    ((ITenantScope)entry.Entity).TenantId = tenantIdProvider.CurrentId;
                }
            }

            return base.SaveChangesAsync();
        }
    }
}
