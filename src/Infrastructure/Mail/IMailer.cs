// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestaoaju.Infrastructure.Mail
{
    public interface IMailer
    {
        ICollection<MailRecipient> Recipients { get; }
        Task SendAsync(string subject, string htmlMessage);
    }
}
