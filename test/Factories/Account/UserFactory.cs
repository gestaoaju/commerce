// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System;
using Gestaoaju.Infrastructure.Security;
using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Account.Users;

namespace Gestaoaju.Factories.Account
{
    public static class UserFactory
    {
        public const string Password = "0123456789";

        public static User BuildUser(this AppDbContext context)
        {
            return new User
            {
                Name = "User Test",
                Email = $"user-{IdFactory.Id}@test.com",
                Password = Password
            };
        }

        public static User CreateUser(this AppDbContext context, Tenant tenant = null)
        {
            User user = context.BuildUser();
            user.Salt = Guid.NewGuid().ToString("N");
            user.Password = new Sha256Hash(user.Password, user.Salt).ToString();
            user.LastChangePassword = DateTime.UtcNow.Date.AddDays(-1);
            user.Tenant = tenant ?? context.CreateTenant();

            context.Users.Add(user);
            context.SaveChanges();

            return user;
        }
    }
}
