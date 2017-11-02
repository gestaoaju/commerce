/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Commercial.PaymentMethodFees
{
    public static class PaymentMethodFeeMapping
    {
        public static void Map(this EntityTypeBuilder<PaymentMethodFee> entity)
        {
            entity.ToTable(nameof(PaymentMethodFee));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.PaymentMethodId,
                p.MinimumNumberInstallments
            });
        }
    }
}
