// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Gestaoaju.Authorization;
using Gestaoaju.Extensions.DependencyInjection;
using Gestaoaju.Infrastructure.Logging;
using Gestaoaju.Infrastructure.Mail;
using Gestaoaju.Infrastructure.Tasks;
using Gestaoaju.Models.EntityModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SharpRaven.Core.Configuration;

namespace Gestaoaju
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RavenOptions>(Configuration.GetSection("Sentry"));
            services.Configure<SmtpOptions>(Configuration.GetSection("Smtp"));

            services.AddJwtAuthentication(options =>
            {
                Configuration.GetSection("Authorization").Bind(options);
            });

            services.AddMvc(options =>
            {
                options.UseJwtAuthorizeFilter();
                options.UseCustomFilters();
            });
            
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
            });

            services.AddTransient<AppDbContext>();
            services.AddTransient<IErrorLogger, SentryLogger>();
            services.AddTransient<IMailer, SmtpMailer>();
            services.AddSingleton<ITaskHandler, TaskHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
