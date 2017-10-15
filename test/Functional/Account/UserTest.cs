/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Extensions.Http;
using Gestaoaju.Factories.Account;
using Gestaoaju.Fakes;
using Gestaoaju.Models.EntityModel.Account.ClosureRequests;
using Gestaoaju.Models.EntityModel.Account.Users;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Gestaoaju.Models.ViewModel.Account.Users;

namespace Gestaoaju.Functional.Account
{
    public class UserTest
    {
        [Fact]
        public async Task SigninCorrectly()
        {
            var server = new ServerFake();
            var user = server.AppDbContext.CreateUser();
            var viewModel = new SigninViewModel { Email = user.Email, Password = UserFactory.Password };
            var response = await server.CreateClient().PostAsJsonAsync("signin", viewModel);

            server.AppDbContext.Entry(user).Reload();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(user.AccessCode);
        }

        [Fact]
        public async Task SigninWithEmptyParameters()
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
        public async Task SigninWithWrongPassword()
        {
            var server = new ServerFake();
            var user = server.AppDbContext.CreateUser();
            var viewModel = new SigninViewModel { Email = user.Email, Password = "wrong" };
            var response = await server.CreateClient().PostAsJsonAsync("signin", viewModel);

            server.AppDbContext.Entry(user).Reload();

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Null(user.AccessCode);
        }

        [Fact]
        public async Task SignupCorrectly()
        {
            var server = new ServerFake();
            var user = server.AppDbContext.BuildUser();
            var viewModel = new SignupViewModel { Name = user.Name, Email = user.Email, Password = user.Password };
            var response = await server.CreateClient().PostAsJsonAsync("signup", viewModel);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(server.AppDbContext.Users.WhereEmail(user.Email).Any());
            Assert.True(server.AppDbContext.ClosureRequests.WhereEmail(user.Email).Any());
            Assert.True(server.Mailer.EmailSent);
        }

        [Fact]
        public async Task SignupWithEmailAlreadyTaken()
        {
            var server = new ServerFake();
            var user = server.AppDbContext.CreateUser();
            var viewModel = new SignupViewModel { Name = "Another", Email = user.Email, Password = UserFactory.Password };
            var response = await server.CreateClient().PostAsJsonAsync("signup", viewModel);
            var errors = await response.Content.ReadAsJsonAsync<List<string>>();
            var expectedErrors = new [] { "E-mail já cadastrado." };

            Assert.Equal((HttpStatusCode)422, response.StatusCode);
            Assert.Equal(errors.OrderBy(e => e), expectedErrors.OrderBy(e => e));
            Assert.False(server.Mailer.EmailSent);
        }

        [Fact]
        public async Task SignupWithEmptyParameters()
        {
            var server = new ServerFake();
            var response = await server.CreateClient().PostAsJsonAsync("signup");
            var errors = await response.Content.ReadAsJsonAsync<List<string>>();
            var expectedErrors = new []
            {
                "The Name field is required.",
                "The Email field is required.",
                "The Password field is required."
            };

            Assert.Equal((HttpStatusCode)422, response.StatusCode);
            Assert.Equal(errors.OrderBy(e => e), expectedErrors.OrderBy(e => e));
        }

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
