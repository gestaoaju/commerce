/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Catalog.Products;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestaoaju.Results.Catalog.Products
{
    public class ProductListJson : IActionResult
    {
        public ProductListJson(IEnumerable<Product> products)
        {
            Products = products.Select(product => new ProductJson(product)).ToList();
        }

        public ICollection<ProductJson> Products { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}
