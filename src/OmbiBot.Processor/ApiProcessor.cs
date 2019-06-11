using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OmbiBot.Processor.Models;
using RestSharp.Portable;
using RestSharp.Portable.WebRequest;

namespace OmbiBot.Processor
{
    public class ApiProcessor : IApiProcessor
    {

        public ApiProcessor(IOptions<ConfigurationModel> config)
        {
            AppSettings = config;
        }

        public GithubConfiguration Config { get; set; }
        private const string ApiEndpoint = "https://api.github.com/";
        private IOptions<ConfigurationModel> AppSettings { get; }

        // PATCH /repos/:owner/:repo/issues/:number
        public async Task<IRestResponse<GithubIssue>> CloseIssue(int issueId)
        {
            using (var client = new RestClient(new Uri(ApiEndpoint)))
            {
                var request = new RestRequest
                {
                    Resource = $"/repos/{Config.Owner}/{Config.RepoName}/issues/{issueId}",
                    Method = Method.PATCH
                };

                var issueTask = await GetIssue(issueId);
                var issue = issueTask.Data; 
                issue.state = "closed";

                request.AddJsonBody(issue);
                AddAuth(request);

                return await client.Execute<GithubIssue>(request);
            }

        }

        public async Task<IRestResponse<GithubIssue>> GetIssue(int issueId)
        {
            ///repos/:owner/:repo/issues/:number.

            using (var client = new RestClient(new Uri(ApiEndpoint)))
            {
                var request = new RestRequest
                {
                    Resource = $"/repos/{Config.Owner}/{Config.RepoName}/issues/{issueId}",
                    Method = Method.GET
                };
                AddAuth(request);

                return await client.Execute<GithubIssue>(request);
            }
        }

        // /repos/:owner/:repo/issues/:number/comments
        public async Task<IRestResponse<CommentResponse>> Comment(Comment comment, int issueNumber)
        {
            using (var client = new RestClient(new Uri(ApiEndpoint)))
            {
                var request = new RestRequest
                {
                    Resource = $"/repos/{Config.Owner}/{Config.RepoName}/issues/{issueNumber}/comments",
                    Method = Method.POST
                };

                request.AddJsonBody(comment);

                AddAuth(request);

                var response = client.Execute<CommentResponse>(request);

                return await response;
            }
        }

        private void AddAuth(RestRequest req)
        {
            
            req.AddHeader("Authorization", $"token {Environment.GetEnvironmentVariable("AuthToken")}");
        }
    }
}