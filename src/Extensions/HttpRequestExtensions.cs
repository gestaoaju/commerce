// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace Gestaoaju.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string AccessToken(this HttpRequest request)
        {
            string token;

            if (request.Cookies.TryGetValue("token", out token))
            {
                if (!string.IsNullOrWhiteSpace(token))
                {
                    if (Regex.IsMatch(token, "^[0-9A-F]+$"))
                    {
                        return token;
                    }
                }
            }

            return null;
        }

        public static string DomainUrl(this HttpRequest request)
        {
            return $"{request.Scheme}{Uri.SchemeDelimiter}{request.Host}";
        }
    }
}
