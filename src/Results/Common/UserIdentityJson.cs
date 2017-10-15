/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System.Threading.Tasks;
using Gestaoaju.Models.EntityModel.Account.Users;
using Microsoft.AspNetCore.Mvc;

namespace Gestaoaju.Results.Common
{
    public class UserIdentityJson : IActionResult
    {
        private User user;

        public UserIdentityJson(User user)
        {
            this.user = user;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var json = new { name = user.Name };
            await new JsonResult(json).ExecuteResultAsync(context);
        }
    }
}
