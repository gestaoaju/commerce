// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Gestaoaju.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Gestaoaju.Extensions
{
    public static class MvcOptionsExtensions
    {
        public static void UseCustomFilters(this MvcOptions options)
        {
            options.Filters.Add(typeof(ErrorHandlerAttribute));
            options.Filters.Add(typeof(ModelValidationAttribute));
        }
    }
}
