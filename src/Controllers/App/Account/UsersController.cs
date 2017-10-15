/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Extensions.Http;
using Gestaoaju.Extensions.Identity;
using Gestaoaju.Infrastructure.Mail;
using Gestaoaju.Infrastructure.Tasks;
using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Account.Users;
using Gestaoaju.Models.ServiceModel.Account;
using Gestaoaju.Models.ViewModel.Account.Users;
using Gestaoaju.Models.ViewModel.Emails;
using Gestaoaju.Results.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Gestaoaju.Controllers.Account
{
    public class UsersController : Controller
    {
        private AppDbContext context;
        private IHostingEnvironment env;
        private IRazorViewEngine viewEngine;
        private IMailer mailer;
        private ITaskHandler taskHandler;

        public UsersController(AppDbContext context,
            IHostingEnvironment env, IRazorViewEngine viewEngine,
            IMailer mailer, ITaskHandler taskHandler)
        {
            this.context = context;
            this.env = env;
            this.viewEngine = viewEngine;
            this.mailer = mailer;
            this.taskHandler = taskHandler;
        }

        [HttpGet, AllowAnonymous, Route("signin")]
        public async Task<IActionResult> Signin()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (await context.Users.WhereAccessCode(User.Claims.NameIdentifier()).AnyAsync())
                {
                    return Redirect("/dashboard");
                }
            }

            return View("~/Views/App/Account/Users/Signin.cshtml");
        }

        [HttpGet, Route("signout")]
        public async Task<IActionResult> Signout()
        {
            UserAuthentication authentication = new UserAuthentication(context);
            await authentication.SignOutAsync(User.Claims.NameIdentifier());
            
            await HttpContext.SignOutAsync();

            return Redirect("/signin");
        }

        [HttpGet, AllowAnonymous, Route("signup")]
        public ActionResult Signup()
        {
            return View("~/Views/App/Account/Users/Signup.cshtml");
        }

        [HttpPost, AllowAnonymous, Route("signin")]
        public async Task<IActionResult> Signin([FromBody] SigninViewModel viewModel)
        {
            UserAuthentication authentication = new UserAuthentication(context);

            if (await authentication.SignInAsync(viewModel.Email, viewModel.Password))
            {
                await HttpContext.SignInAsync(authentication.User);
                return new UserIdentityJson(authentication.User);
            }

            return Unauthorized();
        }

        [HttpPost, AllowAnonymous, Route("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupViewModel viewModel)
        {
            UserSignup userSignup = new UserSignup(context, viewModel.MapToUser());
            await userSignup.SignupAsync();

            if (userSignup.EmailAlreadyTaken)
            {
                return new ModelErrorsJson("E-mail já cadastrado.");
            }

            var viewResult = viewEngine.GetView(env.ContentRootPath,
                "~/Views/Emails/SignupEmail.cshtml", false);

            var htmlMessage = await new MailView(ControllerContext, viewResult)
                .ToHtmlAsync(new SignupEmail(userSignup.User, userSignup.ClosureRequest));

            mailer.Recipients.Add(new MailRecipient(userSignup.User.Name, userSignup.User.Email));

            taskHandler.ExecuteInBackground(async () =>
            {
                await mailer.SendAsync("Bem vindo ao gestaoaju.com.br :)", htmlMessage);
            });

            await HttpContext.SignInAsync(userSignup.User);

            return new UserIdentityJson(userSignup.User);
        }
    }
}
