/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Inventory;

namespace Gestaoaju.Models.ViewModel.Catalog
{
    public class ListProductsViewModel
    {
        public string NameOrCode { get; set; }
        public InventoryControl? InventoryControl { get; set; }
        public int Index { get; set; }
        public int? Limit { get; set; }
    }
}
