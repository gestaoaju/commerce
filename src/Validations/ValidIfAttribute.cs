/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System;
using System.ComponentModel.DataAnnotations;

namespace Gestaoaju.Validations
{
    public class ValidIfAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is bool)
            {
                return (bool)value;
            }

            throw new ArgumentException("The property must return a Boolean.");
        }
    }
}
