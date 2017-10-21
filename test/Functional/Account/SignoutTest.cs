/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System.Net;
using System.Threading.Tasks;
using Gestaoaju.Factories.Account;
using Gestaoaju.Fakes;
using Xunit;

namespace Gestaoaju.Functional.Account
{
    public class SignoutTest
    {
        [Fact]
        public async Task Signout()
        {
            var server = new ServerFake();
            var user = server.AppDbContext.CreateUser();
            var response = await server.CreateAuthenticatedClient(user).GetAsync("signout");

            server.AppDbContext.Entry(user).Reload();

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/signin", response.Headers.Location.ToString());
            Assert.Null(user.AccessCode);
        }
    }
}
