namespace OmbiBot.Processor.Models
{
    public class User
    {
        public string login { get; set; }
        public int id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }

    public class CommentResponse
    {
        public int id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string body { get; set; }
        public User user { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}