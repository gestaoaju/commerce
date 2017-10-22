/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Commercial.Partners
{
    public static class PartnerMapping
    {
        public static void Map(this EntityTypeBuilder<Partner> entity)
        {
            entity.ToTable(nameof(Partner));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
