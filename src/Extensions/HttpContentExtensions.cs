// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gestaoaju.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<TResult> ReadAsJsonAsync<TResult>(this HttpContent httpContent)
        {
            return JsonConvert.DeserializeObject<TResult>(await httpContent.ReadAsStringAsync());
        }
    }
}
