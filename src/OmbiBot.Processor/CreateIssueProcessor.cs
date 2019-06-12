using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OmbiBot.Processor.Models;

namespace OmbiBot.Processor
{
    public class CreateIssueProcessor : IProcessor
    {
        private readonly ILogger _logger;
        private readonly IApiProcessor _api;

        public CreateIssueProcessor(IApiProcessor api, ILogger logger)
        {
            _api = api;
            _logger = logger;
        }


        public async Task Process(GithubIssuePayload payload)
        {
            var allowedUsers = Environment.GetEnvironmentVariable("AllowedUsers").Split(new []{"," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var allowed in allowedUsers)
            {
                if (payload.issue.user.login.Equals(allowed, StringComparison.CurrentCultureIgnoreCase))
                {
                    return;
                }
            }

            if (!payload.issue.body.Contains(Environment.GetEnvironmentVariable("IssueTemplateMatch")))
            {
               _logger.LogDebug("Issue does not contain bug template");
                // Comment
                await _api.Comment(new Comment
                {
                    body = Environment.GetEnvironmentVariable("IssueTemplateNotFollowedText")
                }, payload.issue.number);

                // Close
                await _api.CloseIssue(payload.issue.number);
                return;
            }

            await _api.Comment(new Comment { body = Environment.GetEnvironmentVariable("GenericIssueMessage") }, payload.issue.number);
        }
    }
}
