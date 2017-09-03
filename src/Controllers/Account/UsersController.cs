﻿// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Gestaoaju.Extensions;
using Gestaoaju.Infrastructure.Mail;
using Gestaoaju.Infrastructure.Tasks;
using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Account.Users;
using Gestaoaju.Models.ServiceModel.Account;
using Gestaoaju.Models.ViewModel.Account.Users;
using Gestaoaju.Models.ViewModel.Emails;
using Gestaoaju.Results.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
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

        [HttpGet, Route("signin")]
        public async Task<IActionResult> Signin()
        {
            if (Request.AccessToken() != null)
            {
                if (await context.Users.WhereToken(Request.AccessToken()).AnyAsync())
                {
                    return Redirect("/dashboard");
                }
            }

            return View("~/Views/App/Account/Users/Signin.cshtml");
        }

        [HttpPost, Route("signin")]
        public async Task<IActionResult> Signin([FromBody] SigninViewModel viewModel)
        {
            UserAuthentication authentication = new UserAuthentication(context);

            if (await authentication.SigninAsync(viewModel.Email, viewModel.Password))
            {
                Response.SetAccessToken(authentication.User.AccessCode);
                return Ok();
            }

            return Unauthorized();
        }

        [HttpGet, Route("signout")]
        public async Task<IActionResult> Signout()
        {
            UserAuthentication authentication = new UserAuthentication(context);

            if (await authentication.SignoutAsync(Request.AccessToken()))
            {
                Response.ClearAccessToken();
            }

            return Redirect("/signin");
        }

        [HttpGet, Route("signup")]
        public ActionResult Signup()
        {
            return View("~/Views/App/Account/Users/Signup.cshtml");
        }

        [HttpPost, Route("signup")]
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

            taskHandler.ExecuteInBackground(async () => await mailer
                .SendAsync("Bem vindo ao gestaoaju.com.br :)", htmlMessage));

            Response.SetAccessToken(userSignup.User.AccessCode);

            return Ok();
        }
    }
}
