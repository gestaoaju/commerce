/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Extensions.Http;
using Gestaoaju.Fakes;
using Gestaoaju.Factories.Account;
using Gestaoaju.Factories.Catalog;
using Gestaoaju.Results;
using Gestaoaju.Results.Catalog.Products;
using Gestaoaju.Models.EntityModel.Catalog.Products;
using Gestaoaju.Models.EntityModel.Inventory;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Gestaoaju.Models.ViewModel.Catalog;

namespace Gestaoaju.Functional.Catalog
{
    public class ProductTest
    {
        [Fact]
        public async Task ShouldCreate()
        {
            var server = new ServerFake();
            var user = server.AppDbContext.CreateUser();
            var viewModel = new CreateProductViewModel().Filled();
            var response = await server.CreateAuthenticatedClient(user).PostAsJsonAsync("products/create", viewModel);
            var json = await response.Content.ReadAsJsonAsync<ProductJson>();
            var product = server.AppDbContext.Products.WhereId(json.Id).SingleOrDefault();

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(product);
            Assert.Equal(viewModel.Name, product.Name);
            Assert.Equal(viewModel.Code, product.Code);
            Assert.Equal(viewModel.Marketed, product.Marketed);
            Assert.Equal(viewModel.IsManufactured, product.IsManufactured);
            Assert.Equal(viewModel.CanFraction, product.CanFraction);
            Assert.Equal(viewModel.InventoryControl, product.InventoryControl);
            Assert.Equal(viewModel.AdditionalInformation, product.AdditionalInformation);
        }

        [Fact]
        public async Task ShouldDelete()
        {
            var server = new ServerFake();
            var user = server.AppDbContext.CreateUser();
            var product = server.AppDbContext.CreateProduct(user.Tenant);
            var viewModel = new ProductIdViewModel().For(product);
            var response = await server.CreateAuthenticatedClient(user).PostAsJsonAsync("products/delete", viewModel);
            var json = await response.Content.ReadAsJsonAsync<ProductJson>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.False(server.AppDbContext.Products.WhereId(json.Id).Any());
        }

        [Fact]
        public async Task ShouldFind()
        {
            var server = new ServerFake();
            var user = server.AppDbContext.CreateUser();
            var product = server.AppDbContext.CreateProduct(user.Tenant);
            var viewModel = new ProductIdViewModel().For(product);
            var response = await server.CreateAuthenticatedClient(user).PostAsJsonAsync("products/find", viewModel);
            var json = await response.Content.ReadAsJsonAsync<ProductJson>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(product);
            Assert.Equal(product.Name, json.Name);
            Assert.Equal(product.Code, json.Code);
            Assert.Equal(product.Marketed, json.Marketed);
            Assert.Equal(product.IsManufactured, json.IsManufactured);
            Assert.Equal(product.CanFraction, json.CanFraction);
            Assert.Equal(product.InventoryControl, json.InventoryControl);
            Assert.Equal(product.AdditionalInformation, json.AdditionalInformation);
        }

        [Fact]
        public async Task ShouldList()
        {
            var server = new ServerFake();
            var user = server.AppDbContext.CreateUser();
            var product = server.AppDbContext.CreateProduct(user.Tenant);
            var anotherProduct = server.AppDbContext.CreateProduct(user.Tenant);
            var viewModel = new ListProductsViewModel();
            var response = await server.CreateAuthenticatedClient(user).PostAsJsonAsync("products/list", viewModel);
            var json = await response.Content.ReadAsJsonAsync<ProductListJson>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(2, json.Products.Count);
        }

        [Fact]
        public async Task ShouldListByCode()
        {
            var server = new ServerFake();
            var user = server.AppDbContext.CreateUser();
            var product = server.AppDbContext.CreateProduct(user.Tenant);
            var anotherProduct = server.AppDbContext.CreateProduct(user.Tenant);
            var viewModel = new ListProductsViewModel().Where(nameOrCode: product.Code);
            var response = await server.CreateAuthenticatedClient(user).PostAsJsonAsync("products/list", viewModel);
            var json = await response.Content.ReadAsJsonAsync<ProductListJson>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.True(json.Products.Where(productJson => productJson.Id == product.Id).Any());
            Assert.Equal(1, json.Products.Count);
        }

