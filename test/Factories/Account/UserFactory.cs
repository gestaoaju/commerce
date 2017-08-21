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
        private static int emailId;

        public static User BuildUser(this ApplicationContext context)
        {
            return new User
            {
                Name = "User Test",
                Email = $"user-{++emailId}@test.com",
                Password = "12345678"
            };
        }

        public static User CreateUser(this ApplicationContext context,
            Tenant tenant = null, bool authenticated = false)
        {
            User user = context.BuildUser();
            user.Salt = Guid.NewGuid().ToString("N");
            user.Password = new Sha256Hash(user.Password, user.Salt).ToString();
            user.LastChangePassword = DateTime.UtcNow.Date.AddDays(-1);
            user.Tenant = tenant ?? context.CreateTenant();

            if (authenticated)
            {
                user.AccessCode = new AccessCode().ToString();
            }

            user = context.Users.Add(user).Entity;
            context.SaveChanges();

            return user;
        }
    }
}
