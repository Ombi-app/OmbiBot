using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OmbiBot.Processor.Config;
using OmbiBot.Processor.Models;

namespace OmbiBot.Processor
{
    public class CreateIssueProcessor : IProcessor
    {
        public CreateIssueProcessor(IApiProcessor api, IOptions<IssueConfiguration> config, IOptions<ConfigurationModel> github)
        {
            Api = api;
            _issueConfig = config.Value;

            Config = new GithubConfiguration { RepoName = github.Value.RepoName, Owner = github.Value.Owner};
        }
        private IApiProcessor Api { get; }
        private readonly IssueConfiguration _issueConfig;
        private GithubConfiguration Config { get; }

        public async Task Process(GithubIssuePayload payload)
        {
            var actions = new AdditionalActions();
            Api.Config = Config;

            var actionText = actions.Process(payload.issue.body);

            foreach (var allowed in _issueConfig.AllowedUsers)
            {
                if (payload.issue.user.login.Equals(allowed, StringComparison.CurrentCultureIgnoreCase))
                {
                    return;
                }
            }
            
            if (!string.IsNullOrEmpty(actionText))
            {
                // If we have an action then add a new comment first.
                await Api.Comment(new Comment { body = actionText }, payload.issue.number);
            }

            if (!payload.issue.body.Contains(_issueConfig.TemplateContains))
            {
                Console.WriteLine("Issue does not contain bug template");
                // Comment
                await Api.Comment(new Comment
                {
                    body = _issueConfig.TemplateNotFollowedText
                }, payload.issue.number);

                // Close
                await Api.CloseIssue(payload.issue.number);
                return;
            }

            await Api.Comment(new Comment { body = _issueConfig.Message }, payload.issue.number);
        }
    }
}
