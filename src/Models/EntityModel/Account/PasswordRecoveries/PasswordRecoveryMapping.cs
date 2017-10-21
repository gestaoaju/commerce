/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Account.PasswordRecoveries
{
    public static class PasswordRecoveryMapping
    {
        public static void Map(this EntityTypeBuilder<PasswordRecovery> entity)
        {
            entity.ToTable(nameof(PasswordRecovery));

            entity.HasKey(p => p.Token);

            entity.Property(p => p.Token).HasMaxLength(50);
            entity.Property(p => p.Email).HasMaxLength(80).IsRequired();
            entity.Property(p => p.RequestDate).IsRequired();
        }
    }
}
