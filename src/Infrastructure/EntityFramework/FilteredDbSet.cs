// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).'     

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Gestaoaju.Infrastructure.EntityFramework
{
    public class FilteredDbSet<TEntity> : DbSet<TEntity>, IQueryable<TEntity>,
        IAsyncEnumerableAccessor<TEntity>, IInfrastructure<IServiceProvider>
        where TEntity : class
    {
        private DbSet<TEntity> set;
        private IQueryable<TEntity> query;

        public FilteredDbSet(DbSet<TEntity> baseSet, Expression<Func<TEntity, bool>> filter)
        {
            set = baseSet;
            query = baseSet.Where(filter);
        }

        Type IQueryable.ElementType => typeof(TEntity);

        Expression IQueryable.Expression => query.Expression;

        IQueryProvider IQueryable.Provider => query.Provider;

        IAsyncEnumerable<TEntity> IAsyncEnumerableAccessor<TEntity>.AsyncEnumerable =>
            ((IAsyncEnumerableAccessor<TEntity>)query).AsyncEnumerable;

        IServiceProvider IInfrastructure<IServiceProvider>.Instance =>
            ((IInfrastructure<IServiceProvider>)set).Instance;

        public override LocalView<TEntity> Local => set.Local;

        public IEnumerator<TEntity> GetEnumerator() => query.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => query.GetEnumerator();

        public override TEntity Find(params object[] keyValues) => set.Find(keyValues);

        public override Task<TEntity> FindAsync(params object[] keyValues) => set.FindAsync(keyValues);

        public override Task<TEntity> FindAsync(object[] keyValues,
            CancellationToken cancellationToken) => set.FindAsync(keyValues, cancellationToken);

        public override EntityEntry<TEntity> Add(TEntity entity) => set.Add(entity);

        public override Task<EntityEntry<TEntity>> AddAsync(TEntity entity,
            CancellationToken cancellationToken = default(CancellationToken)) =>
                set.AddAsync(entity, cancellationToken);
        
        public override EntityEntry<TEntity> Attach(TEntity entity) => set.Attach(entity);

        public override EntityEntry<TEntity> Remove(TEntity entity) => set.Remove(entity);

        public override EntityEntry<TEntity> Update(TEntity entity) => set.Update(entity);

        public override void AddRange(params TEntity[] entities) => set.AddRange(entities);

        public override Task AddRangeAsync(params TEntity[] entities) => set.AddRangeAsync(entities);

        public override void AttachRange(params TEntity[] entities) => set.AttachRange(entities);

        public override void RemoveRange(params TEntity[] entities) => set.RemoveRange(entities);

        public override void UpdateRange(params TEntity[] entities) => set.UpdateRange(entities);

        public override void AddRange(IEnumerable<TEntity> entities) => set.AddRange(entities);

        public override Task AddRangeAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default(CancellationToken)) =>
                set.AddRangeAsync(entities, cancellationToken);
        
        public override void AttachRange(IEnumerable<TEntity> entities) => set.AttachRange(entities);

        public override void RemoveRange(IEnumerable<TEntity> entities) => set.RemoveRange(entities);

        public override void UpdateRange(IEnumerable<TEntity> entities) => set.UpdateRange(entities);
    }
}
