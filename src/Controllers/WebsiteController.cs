// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Microsoft.AspNetCore.Mvc;

namespace Gestaoaju.Controllers
{
    public class WebsiteController : Controller
    {
        [HttpGet, Route("")]
        public IActionResult Home()
        {
            return View("~/Views/Website/Home.cshtml");
        }
    }
}
