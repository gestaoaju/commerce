using Gestaoaju.Infrastructure.Logging;
using Gestaoaju.Infrastructure.Mail;
using Gestaoaju.Infrastructure.Mvc;
using Gestaoaju.Models.EntityModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            services.AddMvc();
            
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(Configuration["Database:ConnectionString"]);
            });

            services.AddTransient<ApplicationContext>();
            services.AddTransient<IErrorLogger, SentryLogger>();
            services.AddTransient<IMailer, SmtpMailer>();
            services.AddTransient<TemplateViewEngine>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
