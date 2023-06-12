namespace CS.Example.ChatGPT.Models
{
    public class GptRequest
    {
        public string model { get { return "gpt-3.5-turbo";  } }
        public List<Message>? messages { get; set; }
    }
}
