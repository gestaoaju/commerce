// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Gestaoaju.Results.Security
{
    public class SecurityTokenJson : IActionResult
    {
        private string token;

        public SecurityTokenJson(string token)
        {
            this.token = token;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var json = new { token = token };
            await new JsonResult(json).ExecuteResultAsync(context);
        }
    }
}
