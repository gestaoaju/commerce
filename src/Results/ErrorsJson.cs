/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestaoaju.Results
{
    public class ErrorsJson : IActionResult
    {
        private IEnumerable<string> errors;

        public ErrorsJson(params string[] errors)
        {
            this.errors = errors;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var jsonResult = new JsonResult(errors);
            jsonResult.StatusCode = StatusCodes.Status422UnprocessableEntity;
            
            await jsonResult.ExecuteResultAsync(context);
        }
    }
}
