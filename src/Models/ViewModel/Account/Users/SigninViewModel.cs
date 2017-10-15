/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System.ComponentModel.DataAnnotations;

namespace Gestaoaju.Models.ViewModel.Account.Users
{
    public class SigninViewModel
    {
        [Required, EmailAddress, MaxLength(80)]
        public string Email { get; set; }

        [Required, MaxLength(20)]
        public string Password { get; set; }
    }
}
