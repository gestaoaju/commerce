/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestaoaju.Results.Common
{
    public class ModelErrorsJson : JsonResult
    {
        public ModelErrorsJson(params string[] errors) : base(errors)
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}
