/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System;

namespace Gestaoaju.Extensions
{
    public static class StringExtensions
    {
        public static string[] Words(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return new string[] { };
            }

            return str.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
