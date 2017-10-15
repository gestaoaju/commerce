/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;

namespace Gestaoaju.Infrastructure.Mail
{
    public class MailView
    {
        private ActionContext actionContext;
        private ViewEngineResult viewResult;

        public MailView(ActionContext actionContext, ViewEngineResult viewResult)
        {
            this.actionContext = actionContext;
            this.viewResult = viewResult;
        }

        public async Task<string> ToHtmlAsync<TModel>(TModel viewModel)
        {
            var serviceProvider = actionContext.HttpContext.RequestServices;
            var tempDataProvider = serviceProvider.GetService<ITempDataProvider>();
            var tempData = new TempDataDictionary(actionContext.HttpContext, tempDataProvider);

            var modelState = new ModelStateDictionary();
            var metadataProvider = new EmptyModelMetadataProvider();
            var viewData = new ViewDataDictionary<TModel>(metadataProvider, modelState);

            viewData.Model = viewModel;

            using (var writer = new StringWriter())
            {
                var viewContext = new ViewContext(actionContext, viewResult.View,
                    viewData, tempData, writer, new HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
