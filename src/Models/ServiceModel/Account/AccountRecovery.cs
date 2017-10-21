/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Security;
using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Account.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Gestaoaju.Models.EntityModel.Account.PasswordRecoveries;

namespace Gestaoaju.Models.ServiceModel.Account
{
    public class AccountRecovery
    {
        public AppDbContext Context { get; private set; }
        public PasswordRecovery PasswordRecovery { get; private set; }

        public AccountRecovery(AppDbContext context)
        {
            Context = context;
        }

        public async Task<bool> Recovery(string email)
        {
            if (await Context.Users.WhereEmail(email).AnyAsync())
            {
                PasswordRecovery = new PasswordRecovery();
                PasswordRecovery.Token = Guid.NewGuid().ToString("N");
                PasswordRecovery.Email = email;
                PasswordRecovery.RequestDate = DateTime.UtcNow;

                Context.PasswordRecoveries.Add(PasswordRecovery);
                await Context.SaveChangesAsync();
            }

            return PasswordRecovery != null;
        }
        
        public async Task<bool> ResetPassword(string token, string newPassword)
        {
            PasswordRecovery = await Context.PasswordRecoveries
                .NotExpiredByDate().WhereToken(token).SingleOrDefaultAsync();

            if (PasswordRecovery != null)
            {
                User user = await Context.Users
                    .WhereEmail(PasswordRecovery.Email)
                    .SingleOrDefaultAsync();

                if (user != null && user.LastChangePassword < PasswordRecovery.RequestDate)
                {
                    user.Salt = Guid.NewGuid().ToString("N");
                    user.Password = new Sha256Hash(newPassword, user.Salt).ToString();
                    user.LastChangePassword = DateTime.UtcNow;

                    await Context.SaveChangesAsync();
                }
            }

            return PasswordRecovery != null;
        }
    }
}
