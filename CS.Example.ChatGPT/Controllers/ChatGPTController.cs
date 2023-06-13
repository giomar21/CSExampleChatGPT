﻿using CS.Example.Common.Models;
using CS.Example.Services;
using Microsoft.AspNetCore.Mvc;

namespace CS.Example.ChatGPT.Controllers
{
    public class ChatGPTController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateChat(string text)
        {
            var request = new Message() {
                role = "user",
                content = text
            };

            //var testHttp = new HttpClientTest();
            //GptResponse response = await testHttp.Test(request);

            //var result = await GptService.CreateChatCompletion(new Common.Models.GptRequest() {
            //    messages = new List<Message>() { request }
            //});

            var result = await GptService.CreateChatCompletionHttpClient(new GptRequest()
            {
                messages = new List<Message>() { request }
            });

            return View(result);
        }
    }
}
