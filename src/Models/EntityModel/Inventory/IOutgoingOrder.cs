/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System;
using System.Collections.Generic;

namespace Gestaoaju.Models.EntityModel.Inventory
{
    public interface IOutgoingOrder
    {
        int StoreId { get; }
        DateTime Date { get; }
        IEnumerable<IOutgoingProduct> OutgoingList { get; }
    }
}
