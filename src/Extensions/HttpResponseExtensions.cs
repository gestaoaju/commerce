// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System;
using Microsoft.AspNetCore.Http;

namespace Gestaoaju.Extensions
{
    public static class HttpResponseExtensions
    {
        public static void ClearAccessToken(this HttpResponse response)
        {
            response.Cookies.Delete("token");
        }

        public static void SetAccessToken(this HttpResponse response, string token)
        {
            response.Cookies.Append("token", token, new CookieOptions
            {
                Path = "/",
                HttpOnly = true,
                Expires = DateTime.MinValue
            });
        }
    }
}
