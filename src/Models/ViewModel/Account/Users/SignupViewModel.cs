// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations;
using Gestaoaju.Models.EntityModel.Account.Users;

namespace Gestaoaju.Models.ViewModel.Account.Users
{
    public class SignupViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }

        [Required, EmailAddress, MaxLength(80)]
        public string Email { get; set; }

        [Required, MinLength(8), MaxLength(20)]
        public string Password { get; set; }

        public User MapToUser()
        {
            return new User
            {
                Name = Name,
                Email = Email,
                Password = Password
            };
        }
    }
}
