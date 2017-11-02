/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Catalog.Products;
using Gestaoaju.Models.EntityModel.Inventory;
using Gestaoaju.Validations;

namespace Gestaoaju.Models.ViewModel.Catalog
{
    public class CreateProductViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Code { get; set; }
        
        [MaxLength(255)]
        public string AdditionalInformation { get; set; }

        public bool Marketed { get; set; }

        public bool IsManufactured { get; set; }

        [Required]
        public InventoryControl? InventoryControl { get; set; }

        public bool CanFraction { get; set; }

        public Product MapTo(Product product)
        {
            product.Name = Name;
            product.Code = Code;
            product.Marketed = Marketed;
            product.IsManufactured = IsManufactured;
            product.InventoryControl = InventoryControl.Value;
            product.CanFraction = CanFraction;
            product.AdditionalInformation = AdditionalInformation;

            return product;
        }
    }
}
