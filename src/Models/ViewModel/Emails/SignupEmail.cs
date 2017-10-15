/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using Gestaoaju.Models.EntityModel.Account.ClosureRequests;
using Gestaoaju.Models.EntityModel.Account.Users;

namespace Gestaoaju.Models.ViewModel.Emails
{
    public class SignupEmail
    {
        public User User { get; private set; }
        public ClosureRequest ClosureRequest { get; private set; }

        public SignupEmail(User user, ClosureRequest closureRequest)
        {
            User = user;
            ClosureRequest = closureRequest;
        }
    }
}
