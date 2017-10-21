/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

namespace Gestaoaju.Models.EntityModel.Inventory.ProductQuantities
{
    public class ProductQuantity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Marketed { get; set; }
        public decimal TotalAvailable { get; set; }
    }
}
