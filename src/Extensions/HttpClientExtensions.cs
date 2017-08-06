// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gestaoaju.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient httpClient,
            string requestUri, object content)
        {
            var jsonContent = JsonConvert.SerializeObject(content);
            return await httpClient.PostAsJsonAsync(requestUri, jsonContent);
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient httpClient,
            string requestUri, string content)
        {
            var jsonContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await httpClient.PostAsync(requestUri, jsonContent);
        }
    }
}
