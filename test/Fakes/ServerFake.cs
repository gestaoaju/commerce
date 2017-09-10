// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Gestaoaju.Extensions.Http;
using Gestaoaju.Factories.Account;
using Gestaoaju.Infrastructure.Logging;
using Gestaoaju.Infrastructure.Mail;
using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Account.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System.Linq;
using System.Net.Http;

namespace Gestaoaju.Fakes
{
    public class ServerFake : TestServer
    {
        public ServerFake() : base(new WebHostBuilder()
            .UseStartup<StartupFake>().UseEnvironment("Testing"))
        {
        }

        public AppDbContext AppDbContext => Host.Services.GetService<AppDbContext>();

        public ErrorLoggerFake ErrorLogger => Host.Services.GetService<IErrorLogger>() as ErrorLoggerFake;

        public MailerFake Mailer => Host.Services.GetService<IMailer>() as MailerFake;

        public HttpClient CreateAuthenticatedClient(User user)
        {
            var client = base.CreateClient();
            var json = new { email = user.Email, password = UserFactory.Password };
            var response = client.PostAsJsonAsync("signin", json).Result;
            var header = response.Headers.GetValues("Set-Cookie");
            var cookies = SetCookieHeaderValue.ParseList(header.ToList());
            var cookie = cookies.Single();

            client.DefaultRequestHeaders.Add("Cookie", cookie.ToString());
            
            return client;
        }
    }
}
