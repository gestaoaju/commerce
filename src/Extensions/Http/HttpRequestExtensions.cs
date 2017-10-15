/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace Gestaoaju.Extensions.Http
{
    public static class HttpRequestExtensions
    {
        public static string DomainUrl(this HttpRequest request)
        {
            return $"{request.Scheme}{Uri.SchemeDelimiter}{request.Host}";
        }
    }
}
