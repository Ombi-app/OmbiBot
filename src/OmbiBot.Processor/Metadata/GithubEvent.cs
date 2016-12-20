namespace OmbiBot.Processor
{
    public class GithubEvent
    {
        // https://developer.github.com/webhooks/#events
        public const string Wildcard = "*";
        public const string CommitComment = "commit_comment";
        public const string Create = "create";
        public const string Delete = "delete";
        public const string IssueComment = "issue_comment";
        public const string Issues = "issues";
        public const string Label = "label";
    }
}