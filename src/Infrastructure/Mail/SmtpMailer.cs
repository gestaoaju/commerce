// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestaoaju.Infrastructure.Mail
{
    public class SmtpMailer : IMailer
    {
        private SmtpMailerOptions options;

        public SmtpMailer(IOptions<SmtpMailerOptions> options)
        {
            this.options = options.Value;
        }

        public ICollection<MailRecipient> Recipients { get; private set; }

        public async Task SendAsync(string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(options.Name, options.Email));
            message.To.AddRange(Recipients.Select(recipient =>
                new MailboxAddress(recipient.Name, recipient.Address)));
            message.Subject = subject;
            message.Body = new BodyBuilder { HtmlBody = htmlMessage }.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(options.Host, options.Port, options.UseSsl);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(options.Username, options.Password);

                await client.SendAsync(message);

                client.Disconnect(true);
            }
        }
    }
}
