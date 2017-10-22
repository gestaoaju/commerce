/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Financial.OtherCashActivities
{
    public static class OtherCashActivityMapping
    {
        public static void Map(this EntityTypeBuilder<OtherCashActivity> entity)
        {
            entity.ToTable(nameof(OtherCashActivity));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
