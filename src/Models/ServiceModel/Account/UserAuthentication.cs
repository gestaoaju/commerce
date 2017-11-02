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

namespace Gestaoaju.Models.ServiceModel.Account
{
    public class UserAuthentication
    {
        public AppDbContext Context { get; private set; }
        public User User { get; private set; }

        public UserAuthentication(AppDbContext context)
        {
            Context = context;
        }

        public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            User = await Context.Users.WhereId(userId).SingleOrDefaultAsync();

            Sha256Hash oldPasswordHash = new Sha256Hash(oldPassword, User.Salt);

            if (User.Password != oldPasswordHash.ToString())
            {
                return false;
            }

            User.Salt = Guid.NewGuid().ToString("N");
            User.Password = new Sha256Hash(newPassword, User.Salt).ToString();
            User.LastChangePassword = DateTime.UtcNow;

            await Context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SignInAsync(string email, string password)
        {
            User = await Context.Users
                .WhereEmail(email)
                .SingleOrDefaultAsync();

            if (User != null)
            {
                Sha256Hash passwordHash = new Sha256Hash(password, User.Salt);

                if (User.Password == passwordHash.ToString())
                {
                    User.LastLogin = DateTime.UtcNow;
                    await Context.SaveChangesAsync();

                    return true;
                }
            }

            return false;
        }
    }
}
