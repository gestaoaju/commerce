// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).'     

using System;
using System.Linq.Expressions;

namespace Gestaoaju.Infrastructure.EntityFramework
{
    public class PropertyFilterExpression<TEntity> where TEntity : class
    {
        private Expression<Func<TEntity, bool>> expression;

        public PropertyFilterExpression(string propertyName, object propertyValue)
        {
            var parameter = Expression.Parameter(typeof(TEntity));
            var property = Expression.Property(parameter, propertyName);
            var constant = Expression.Constant(propertyValue);
            var equality = Expression.Equal(property, constant);

            expression = Expression.Lambda<Func<TEntity, bool>>(equality, parameter);
        }

        public static implicit operator Expression<Func<TEntity, bool>>(
            PropertyFilterExpression<TEntity> filter) => filter.expression;
    }
}
