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
            

            return View();
        }
    }
}
