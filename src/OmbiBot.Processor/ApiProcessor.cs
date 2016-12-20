using System;
using OmbiBot.Processor.Models;
using RestSharp.Portable;
using RestSharp.Portable.WebRequest;

namespace OmbiBot.Processor
{
    public class ApiProcessor
    {

        public ApiProcessor(GithubConfiguration config)
        {
            Config = config;
        }

        private GithubConfiguration Config { get; }
        private const string ApiEndpoint = "https://api.github.com/";

        // /repos/:owner/:repo/issues/:number/comments
        public CommentResponse Comment(Comment comment)
        {
            using (var client = new RestClient(new Uri(ApiEndpoint)))
            {
                var request = new RestRequest
                {
                    Resource = $"/repos/{Config.Owner}/{Config.RepoName}/issues/{comment.IssueNumber}/comments",
                    Method = Method.POST
                };

                request.AddJsonBody(comment.Body);

               return client.Execute<CommentResponse>(request).Result.Data;
            }
        }
    }
}