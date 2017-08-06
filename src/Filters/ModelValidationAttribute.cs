// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Gestaoaju.Filters
{
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionArguments.Any(arg => arg.Value == null))
            {
                filterContext.HttpContext.Response.StatusCode = 400;
                filterContext.Result = new EmptyResult();

                return;
            }

            if (!filterContext.ModelState.IsValid)
            {
                var errors = filterContext.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                filterContext.HttpContext.Response.StatusCode = 422;
                filterContext.Result = new JsonResult(new { errors = errors });
            }
        }
    }
}
