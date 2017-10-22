/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Commercial.RentedProducts
{
    public static class RentedProductMapping
    {
        public static void Map(this EntityTypeBuilder<RentedProduct> entity)
        {
            entity.ToTable(nameof(RentedProduct));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.RentContractId,
                p.ProductId
            });
        }
    }
}
