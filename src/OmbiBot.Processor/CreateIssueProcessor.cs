using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OmbiBot.Processor.Models;

namespace OmbiBot.Processor
{
    public class CreateIssueProcessor : IProcessor
    {
        public CreateIssueProcessor(IApiProcessor api)
        {
            Api = api;
            Config = new GithubConfiguration {RepoName = "OmbiBot", Owner = "Tidusjar"};
        }
        private IApiProcessor Api { get; }

        private GithubConfiguration Config { get; }

        public async Task Process(GithubIssuePayload payload)
        {
            Api.Config = Config;

            var defaultText = @"Hi!
Thanks for the issue report. Before a real human comes by, please make sure you used our bug report format.
Before posting make sure you also read our [FAQ](https://github.com/tidusjar/Ombi/wiki/FAQ) and [known issues](https://github.com/tidusjar/Ombi/wiki/Known-Issues).
Make the title describe your issue. Having ""not working"" or ""I get this bug"" for 100 issues, isn't really helpful.
If we need more information or there is some progress we tag the issue or update the tag and keep you updated.
Cheers!
Ombi Support Team";

            if (!payload.issue.body.Contains("Problem Description:"))
           
            {
                Console.WriteLine("Issue does not contain Ombi Version");
                // Comment
                await Api.Comment(new Comment
                    {
                        body =
                            @"Hello, Please use the Github template to report an issue, If it is a feature request then please visit: http://feathub.com/tidusjar/Ombi 
                            cheers!
                            Ombi Support Team"
                    }, payload.issue.number);

                // Close
                await Api.CloseIssue(payload.issue.number);
                return;
            }

            await Api.Comment(new Comment {body = defaultText}, payload.issue.number);

        }
    }
}
