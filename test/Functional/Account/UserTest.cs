// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Gestaoaju.Extensions;
using Gestaoaju.Factories.Account;
using Gestaoaju.Fakes;
using System.Threading.Tasks;
using System.Net;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using Gestaoaju.Models.EntityModel.Account.Users;

namespace Gestaoaju.Functional.Account
{
    public class UserTest
    {
        [Fact]
        public async Task SigninCorrectly()
        {
            var server = new ServerFake();
            var user = server.ApplicationContext.CreateUser();
            var json = new { email = user.Email, password = "12345678" };
            var response = await server.CreateClient().PostAsJsonAsync("signin", json);

            server.ApplicationContext.Entry(user).Reload();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(user.Token);
        }

        [Fact]
        public async Task SigninWithEmptyParameters()
        {
            var server = new ServerFake();
            var response = await server.CreateClient().PostAsJsonAsync("signin");
            var errors = await response.Content.ReadAsJsonAsync<List<string>>();
            var expectedErrors = new List<string>
            {
                "The Email field is required.",
                "The Password field is required."
            };

            Assert.Equal(errors.OrderBy(e => e), expectedErrors.OrderBy(e => e));
        }

        [Fact]
        public async Task SigninWithWrongPassword()
        {
            var server = new ServerFake();
            var user = server.ApplicationContext.CreateUser();
            var json = new { email = user.Email, password = "wrong" };
            var response = await server.CreateClient().PostAsJsonAsync("signin", json);

            server.ApplicationContext.Entry(user).Reload();

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Null(user.Token);
        }

        [Fact]
        public async Task SignupCorrectly()
        {
            var server = new ServerFake();
            var user = server.ApplicationContext.BuildUser();
            var json = new { name = user.Name, email = user.Email, password = user.Password };
            var response = await server.CreateClient().PostAsJsonAsync("signup", json);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(server.ApplicationContext.Users.WhereEmail(user.Email).Any());
            Assert.True(server.Mailer.EmailSent);
        }

        [Fact]
        public async Task SignupWithEmailAlreadyTaken()
        {
            var server = new ServerFake();
            var user = server.ApplicationContext.CreateUser();
            var json = new { name = "Another", email = user.Email, password = "12345678" };
            var response = await server.CreateClient().PostAsJsonAsync("signup", json);
            var errors = await response.Content.ReadAsJsonAsync<List<string>>();
            var expectedErrors = new List<string> { "E-mail já cadastrado." };

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
            var expectedErrors = new List<string>
            {
                "The Name field is required.",
                "The Email field is required.",
                "The Password field is required."
            };

            Assert.Equal(errors.OrderBy(e => e), expectedErrors.OrderBy(e => e));
        }

        [Fact]
        public async Task Signout()
        {
            var server = new ServerFake();
            var user = server.ApplicationContext.CreateUser(authenticated: true);
            var response = await server.CreateClient(accessToken: user.Token).GetAsync("signout");

            server.ApplicationContext.Entry(user).Reload();

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/signin", response.Headers.Location.ToString());
            Assert.Null(user.Token);
        }
    }
}
