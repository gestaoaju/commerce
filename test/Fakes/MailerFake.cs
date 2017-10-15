// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System.Collections.Generic;
using System.Threading.Tasks;
/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Infrastructure.Mail;

namespace Gestaoaju.Fakes
{
    public class MailerFake : IMailer
    {
        public bool EmailSent { get; private set; }

        public ICollection<MailRecipient> Recipients => new List<MailRecipient>();

        public Task SendAsync(string subject, string htmlMessage)
        {
            EmailSent = true;
            return Task.CompletedTask;
        }
    }
}
