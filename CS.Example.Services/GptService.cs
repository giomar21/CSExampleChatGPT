using CS.Example.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace CS.Example.Services
{
    /// <summary>
    /// Servicios de chat GTP
    /// </summary>
    public static class GptService
    {
        /// <summary>
        /// Método que crea un chat de GPT a través de una petición POST
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<GptResponse> CreateChatCompletionRestSharp(GptRequest request)
        {
            string urlRequest = $"https://openai80.p.rapidapi.com/chat/completions";

            var headers = new Dictionary<string, string>() {
                { "Content-Type", ContentType.JSON },
                { "X-RapidAPI-Key", "7bb42da06fmshffec60946a92f3cp1fa214jsn982a8315f1f9" },
                { "X-RapidAPI-Host", "openai80.p.rapidapi.com" }
            };

            GptResponse response = await RestHttpClient.JsonRequest<GptResponse, GptRequest>(request, urlRequest, ContentType.JSON, HttpMethods.Post, headers);

            return response;
        }

        /// <summary>
        /// Método que crea un chat de GPT a través de una petición POST
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<GptResponse> CreateChatCompletionHttpClient(GptRequest request)
        {
            string urlRequest = $"https://openai80.p.rapidapi.com/chat/completions";

            var headers = new Dictionary<string, string>() {
                { "X-RapidAPI-Key", "7bb42da06fmshffec60946a92f3cp1fa214jsn982a8315f1f9" },
                { "X-RapidAPI-Host", "openai80.p.rapidapi.com" }
            };

            GptResponse response = await HttpClientHelper.Request(request.messages.FirstOrDefault(), urlRequest, ContentType.JSON, HttpMethod.Post, headers);

            return response;
        }

    }
}
