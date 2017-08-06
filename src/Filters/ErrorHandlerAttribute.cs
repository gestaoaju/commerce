// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System.Threading.Tasks;
using Gestaoaju.Infrastructure.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Gestaoaju.Filters
{
    public class ErrorHandlerAttribute : ExceptionFilterAttribute
    {
        private IHostingEnvironment env;
        private IErrorLogger logger;

        public ErrorHandlerAttribute(IHostingEnvironment env, IErrorLogger logger)
        {
            this.env = env;
            this.logger = logger;
        }
        
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            await logger.LogAsync(context.Exception);
            context.Result = new StatusCodeResult(500);
        }
    }
}

