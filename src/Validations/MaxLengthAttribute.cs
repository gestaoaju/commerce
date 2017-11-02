/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using DataAnnotations = System.ComponentModel.DataAnnotations;

namespace Gestaoaju.Validations
{
    public class MaxLengthAttribute : DataAnnotations.MaxLengthAttribute
    {
        public MaxLengthAttribute(int length) : base(length)
        {
            ErrorMessage = "{0}: MaxLength[{1}]";
        }
    }
}
