// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Gestaoaju.Extensions;
using Gestaoaju.Infrastructure.Mail;
using Gestaoaju.Infrastructure.Tasks;
using Gestaoaju.Infrastructure.Mvc;
using Gestaoaju.Models.EntityModel;
using Gestaoaju.Models.EntityModel.Account.Users;
using Gestaoaju.Models.ServiceModel.Account;
using Gestaoaju.Models.ViewModel.Account.Users;
using Gestaoaju.Models.ViewModel.Emails;
using Gestaoaju.Results.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Gestaoaju.Controllers.Account
{
    public class UsersController : Controller
    {
        private ApplicationContext context;
        private TemplateViewEngine templateView;
        private IMailer mailer;
        private ITaskHandler taskHandler;

        public UsersController(ApplicationContext context, TemplateViewEngine templateView,
            IMailer mailer, ITaskHandler taskHandler)
        {
            this.context = context;
            this.templateView = templateView;
            this.mailer = mailer;
            this.taskHandler = taskHandler;
        }

        [HttpGet, Route("signin")]
        public async Task<ActionResult> Signin()
        {
            if (Request.AccessToken() != null)
            {
                if (await context.Users.WhereToken(Request.AccessToken()).AnyAsync())
                {
                    return Redirect("/dashboard");
                }
            }

            return View("~/Views/Account/Users/Signin.cshtml");
        }

        [HttpPost, Route("signin")]
        public async Task<ActionResult> Signin([FromBody] SigninViewModel viewModel)
        {
            UserAuthentication authentication = new UserAuthentication(context);

            if (await authentication.SigninAsync(viewModel.Email, viewModel.Password))
            {
                Response.SetAccessToken(authentication.User.Token);
                return Ok();
            }

            return Unauthorized();
        }

        [HttpGet, Route("signout")]
        public async Task<ActionResult> Signout()
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
            return View("~/Views/Account/Users/SignupSoon.cshtml");
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

            mailer.Recipients.Add(new MailRecipient(userSignup.User.Name, userSignup.User.Email));

            taskHandler.Execute(async () => await mailer.SendAsync(
                subject: "Bem vindo ao gestaoaju.com.br :)",
                htmlMessage: await templateView.RenderToStringAsync(
                    viewName: "~/Views/Emails/SignupEmail.cshtml",
                    model: new SignupEmail(userSignup.User, userSignup.ClosureRequest)
                )
            ));

            Response.SetAccessToken(userSignup.User.Token);

            return Ok();
        }
    }
}
