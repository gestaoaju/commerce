/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Account.ClosureRequests
{
    public static class ClosureRequestMapping
    {
        public static void Map(this EntityTypeBuilder<ClosureRequest> entity)
        {
            entity.ToTable(nameof(ClosureRequest));

            entity.HasKey(p => p.Token);

            entity.Property(p => p.Token).HasMaxLength(50);
            entity.Property(p => p.Email).HasMaxLength(80).IsRequired();
            entity.Property(p => p.RequestDate).IsRequired();
            entity.Property(p => p.ExpiryDate).IsRequired();
        }
    }
}
