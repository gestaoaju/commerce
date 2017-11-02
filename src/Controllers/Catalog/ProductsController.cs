/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Extensions;
using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Catalog.Products;
using Gestaoaju.Models.ViewModel.Catalog;
using Gestaoaju.Results.Catalog.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Gestaoaju.Controllers.Catalog
{
    [Route("products")]
    public class ProductsController : Controller
    {
        private TenantDbContext context;

        public ProductsController(TenantDbContext context)
        {
            this.context = context;
        }

        [HttpGet, Route("new")]
        public IActionResult Add() => View("~/Views/Catalog/Products/ProductEdit.cshtml");

        [HttpGet, Route("{id:int}/edit")]
        public IActionResult Edit() => View("~/Views/Catalog/Products/ProductEdit.cshtml");

        [HttpGet, Route("products")]
        public IActionResult List() => View("~/Views/Catalog/Products/ProductList.cshtml");

        [HttpPost, Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateProductViewModel viewModel)
        {
            var product = context.Products.Add(viewModel.MapTo(new Product()));
            await context.SaveChangesAsync();

            return new ProductJson(product.Entity);
        }

        [HttpPost, Route("delete")]
        public async Task<IActionResult> Delete([FromBody] ProductIdViewModel viewModel)
        {
            Product product = await context.Products
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (product == null)
            {
                return new ProductNotFoundJson();
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return new ProductJson(product);
        }

        [HttpPost, Route("find")]
        public async Task<IActionResult> Find([FromBody] ProductIdViewModel viewModel)
        {
            Product product = await context.Products
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (product == null)
            {
                return new ProductNotFoundJson();
            }

            return new ProductJson(product);
        }

        [HttpPost, Route("list")]
        public async Task<IActionResult> List([FromBody] ListProductsViewModel viewModel)
        {
            ICollection<Product> products = await context.Products
                .OrderByName()
                .WhereNameOrCode(viewModel.NameOrCode.Words())
                .WhereInventoryControl(viewModel.InventoryControl)
                .Paginate(viewModel.Index, viewModel.Limit)
                .ToListAsync();

            return new ProductListJson(products);
        }

        [HttpPost, Route("update")]
        public async Task<IActionResult> Update([FromBody] UpdateProductViewModel viewModel)
        {
            Product product = await context.Products
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (product == null)
            {
                return new ProductNotFoundJson();
            }

            viewModel.MapTo(product);
            await context.SaveChangesAsync();

            return new ProductJson(product);
        }
    }
}
