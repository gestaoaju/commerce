// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Gestaoaju.Extensions;
using Gestaoaju.Infrastructure.Logging;
using Gestaoaju.Infrastructure.Mail;
using Gestaoaju.Infrastructure.Mvc;
using Gestaoaju.Infrastructure.Tasks;
using Gestaoaju.Models.EntityModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gestaoaju.Fakes
{
    public class StartupFake
    {
        public IHostingEnvironment Environment { get; }
        public IConfigurationRoot Configuration { get; }

        public StartupFake()
        {
            Environment = new HostingEnvironmentFake();
            
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.ContentRootPath)
                .AddEnvironmentVariables()
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.UseCustomFilters();
            });
            
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("db_test");
            });

            services.AddSingleton(Environment);
            services.AddSingleton<AppDbContext>();
            services.AddSingleton<IErrorLogger, ErrorLoggerFake>();
            services.AddSingleton<IMailer, MailerFake>();
            services.AddSingleton<ITaskHandler, TaskHandlerFake>();
            services.AddSingleton<TemplateViewEngine>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
