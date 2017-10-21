/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

namespace Gestaoaju.Models.EntityModel.Commercial.SaleItems
{
    public class SaleItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public decimal? Discount { get; set; }
        public decimal TotalPayable { get; set; }
        public bool CanFraction { get; set; }
        public bool IsProduct { get; set; }
        public bool IsService { get; set; }
    }
}
