/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestaoaju.Models.EntityModel.Financial.PurchaseExpenses
{
    public static class PurchaseExpenseMapping
    {
        public static void Map(this EntityTypeBuilder<PurchaseExpense> entity)
        {
            entity.ToTable(nameof(PurchaseExpense));
            
            entity.HasKey(p => new
            {
                p.TenantId,
                p.Id
            });

            entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        }
    }
}
