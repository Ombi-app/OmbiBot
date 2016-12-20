using System.Threading.Tasks;

namespace OmbiBot.Processor
{
    public interface IProcessor
    {
        Task Process(GithubIssuePayload payload);
    }
}