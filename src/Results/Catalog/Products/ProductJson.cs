/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Catalog.Products;
using Gestaoaju.Models.EntityModel.Inventory;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gestaoaju.Results.Catalog.Products
{
    public class ProductJson : IActionResult
    {
        public ProductJson() { }

        public ProductJson(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Code = product.Code;
            AdditionalInformation = product.AdditionalInformation;
            Marketed = product.Marketed;
            IsManufactured = product.IsManufactured;
            CanFraction = product.CanFraction;
            InventoryControl = product.InventoryControl;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string AdditionalInformation { get; set; }
        public bool Marketed { get; set; }
        public bool IsManufactured { get; set; }
        public bool CanFraction { get; set; }
        public InventoryControl InventoryControl { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}
