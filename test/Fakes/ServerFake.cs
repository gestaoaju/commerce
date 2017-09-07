// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Gestaoaju.Authorization;
using Gestaoaju.Infrastructure.Logging;
using Gestaoaju.Infrastructure.Mail;
using Gestaoaju.Models.EntityModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace Gestaoaju.Fakes
{
    public class ServerFake : TestServer
    {
        public ServerFake() : base(new WebHostBuilder()
            .UseStartup<StartupFake>().UseEnvironment("Testing"))
        {
        }

        public AppDbContext ApplicationContext => Host.Services.GetService<AppDbContext>();

        public ErrorLoggerFake ErrorLogger => Host.Services.GetService<IErrorLogger>() as ErrorLoggerFake;

        public MailerFake Mailer => Host.Services.GetService<IMailer>() as MailerFake;

        public HttpClient CreateClient(string accessCode = null)
        {
            var client = base.CreateClient();

            if (accessCode != null)
            {
                var options = Host.Services.GetService<IOptions<JwtOptions>>();
                var provider = new JwtProvider(options.Value);
                var token = provider.CreateToken(accessCode);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }
    }
}
