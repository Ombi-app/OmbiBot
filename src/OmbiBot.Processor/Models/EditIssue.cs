using System.Collections.Generic;

namespace OmbiBot.Processor.Models
{
    public class EditIssue
    {
        public string title { get; set; }
        public string body { get; set; }
        public string assignee { get; set; }
        public int milestone { get; set; }
        public string state { get; set; }
        public List<string> labels { get; set; }
    }
}