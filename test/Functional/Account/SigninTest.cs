/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Extensions.Http;
using Gestaoaju.Factories.Account;
using Gestaoaju.Fakes;
using Gestaoaju.Models.ViewModel.Account.Users;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Gestaoaju.Functional.Account
{
    public class SigninTest
    {
        [Fact]
        public async Task ShouldSignin()
        {
            var server = new ServerFake();
            var user = server.AppDbContext.CreateUser();
            var viewModel = UserFactory.SigninViewModel(user);
            var response = await server.CreateClient().PostAsJsonAsync("signin", viewModel);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ShouldNotSigninWithoutRequiredFields()
        {
            var server = new ServerFake();
            var response = await server.CreateClient().PostAsJsonAsync("signin");
            var errors = await response.Content.ReadAsJsonAsync<List<string>>();
            var expectedErrors = new []
            {
                "The Email field is required.",
                "The Password field is required."
            };

            Assert.Equal((HttpStatusCode)422, response.StatusCode);
            Assert.Equal(errors.OrderBy(e => e), expectedErrors.OrderBy(e => e));
        }

        [Fact]
        public async Task ShouldNotSigninWithWrongPassword()
        {
            var server = new ServerFake();
            var user = server.AppDbContext.CreateUser();
            var viewModel = UserFactory.SigninViewModel(user, password: "wrong");
            var response = await server.CreateClient().PostAsJsonAsync("signin", viewModel);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
