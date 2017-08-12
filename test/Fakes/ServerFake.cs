// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System.Net.Http;
using Gestaoaju.Infrastructure.Logging;
using Gestaoaju.Infrastructure.Mail;
using Gestaoaju.Models.EntityModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gestaoaju.Fakes
{
    public class ServerFake : TestServer
    {
        public ServerFake() : base(new WebHostBuilder()
            .UseStartup<StartupFake>().UseEnvironment("Testing"))
        {
        }

        public ApplicationContext ApplicationContext => Host.Services
            .GetRequiredService<ApplicationContext>();

        public ErrorLoggerFake ErrorLogger => Host.Services
            .GetRequiredService<IErrorLogger>() as ErrorLoggerFake;

        public MailerFake Mailer => Host.Services
            .GetRequiredService<IMailer>() as MailerFake;

        public HttpClient CreateClient(string accessToken = null)
        {
            var client = base.CreateClient();

            if (accessToken != null)
            {
                client.DefaultRequestHeaders.Add("Cookie", $"token={accessToken}");
            }

            return client;
        }
    }
}
