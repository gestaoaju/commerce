/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

namespace Gestaoaju.Infrastructure.Mail
{
    public class MailRecipient
    {
        public string Name { get; private set; }
        public string Address { get; private set; }

        public MailRecipient(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }
}
