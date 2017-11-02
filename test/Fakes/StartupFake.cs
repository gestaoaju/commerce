/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Extensions.DependencyInjection;
using Gestaoaju.Infrastructure.Logging;
using Gestaoaju.Infrastructure.Mail;
using Gestaoaju.Infrastructure.Tasks;
using Gestaoaju.Infrastructure.Tenancy;
using Gestaoaju.Models.EntityModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            services.AddCookieAuthentication();

            services.AddMvc(options =>
            {
                options.UseAuthorizeFilter();
                options.UseCustomFilters();
            });
            
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("db_test");
            });

            services.AddSingleton(Environment);
            services.AddSingleton<AppDbContext>();
            services.AddSingleton<TenantDbContext>();
            services.AddSingleton<IErrorLogger, ErrorLoggerFake>();
            services.AddSingleton<IMailer, MailerFake>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ITaskHandler, TaskHandlerFake>();
            services.AddSingleton<ITenantScopeProvider, TenantScopeHttpProvider>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
