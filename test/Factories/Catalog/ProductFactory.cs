/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Factories.Account;
using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Catalog.Products;
using Gestaoaju.Models.EntityModel.Inventory;
using Gestaoaju.Models.ViewModel.Catalog;

namespace Gestaoaju.Factories.Catalog
{
    public static class ProductFactory
    {
        public static Product BuildProduct(this AppDbContext context)
        {
            var product = new Product();
            product.Code = IdFactory.Id.ToString();
            product.Name = $"Product {product.Code}";
            product.Marketed = true;
            product.IsManufactured = false;
            product.InventoryControl = InventoryControl.None;
            product.CanFraction = true;
            product.AdditionalInformation = $"More information about product {product.Code}";

            return product;
        }

        public static Product CreateProduct(this AppDbContext context, Tenant tenant = null, InventoryControl? inventoryControl = null)
        {
            Product product = context.BuildProduct();
            product.Tenant = tenant ?? context.CreateTenant();

            if (inventoryControl != null)
            {
                product.InventoryControl = inventoryControl.Value;
            }

            context.Products.Add(product);
            context.SaveChanges();

            return product;
        }

        public static CreateProductViewModel Filled(this CreateProductViewModel viewModel)
        {
            viewModel.Code = IdFactory.Id.ToString();
            viewModel.Name = $"Product {viewModel.Code}";
            viewModel.Marketed = true;
            viewModel.IsManufactured = false;
            viewModel.InventoryControl = InventoryControl.None;
            viewModel.CanFraction = true;
            viewModel.AdditionalInformation = $"More information about product {viewModel.Code}";

            return viewModel;
        }

        public static ProductIdViewModel For(this ProductIdViewModel viewModel, Product product)
        {
            viewModel.Id = product.Id;
            return viewModel;
        }

        public static UpdateProductViewModel For(this UpdateProductViewModel viewModel, Product product)
        {
            viewModel.Id = product.Id;
            viewModel.Code = $"{product.Code} [UPDATED]";
            viewModel.Name = $"{product.Name} [UPDATED]";
            viewModel.Marketed = !product.Marketed;
            viewModel.IsManufactured = !product.IsManufactured;
            viewModel.InventoryControl = InventoryControl.SerialNumber;
            viewModel.CanFraction = !product.CanFraction;
            viewModel.AdditionalInformation = $"{product.AdditionalInformation} [UPDATED]";

            return viewModel;
        }

        public static CreateProductViewModel Invalid(this CreateProductViewModel viewModel)
        {
            viewModel.Code = IdFactory.Id.ToString();
            viewModel.Name = $"Product {viewModel.Code}";
            viewModel.Marketed = true;
            viewModel.IsManufactured = false;
            viewModel.InventoryControl = InventoryControl.None;
            viewModel.CanFraction = true;
            viewModel.AdditionalInformation = $"More information about product {viewModel.Code}";

            return viewModel;
        }

        public static ListProductsViewModel Where(this ListProductsViewModel viewModel,
            string nameOrCode = null, InventoryControl? inventoryControl = null)
        {
            viewModel.NameOrCode = nameOrCode;
            viewModel.InventoryControl = inventoryControl;

            return viewModel;
        }
    }
}
