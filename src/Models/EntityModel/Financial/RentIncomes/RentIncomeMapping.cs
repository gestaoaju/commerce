/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Financial.RentIncomes
{
    public static class RentIncomeMapping
    {
        public static void Map(this EntityTypeBuilder<RentIncome> entity)
        {
            entity.ToTable(nameof(RentIncome));

            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        }
    }
}
