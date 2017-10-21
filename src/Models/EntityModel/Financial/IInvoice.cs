/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System;
using System.Collections.Generic;

namespace Gestaoaju.Models.EntityModel.Financial
{
    public interface IInvoice
    {
        DateTime? ConfirmationDate { get; set; }
        decimal Total { get; set; }
        IEnumerable<IPayment> Payments { get; }
    }
}
