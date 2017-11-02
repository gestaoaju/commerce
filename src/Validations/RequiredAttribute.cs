/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

 using DataAnnotations = System.ComponentModel.DataAnnotations;

namespace Gestaoaju.Validations
{
    public class RequiredAttribute : DataAnnotations.RequiredAttribute
    {
        public RequiredAttribute()
        {
            ErrorMessage = "{0}: Required";
        }
    }
}
