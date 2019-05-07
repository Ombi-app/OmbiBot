namespace OmbiBot.Processor.Config
{
    public class IssueConfiguration
    {
        public string[] AllowedUsers { get; set; }
        public string TemplateContains { get; set; }
        public string Message { get; set; }
        public string TemplateNotFollowedText { get; set; }
    }
}