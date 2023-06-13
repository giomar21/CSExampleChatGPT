using CS.Example.Common.Models;
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

            var result = await GptService.CreateChatCompletion(new Common.Models.GptRequest() {
                messages = new List<Message>() { request }
            });

            return View(result);
        }
    }
}
