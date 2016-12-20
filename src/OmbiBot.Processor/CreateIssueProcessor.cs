using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OmbiBot.Processor.Models;

namespace OmbiBot.Processor
{
    public class CreateIssueProcessor : IProcessor
    {
        public CreateIssueProcessor()
        {
            Config = new GithubConfiguration {RepoName = "OmbiBot", Owner = "Tidusjar"};
        }

        private GithubConfiguration Config { get; }

        public void Process(GithubIssuePayload payload)
        {
            var api = new ApiProcessor(Config);

            api.Comment(new Comment {Body = "Test", IssueNumber = 1});
        }
    }
}
