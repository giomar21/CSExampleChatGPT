using CS.Example.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace CS.Example.Services
{
    public static class HttpClientHelper
    {
        public static async Task<GptResponse?> Request(GptRequest gptRequest, string uri, string contentType, HttpMethod method, Dictionary<string, string> headers = null)
        {
            var jsonRequest = JsonConvert.SerializeObject(gptRequest, Formatting.Indented);
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(uri),
                Headers = { },
                Content = new StringContent(jsonRequest)
                {
                    Headers =

                    {
                        ContentType = new MediaTypeHeaderValue(contentType)
                    }
                }
            };

            if (headers != null)
            {
                foreach (var item in headers)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }

            using (var response = await client.SendAsync(request))
            {
	            response.EnsureSuccessStatusCode();
	            var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GptResponse>(body);
            }
        }
    }
}
