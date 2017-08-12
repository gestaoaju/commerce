// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace Gestaoaju.Infrastructure.Mvc
{
    public class TemplateViewEngine
    {
        private readonly IHostingEnvironment env;
        private readonly IServiceProvider serviceProvider;
        private readonly ITempDataProvider tempDataProvider;
        private readonly IRazorViewEngine viewEngine;

        public TemplateViewEngine(IHostingEnvironment env, IServiceProvider serviceProvider,
            ITempDataProvider tempDataProvider, IRazorViewEngine viewEngine)
        {
            this.env = env;
            this.serviceProvider = serviceProvider;
            this.tempDataProvider = tempDataProvider;
            this.viewEngine = viewEngine;
        }

        public async Task<string> RenderToStringAsync<TModel>(string viewName, TModel model)
        {
            var viewResult = viewEngine.GetView(env.ContentRootPath, viewName, false);

            if (!viewResult.Success)
            {
                throw new InvalidOperationException(string.Format("Couldn't find view '{0}'", viewName));
            }

            var httpContext = new DefaultHttpContext { RequestServices = serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            var viewData = new ViewDataDictionary<TModel>(
                new EmptyModelMetadataProvider(), new ModelStateDictionary());
            var tempData = new TempDataDictionary(actionContext.HttpContext, tempDataProvider);

            viewData.Model = model;

            using (var writer = new StringWriter())
            {
                var viewContext = new ViewContext(actionContext, viewResult.View,
                    viewData, tempData, writer, new HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext);

                return writer.ToString();
            }
        }
    }
}
