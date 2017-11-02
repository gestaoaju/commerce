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
        public ErrorsJson() { }

        public ErrorsJson(params string[] errors)
        {
            Errors = errors;
        }

        public ICollection<string> Errors { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var jsonResult = new JsonResult(this);
            jsonResult.StatusCode = StatusCodes.Status422UnprocessableEntity;

            await jsonResult.ExecuteResultAsync(context);
        }
    }
}