        [Fact]
        public async Task ShouldListByName()
        {
            var server = new ServerFake();
            var user = server.AppDbContext.CreateUser();
            var product = server.AppDbContext.CreateProduct(user.Tenant);
            var anotherProduct = server.AppDbContext.CreateProduct(user.Tenant);
            var viewModel = new ListProductsViewModel().Where(nameOrCode: product.Name);
            var response = await server.CreateAuthenticatedClient(user).PostAsJsonAsync("products/list", viewModel);
            var json = await response.Content.ReadAsJsonAsync<ProductListJson>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.True(json.Products.Where(productJson => productJson.Id == product.Id).Any());
            Assert.Equal(1, json.Products.Count);
        }

        [Fact]
        public async Task ShouldListByInventoryControl()
        {
            var server = new ServerFake();
            var user = server.AppDbContext.CreateUser();
            var product = server.AppDbContext.CreateProduct(user.Tenant, inventoryControl: InventoryControl.Unit);
            var anotherProduct = server.AppDbContext.CreateProduct(user.Tenant, inventoryControl: InventoryControl.None);
            var viewModel = new ListProductsViewModel().Where(inventoryControl: product.InventoryControl);
            var response = await server.CreateAuthenticatedClient(user).PostAsJsonAsync("products/list", viewModel);
            var json = await response.Content.ReadAsJsonAsync<ProductListJson>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.True(json.Products.Where(productJson => productJson.Id == product.Id).Any());
            Assert.Equal(1, json.Products.Count);
        }

        [Fact]
        public async Task ShouldUpdate()
        {
            var server = new ServerFake();
            var user = server.AppDbContext.CreateUser();
            var product = server.AppDbContext.CreateProduct(user.Tenant);
            var viewModel = new UpdateProductViewModel().For(product);
            var response = await server.CreateAuthenticatedClient(user).PostAsJsonAsync("products/update", viewModel);
            var json = await response.Content.ReadAsJsonAsync<ProductJson>();
            
            product = server.AppDbContext.Products.WhereId(product.Id).Single();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(viewModel.Name, product.Name);
            Assert.Equal(viewModel.Code, product.Code);
            Assert.Equal(viewModel.Marketed, product.Marketed);
            Assert.Equal(viewModel.IsManufactured, product.IsManufactured);
            Assert.Equal(viewModel.CanFraction, product.CanFraction);
            Assert.Equal(viewModel.InventoryControl, product.InventoryControl);
            Assert.Equal(viewModel.AdditionalInformation, product.AdditionalInformation);
        }

        [Fact]
        public async Task ShouldNotCreateWithInvalidFields()
        {
            
        }

        [Fact]
        public async Task ShouldNotCreateWithoutRequiredFields()
        {
            var server = new ServerFake();
            var user = server.AppDbContext.CreateUser();
            var viewModel = new CreateProductViewModel();
            var response = await server.CreateAuthenticatedClient(user).PostAsJsonAsync("products/create", viewModel);
            var json = await response.Content.ReadAsJsonAsync<ErrorsJson>();
            var errors = new[] { "Name: Required", "InventoryControl: Required" };

            Assert.Equal((HttpStatusCode)422, response.StatusCode);
            Assert.Equal(json.Errors.OrderBy(e => e), errors.OrderBy(e => e));
        }

        [Fact]
        public async Task ShouldNotDeleteProductWithProductMovements()
        {
            
        }

        [Fact]
        public async Task ShouldNotDeleteProductWithPurchaseProducts()
        {
            
        }

        [Fact]
        public async Task ShouldNotDeleteProductWithRentedProducts()
        {
            
        }

        [Fact]
        public async Task ShouldNotUpdateWithInvalidFields()
        {
            
        }

        [Fact]
        public async Task ShouldNotUpdateWithoutRequiredFields()
        {
            
        }
    }
}
