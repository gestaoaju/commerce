// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System;

namespace Gestaoaju.Models.EntityModel.Account.ClosureRequests
{
    public class ClosureRequest
    {
        public const int ExpiryHours = 48;
        
        public string Token { get; set; }
        public string Email { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
