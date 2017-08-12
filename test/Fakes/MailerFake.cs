// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System.Collections.Generic;
using System.Threading.Tasks;
using Gestaoaju.Infrastructure.Mail;
using Gestaoaju.Infrastructure.Mvc;

namespace Gestaoaju.Fakes
{
    public class MailerFake : IMailer
    {
        private TemplateViewEngine templateViewEngine;

        public MailerFake(TemplateViewEngine templateViewEngine)
        {
            this.templateViewEngine = templateViewEngine;
        }

        public bool EmailSent { get; private set; }

        public ICollection<MailRecipient> Recipients => new List<MailRecipient>();

        public Task SendAsync(string subject, string htmlMessage)
        {
            EmailSent = true;
            return Task.CompletedTask;
        }
    }
}
