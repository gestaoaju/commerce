/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Commercial.PaymentMethods;
using System;

namespace Gestaoaju.Models.EntityModel.Financial
{
    public interface IPayment
    {
        int Id { get; set; }
        int NumberOfInstallments { get; }
        PaymentMethod PaymentMethod { get; }
        decimal InstallmentBilling { get; set; }
        decimal Total { get; set; }
        decimal? FeePercentage { get; set; }
        decimal? FeeFixedValue { get; set; }
        decimal BilledAmount { get; set; }
        DateTime Date { get; set; }
    }
}
