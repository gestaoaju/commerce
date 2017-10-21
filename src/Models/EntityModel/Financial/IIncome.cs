/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System;

namespace Gestaoaju.Models.EntityModel.Financial
{
    public interface IIncome
    {
        int PaymentId { get; set; }
        decimal AmountReceived { get; set; }
        DateTime PaymentDate { get; set; }
        DateTime? ReceivedDate { get; set; }
    }
}
