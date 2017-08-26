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
        private DbSet<TEntity> dbSet;
        private IQueryable<TEntity> query;

        public FilteredDbSet(DbSet<TEntity> baseDbSet, Expression<Func<TEntity, bool>> filter)
        {
            dbSet = baseDbSet;
            query = baseDbSet.Where(filter);
        }

        Type IQueryable.ElementType => typeof(TEntity);

        Expression IQueryable.Expression => query.Expression;

        IQueryProvider IQueryable.Provider => query.Provider;

        IAsyncEnumerable<TEntity> IAsyncEnumerableAccessor<TEntity>.AsyncEnumerable =>
            ((IAsyncEnumerableAccessor<TEntity>)query).AsyncEnumerable;

        IServiceProvider IInfrastructure<IServiceProvider>.Instance =>
            ((IInfrastructure<IServiceProvider>)dbSet).Instance;

        public override LocalView<TEntity> Local => dbSet.Local;

        public IEnumerator<TEntity> GetEnumerator() => query.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => query.GetEnumerator();

        public override TEntity Find(params object[] keyValues) => dbSet.Find(keyValues);

        public override Task<TEntity> FindAsync(params object[] keyValues) => dbSet.FindAsync(keyValues);

        public override Task<TEntity> FindAsync(object[] keyValues,
            CancellationToken cancellationToken) => dbSet.FindAsync(keyValues, cancellationToken);

        public override EntityEntry<TEntity> Add(TEntity entity) => dbSet.Add(entity);

        public override Task<EntityEntry<TEntity>> AddAsync(TEntity entity,
            CancellationToken cancellationToken = default(CancellationToken)) =>
                dbSet.AddAsync(entity, cancellationToken);
        
        public override EntityEntry<TEntity> Attach(TEntity entity) => dbSet.Attach(entity);

        public override EntityEntry<TEntity> Remove(TEntity entity) => dbSet.Remove(entity);

        public override EntityEntry<TEntity> Update(TEntity entity) => dbSet.Update(entity);

        public override void AddRange(params TEntity[] entities) => dbSet.AddRange(entities);

        public override Task AddRangeAsync(params TEntity[] entities) => dbSet.AddRangeAsync(entities);

        public override void AttachRange(params TEntity[] entities) => dbSet.AttachRange(entities);

        public override void RemoveRange(params TEntity[] entities) => dbSet.RemoveRange(entities);

        public override void UpdateRange(params TEntity[] entities) => dbSet.UpdateRange(entities);

        public override void AddRange(IEnumerable<TEntity> entities) => dbSet.AddRange(entities);

        public override Task AddRangeAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default(CancellationToken)) =>
                dbSet.AddRangeAsync(entities, cancellationToken);
        
        public override void AttachRange(IEnumerable<TEntity> entities) => dbSet.AttachRange(entities);

        public override void RemoveRange(IEnumerable<TEntity> entities) => dbSet.RemoveRange(entities);

        public override void UpdateRange(IEnumerable<TEntity> entities) => dbSet.UpdateRange(entities);
    }
}
