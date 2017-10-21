/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System;
using System.Linq;

namespace Gestaoaju.Models.EntityModel.Account.PasswordRecoveries
{
    public static class PasswordRecoveryQuery
    {
        public static IQueryable<PasswordRecovery> NotExpiredByDate(this IQueryable<PasswordRecovery> passwordRecoveries)
        {
            var expiryDate = DateTime.UtcNow.AddHours(PasswordRecovery.ExpiryHours);
            return passwordRecoveries.Where(passwordRecovery => passwordRecovery.RequestDate < expiryDate);
        }

        public static IQueryable<PasswordRecovery> WhereToken(this IQueryable<PasswordRecovery> passwordRecoveries, string token)
        {
            return passwordRecoveries.Where(passwordRecovery => passwordRecovery.Token == token);
        }

        public static IQueryable<PasswordRecovery> WhereEmail(this IQueryable<PasswordRecovery> passwordRecoveries, string email)
        {
            return passwordRecoveries.Where(passwordRecovery => passwordRecovery.Email == email);
        }
    }
}
