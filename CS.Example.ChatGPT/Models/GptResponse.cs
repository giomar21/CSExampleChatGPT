using System.ComponentModel;

namespace CS.Example.ChatGPT.Models
{
    public class GptResponse
    {
        public string id { get; set; }
        [DisplayName("object")]
        public string object_ { get; set; }
        public long created { get; set; }
        public string model { get; set; }
        public Usage usage { get; set; }
        public List<Choice> choices { get; set; }
    }
}
