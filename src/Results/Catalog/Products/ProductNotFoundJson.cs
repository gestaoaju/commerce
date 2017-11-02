/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gestaoaju.Results.Catalog.Products
{
    public class ProductNotFoundJson : IActionResult
    {
        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new ErrorsJson("Produto não encontrado").ExecuteResultAsync(context);
        }
    }
}