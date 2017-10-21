/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel.Inventory;

namespace Gestaoaju.Models.EntityModel.Catalog.Products
{
    public class Product : ITenantScope
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string AdditionalInformation { get; set; }
        public bool Marketed { get; set; }
        public bool IsManufactured { get; set; }
        public bool CanFraction { get; set; }
        public InventoryControl InventoryControl { get; set; }
    }
}
