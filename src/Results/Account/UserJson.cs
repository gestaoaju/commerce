/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System.Threading.Tasks;
using Gestaoaju.Models.EntityModel.Account.Users;
using Microsoft.AspNetCore.Mvc;

namespace Gestaoaju.Results.Account
{
    public class UserJson : IActionResult
    {
        public UserJson(User user)
        {
            Name = user.Name;
        }

        public string Name { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}
