// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Gestaoaju.Infrastructure.Security;
using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Account.ClosureRequests;
using Gestaoaju.Models.EntityModel.Account.Tenants;
using Gestaoaju.Models.EntityModel.Account.Users;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace Gestaoaju.Models.ServiceModel.Account
{
    public class UserSignup
    {
        public ApplicationContext Context { get; private set; }
        public User User { get; private set; }
        public ClosureRequest ClosureRequest { get; private set; }
        public bool EmailAlreadyTaken { get; private set; }

        public UserSignup(ApplicationContext context, User user)
        {
            Context = context;
            User = user;
        }

        public async Task SignupAsync()
        {
            EmailAlreadyTaken = await Context.Users.WhereEmail(User.Email).AnyAsync();

            if (!EmailAlreadyTaken)
            {
                User.Token = new AccessToken().ToString();
                User.LastLogin = DateTime.UtcNow;
                User.Salt = Guid.NewGuid().ToString("N");
                User.Password = new Sha256Hash(User.Password, User.Salt).ToString();
                User.LastChangePassword = DateTime.UtcNow;
                User.Tenant = new Tenant();
                User.Tenant.Owner = User.Email;
                User.Tenant.CreatedAt = DateTime.UtcNow;

                ClosureRequest = new ClosureRequest();
                ClosureRequest.Token = Guid.NewGuid().ToString("N");
                ClosureRequest.Email = User.Email;
                ClosureRequest.RequestDate = User.LastLogin;
                ClosureRequest.ExpiryDate = ClosureRequest.RequestDate
                    .AddHours(ClosureRequest.TokenExpiryTime);

                Context.Users.Add(User);
                Context.ClosureRequests.Add(ClosureRequest);

                await Context.SaveChangesAsync();
            }
        }
    }
}
