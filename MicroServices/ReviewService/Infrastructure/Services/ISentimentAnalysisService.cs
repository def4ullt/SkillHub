namespace Infrastructure.Services
{
    public interface ISentimentAnalysisService
    {
        Task<(string Sentiment, List<string> KeyIssues)> AnalyzeAsync(string comment, CancellationToken cancellationToken = default);
    }
}
