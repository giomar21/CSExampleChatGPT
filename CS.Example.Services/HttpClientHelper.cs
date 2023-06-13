using CS.Example.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Headers =
                {
                    { "X-RapidAPI-Key", "7bb42da06fmshffec60946a92f3cp1fa214jsn982a8315f1f9" },
                    { "X-RapidAPI-Host", "openai80.p.rapidapi.com" },
                },
                Content = new StringContent("{\r\"model\": \"gpt-3.5-turbo\",\r\"messages\": [\r{\r\"role\": \"user\",\r\"content\": \"" + message.content + "\"\r}\r]\r}")
                {
                    Headers =

                    {
                        ContentType = new MediaTypeHeaderValue(contentType)
                    }
                }
            };

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
