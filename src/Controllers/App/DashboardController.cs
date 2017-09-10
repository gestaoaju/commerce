// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Microsoft.AspNetCore.Mvc;

namespace Gestaoaju.Controllers.App
{
    [Route("[controller]")]
    public class DashboardController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View("~/Views/App/Dashboard/Index.cshtml");
        }
    }
}
