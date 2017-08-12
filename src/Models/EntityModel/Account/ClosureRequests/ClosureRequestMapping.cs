// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Microsoft.EntityFrameworkCore;

namespace Gestaoaju.Models.EntityModel.Account.ClosureRequests
{
    public static class ClosureRequestMapping
    {
        public static void MapClosureRequest(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClosureRequest>(entity =>
            {
                entity.HasKey(p => p.Token);

                entity.Property(p => p.Token).HasMaxLength(50);
                entity.Property(p => p.Email).HasMaxLength(80).IsRequired();
                entity.Property(p => p.RequestDate).IsRequired();
                entity.Property(p => p.ExpiryDate).IsRequired();
            });
        }
    }
}
