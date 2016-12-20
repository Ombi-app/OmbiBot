namespace OmbiBot.Processor
{
    public interface IProcessor
    {
        void Process(GithubIssuePayload payload);
    }
}