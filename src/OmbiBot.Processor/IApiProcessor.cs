using System.Threading.Tasks;
using OmbiBot.Processor.Models;
using RestSharp.Portable;

namespace OmbiBot.Processor
{
    public interface IApiProcessor
    {
        Task<IRestResponse<GithubIssue>> CloseIssue(int issueId);
        Task<IRestResponse<CommentResponse>> Comment(Comment comment, int issueNumber);
        Task<IRestResponse<GithubIssue>> GetIssue(int issueId);
    }
}