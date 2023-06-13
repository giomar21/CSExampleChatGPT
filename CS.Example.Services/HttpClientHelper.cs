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
        public static async Task<GptResponse> Request(Message message, string uri, string contentType, HttpMethod method, Dictionary<string, string> headers = null)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(uri),
                Headers = { },
                Content = new StringContent("{\r\"model\": \"gpt-3.5-turbo\",\r\"messages\": [\r{\r\"role\": \"user\",\r\"content\": \"" + message.content + "\"\r}\r]\r}")
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
                if (typeof(GptResponse) == typeof(string)) return (GptResponse)Convert.ChangeType(body, typeof(GptResponse));
                return JsonConvert.DeserializeObject<GptResponse>(body);
            }
        }
    }
}
