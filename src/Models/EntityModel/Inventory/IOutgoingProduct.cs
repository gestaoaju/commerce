/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System;

namespace Gestaoaju.Models.EntityModel.Inventory
{
    public interface IOutgoingProduct
    {
        int ProductId { get; }
        decimal Quantity { get; }
        Guid? StockTransactionId { get; set; }
    }
}
